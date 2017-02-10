using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;
using PokemonDataAccessLayer;
using PokemonDataAccessLayerStub;

namespace PokemonBusinessLayer
{
    public sealed class BusinessManager
    {
        private static BusinessManager instance;
        private static object syncRoot = new Object();

        private PokemonDataAccessLayerStub.DalManager dalManagerStub { get; set; }
        private Random rng { get; set; }

        private PokemonDataAccessLayer.DalManager dalManager { get; set; } 

        private BusinessManager()
        {
            dalManagerStub = PokemonDataAccessLayerStub.DalManager.Instance;
            dalManager = PokemonDataAccessLayer.DalManager.Instance;
            rng = new Random(5);
        }

        public static BusinessManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new BusinessManager();
                    }
                }
                return instance;
            }
        }

        public static bool CheckConnectionUserStub(string login, string password)
        {
            bool user = false;
            Utilisateur utilisateur = PokemonDataAccessLayerStub.DalManager.GetUtilisateurByLogin(login);
            if (utilisateur != null && utilisateur.Password == password)
                user = true;
            return user;
        }

        #region Display Console
        public List<string> DisplayAllPokemons()
        {
            return dalManagerStub.GetAllPokemons().Select(p => p.ToString()).ToList();
        }

        public List<string> DisplayAllPokemonsByType(ETypeElement type)
        {
            return dalManagerStub.GetAllPokemonsByType(type).Select(p => p.ToString()).ToList();
        }

        public List<string> DisplayAllPokemonsByStats(int attaque, int pv)
        {
            return dalManagerStub.GetAllPokemons().Where(p => p.Caracteristiques.Attaque >= attaque
                && p.Caracteristiques.PV >= pv).Select(p => p.ToString()).ToList();
        }

        public List<string> DisplayAllMatchs()
        {
            return dalManagerStub.GetAllMatchs().Select(m => m.ToString()).ToList();
        }

        public List<string> DisplayMatchsByPlaces(int nbPlaces)
        {
            return dalManagerStub.GetAllMatchs().Where(m => m.Stade.NbPlaces >= nbPlaces).Select(m => m.ToString()).ToList();
        }

        public List<string> DisplayAllStades()
        {
            return dalManagerStub.GetAllStades().Select(s => s.ToString()).ToList();
        }

        public List<string> DisplayAllCaracteristiques()
        {
            return dalManagerStub.GetAllCaracteristiques().Select(c => c.ToString()).ToList();
        }

        public string DisplayWinner()
        {
            return dalManagerStub.GetWinner().ToString();
        }
        #endregion

        #region Tournoi Mode Console
        public void RunTournament()
        {
            List<Pokemon> pokemons = new List<Pokemon>();
            pokemons.AddRange(dalManagerStub.GetAllPokemons());
            for (int i = 0; i < 5; i++)
            {
                RunPhaseOfTournament(pokemons, (EPhaseTournoi)i);
            }
        }

        private List<Pokemon> RunPhaseOfTournament(List<Pokemon> pokemons, EPhaseTournoi phase)
        {
            for (int i = pokemons.Count - 1; i >= 0; i -= 2)
            {
                Match match;
                if (FasterPokemon(pokemons[i], pokemons[i - 1]))
                    match = RunMatch(pokemons[i], pokemons[i - 1], phase);
                else
                    match = RunMatch(pokemons[i - 1], pokemons[i], phase);
                dalManagerStub.AddMatchToList(match);

                if (pokemons[i].ID == match.IdPokemonVainqueur)
                    pokemons.RemoveAt(i - 1);
                else
                    pokemons.RemoveAt(i);
            }

            return pokemons;
        }

        private Match RunMatch(Pokemon pokemon1, Pokemon pokemon2, EPhaseTournoi phase)
        {
            Caracteristique oldCaracP1 = new Caracteristique(pokemon1.Caracteristiques);
            Caracteristique oldCaracP2 = new Caracteristique(pokemon2.Caracteristiques);

            Match match = new Match(PokemonDataAccessLayerStub.DalManager.LastId, phase, pokemon1, pokemon2);
            PokemonDataAccessLayerStub.DalManager.LastId++;

            match.Stade = dalManagerStub.GetAllStades()[rng.Next(0, 6)];

            BuffNerfPokemonByStade(pokemon1, match.Stade);
            BuffNerfPokemonByStade(pokemon2, match.Stade);

            decimal multiplicatorP1 = GetMultiplicatorBetweenType(pokemon1.Type, pokemon2.Type);
            decimal multiplicatorP2 = GetMultiplicatorBetweenType(pokemon2.Type, pokemon1.Type);
            while (pokemon1.Caracteristiques.PV > 0 && pokemon2.Caracteristiques.PV > 0)
            {
                if (!EsquiveOrNot(pokemon2))
                    pokemon2.Caracteristiques.PV -= (int)Math.Ceiling(multiplicatorP1 * (decimal)pokemon1.Caracteristiques.Attaque / (decimal)pokemon2.Caracteristiques.Defense * 4m);
                if (!EsquiveOrNot(pokemon1))
                    pokemon1.Caracteristiques.PV -= (int)Math.Ceiling(multiplicatorP2 * (decimal)pokemon2.Caracteristiques.Attaque / (decimal)pokemon1.Caracteristiques.Defense * 4m);
            }

            if (pokemon1.Caracteristiques.PV <= 0 && pokemon2.Caracteristiques.PV <= 0)
                match.IdPokemonVainqueur = pokemon1.ID;
            else if (pokemon1.Caracteristiques.PV <= 0)
                match.IdPokemonVainqueur = pokemon2.ID;
            else
                match.IdPokemonVainqueur = pokemon1.ID;

            pokemon1.Caracteristiques = oldCaracP1;
            pokemon2.Caracteristiques = oldCaracP2;

            return match;
        }

        private decimal GetMultiplicatorBetweenType(ETypeElement type1, ETypeElement type2)
        {
            decimal multiplicator = 1m;
            if (Pokemon.TableFaiblesses[(int)type1, (int)type2] == -1)
                multiplicator = 0.5m;
            else if (Pokemon.TableFaiblesses[(int)type1, (int)type2] == 1)
                multiplicator = 2m;
            return multiplicator;
        }

        private void BuffNerfPokemonByStade(Pokemon pokemon, Stade stade)
        {
            if (pokemon.Type == stade.Type)
            {
                pokemon.Caracteristiques.Attaque += stade.Attaque;
                pokemon.Caracteristiques.Defense += stade.Defense;
            }
            else if (GetMultiplicatorBetweenType(pokemon.Type, stade.Type) == 0.5m)
            {
                pokemon.Caracteristiques.Attaque -= stade.Attaque;
                pokemon.Caracteristiques.Defense -= stade.Defense;
            }
        }

        private bool FasterPokemon(Pokemon pokemon1, Pokemon pokemon2)
        {
            bool firstPokemon1;
            if (pokemon1.Caracteristiques.Vitesse > pokemon2.Caracteristiques.Vitesse)
                firstPokemon1 = true;
            else if (pokemon1.Caracteristiques.Vitesse == pokemon2.Caracteristiques.Vitesse)
            {
                if (Rand.rand.Next(0, 1) > 0.5)
                    firstPokemon1 = true;
                else
                    firstPokemon1 = false;
            }
            else
                firstPokemon1 = false;

            return firstPokemon1;
        }

        private bool EsquiveOrNot(Pokemon pokemon1)
        {
            bool esquive = true;
            if (rng.Next(0, 101) > pokemon1.Caracteristiques.Esquive)
                esquive = false;
            return esquive;
        }
        #endregion
        
        public bool CheckConnectionUser(string login, string password)
        {
            bool user = false;
            Utilisateur utilisateur = dalManager.GetUtilisateurByLogin(login);
            if (utilisateur != null && utilisateur.Password == password)
                user = true;
            return user;
        }
        public bool RegisterLogin(Utilisateur user)
        {
            return dalManager.RegisterLogin(user);
        }

        public List<Tournoi> GetAllTournois()
        {
            return dalManager.GetAllTournois();
        }
        public List<Pokemon> GetAllPokemons()
        {
            //return dalManagerStub.GetAllPokemons();
            return dalManager.GetAllPokemons();
        }
        public List<Pokemon> GetAllPokemonsByType(ETypeElement type)
        {
            //return dalManagerStub.GetAllPokemonsByType(type);
            return dalManager.GetAllPokemonsByType(type);
        }
        public List<Pokemon> GetAllPokemonsByStats(int attaque, int pv)
        {
            return dalManagerStub.GetAllPokemons().FindAll(p => p.Caracteristiques.Attaque >= attaque && p.Caracteristiques.PV >= pv);
        }
        public List<Match> GetAllMatchs()
        {
            //return dalManagerStub.GetAllMatchs();
            return dalManager.GetAllMatches();
        }
        public List<Match> GetMatchsByPlaces(int nbPlaces)
        {
            return dalManagerStub.GetAllMatchs().FindAll(m => m.Stade.NbPlaces >= nbPlaces);
        }
        public List<Stade> GetAllStades()
        {
            List<Stade> allStades = dalManager.GetAllStades();
            Stade.NbStades = allStades.Count;
            return allStades;
            //return dalManagerStub.GetAllStades();
        }
        public List<Caracteristique> GetAllCaracteristiques()
        {
            //return dalManagerStub.GetAllCaracteristiques();
            return dalManager.GetAllCaracteristiques();
        }
        
        public bool AddStade(Stade stade)
        {
            bool succeed = dalManager.InsertStade(stade);
            if (succeed)
                Stade.NbStades++;
            return succeed;
            //dalManagerStub.AddNewStade(stade);
        }
        public void AddMatches(List<Match> matches)
        {
            foreach (Match m in matches)
            {
                //dalManagerStub.AddMatchToList(m);
                dalManager.InsertMatch(m);
            }
        }
        public bool AddTournoi(Tournoi tournoi)
        {
            return dalManager.InsertTournoi(tournoi);
        }
        public bool AddPokemon(Pokemon pokemon)
        {
            return dalManager.InsertPokemon(pokemon);
        }

        public bool UpdateStade(Stade stade)
        {
            return dalManager.UpdateStade(stade);
        }
        public bool UpdateTournoi(Tournoi tournoi)
        {
            return dalManager.UpdateTournoi(tournoi);
        }
        public bool UpdatePokemon(Pokemon pokemon)
        {
            return dalManager.UpdatePokemon(pokemon);
        }

        public bool DeletePokemon(Pokemon poke)
        {
            return dalManager.DeletePokemon(poke);
            //dalManagerStub.DeleteNewPokemon(poke);
        }
        public bool DeleteStade(Stade stade)
        {
            return dalManager.DeleteStade(stade);
            //dalManagerStub.DeleteNewStade(stade);
        }
        public bool DeleteTournoi(Tournoi tournoi)
        {
            return dalManager.DeleteTournoi(tournoi);
        }
    }
}