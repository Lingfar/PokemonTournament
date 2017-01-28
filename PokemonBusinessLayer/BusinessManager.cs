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

        public static bool CheckConnectionUser(string login, string password)
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

        #region Get data
        public List<Pokemon> GetAllPokemons()
        {
            //return dalManagerStub.GetAllPokemons();
            return dalManager.GetAllPokemons();
        }

        public List<Pokemon> GetAllPokemonsByType(ETypeElement type)
        {
            return dalManagerStub.GetAllPokemonsByType(type);
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
            return dalManagerStub.GetAllStades();
        }

        public List<Caracteristique> GetAllCaracteristiques()
        {
            return dalManagerStub.GetAllCaracteristiques();
        }
        #endregion
        
        public void AddNewStade(Stade stade)
        {
            dalManagerStub.AddNewStade(stade);
        }
        public void AddMatches(List<Match> matches)
        {
            foreach (Match m in matches)
            {
                //dalManagerStub.AddMatchToList(m);
                dalManager.InsertMatch(m);
            }
        }


        public void DeletePokemon(Pokemon poke)
        {
            //dalManager.DeleteNewPokemon(poke);
        }
        public void DeleteStade(Stade stade)
        {
            dalManagerStub.DeleteNewStade(stade);
        }

        #region Tournoi
        public Tournoi RunTournament()
        {
            Tournoi tournament = new Tournoi("Pokemon Tournament");            
            tournament.Pokemons.AddRange(dalManager.GetAllPokemons());
            for (int i = 0; i < 5; i++)
            {
                RunPhaseOfTournament(tournament, (EPhaseTournoi)i);
            }
            return tournament;
        }

        private List<Pokemon> RunPhaseOfTournament(Tournoi tournament, EPhaseTournoi phase)
        {
            for (int i = tournament.Pokemons.Count - 1; i >= 0; i -= 2)
            {
                Match match;
                if (FastestPokemon(tournament.Pokemons[i], tournament.Pokemons[i - 1]))
                    match = RunMatch(tournament.Pokemons[i], tournament.Pokemons[i - 1], phase);
                else
                    match = RunMatch(tournament.Pokemons[i - 1], tournament.Pokemons[i], phase);
                dalManagerStub.AddMatchToList(match);

                if (tournament.Pokemons[i].ID == match.IdPokemonVainqueur)
                    tournament.Pokemons.RemoveAt(i - 1);
                else
                    tournament.Pokemons.RemoveAt(i);
            }

            return tournament.Pokemons;
        }

        private Match RunMatch(Pokemon pokemon1, Pokemon pokemon2, EPhaseTournoi phase)
        {
            Caracteristique newCaracP1 = new Caracteristique(pokemon1.Caracteristiques);
            Caracteristique newCaracP2 = new Caracteristique(pokemon2.Caracteristiques);

            Match match = new Match(PokemonDataAccessLayerStub.DalManager.LastId, phase, pokemon1, pokemon2);
            PokemonDataAccessLayerStub.DalManager.LastId++;
            match.Stade = dalManagerStub.GetAllStades()[rng.Next(0, 6)];

            BuffNerfPokemonByStade(pokemon1.Type, newCaracP1, match.Stade);
            BuffNerfPokemonByStade(pokemon2.Type, newCaracP2, match.Stade);

            decimal multiplicatorP1 = GetMultiplicatorBetweenType(pokemon1.Type, pokemon2.Type);
            decimal multiplicatorP2 = GetMultiplicatorBetweenType(pokemon2.Type, pokemon1.Type);
            while (newCaracP1.PV > 0 && newCaracP2.PV > 0)
            {
                if (!EsquiveOrNot(pokemon2))
                    newCaracP2.PV -= (int)Math.Ceiling(multiplicatorP1 * (decimal)newCaracP1.Attaque / (decimal)newCaracP2.Defense * 4m);
                if (!EsquiveOrNot(pokemon1))
                    newCaracP1.PV -= (int)Math.Ceiling(multiplicatorP2 * (decimal)newCaracP2.Attaque / (decimal)newCaracP1.Defense * 4m);
            }

            if (newCaracP1.PV <= 0 && newCaracP2.PV <= 0)
                match.IdPokemonVainqueur = pokemon1.ID;
            else if (newCaracP1.PV <= 0)
                match.IdPokemonVainqueur = pokemon2.ID;
            else
                match.IdPokemonVainqueur = pokemon1.ID;

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

        private void BuffNerfPokemonByStade(ETypeElement type, Caracteristique carac, Stade stade)
        {
            if (type == stade.Type)
            {
                carac.Attaque += stade.Caracteristiques.Attaque;
                carac.Defense += stade.Caracteristiques.Defense;
            }
            else if (GetMultiplicatorBetweenType(type, stade.Type) == 0.5m)
            {
                carac.Attaque -= stade.Caracteristiques.Attaque;
                carac.Defense -= stade.Caracteristiques.Defense;
            }
        }

        private bool FastestPokemon(Pokemon pokemon1, Pokemon pokemon2)
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
    }
}