using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;
using PokemonDataAccessLayer;

namespace PokemonBusinessLayer
{
    public sealed class BusinessManager
    {
        private static BusinessManager instance;
        private static object syncRoot = new Object();

        private DalManager dalManager { get; set; }
        private Random rng { get; set; }

        private BusinessManager()
        {
            dalManager = new DalManager();
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
            Utilisateur utilisateur = DalManager.GetUtilisateurByLogin(login);
            if (utilisateur != null && utilisateur.Password == password)
                user = true;
            return user;
        }

        #region Display Console
        public List<string> DisplayAllPokemons()
        {
            return dalManager.GetAllPokemons().Select(p => p.ToString()).ToList();
        }

        public List<string> DisplayAllPokemonsByType(ETypeElement type)
        {
            return dalManager.GetAllPokemonsByType(type).Select(p => p.ToString()).ToList();
        }

        public List<string> DisplayAllPokemonsByStats(int attaque, int pv)
        {
            return dalManager.GetAllPokemons().Where(p => p.Caracteristiques.Attaque >= attaque
                && p.Caracteristiques.PV >= pv).Select(p => p.ToString()).ToList();
        }

        public List<string> DisplayAllMatchs()
        {
            return dalManager.GetAllMatchs().Select(m => m.ToString()).ToList();
        }

        public List<string> DisplayMatchsByPlaces(int nbPlaces)
        {
            return dalManager.GetAllMatchs().Where(m => m.Stade.NbPlaces >= nbPlaces).Select(m => m.ToString()).ToList();
        }

        public List<string> DisplayAllStades()
        {
            return dalManager.GetAllStades().Select(s => s.ToString()).ToList();
        }

        public List<string> DisplayAllCaracteristiques()
        {
            return dalManager.GetAllCaracteristiques().Select(c => c.ToString()).ToList();
        }

        public string DisplayWinner()
        {
            return dalManager.GetWinner().ToString();
        }
        #endregion

        #region Get data
        public List<Pokemon> GetAllPokemons()
        {
            return dalManager.GetAllPokemons();
        }

        public List<Pokemon> GetAllPokemonsByType(ETypeElement type)
        {
            return dalManager.GetAllPokemonsByType(type);
        }

        public List<Pokemon> GetAllPokemonsByStats(int attaque, int pv)
        {
            return dalManager.GetAllPokemons().FindAll(p => p.Caracteristiques.Attaque >= attaque && p.Caracteristiques.PV >= pv);
        }

        public List<Match> GetAllMatchs()
        {
            return dalManager.GetAllMatchs();
        }

        public List<Match> GetMatchsByPlaces(int nbPlaces)
        {
            return dalManager.GetAllMatchs().FindAll(m => m.Stade.NbPlaces >= nbPlaces);
        }

        public List<Stade> GetAllStades()
        {
            return dalManager.GetAllStades();
        }

        public List<Caracteristiques> GetAllCaracteristiques()
        {
            return dalManager.GetAllCaracteristiques();
        }
        #endregion

        public void AddNewStade(Stade stade)
        {
            dalManager.AddNewStade(stade);
        }

        public void ModifyStade(Stade stade)
        {

        }

        #region Tournoi
        public void RunTournament()
        {
            List<Pokemon> pokemons = new List<Pokemon>();
            pokemons.AddRange(dalManager.GetAllPokemons());
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
                if (FastestPokemon(pokemons[i], pokemons[i - 1]))
                    match = RunMatch(pokemons[i], pokemons[i - 1], phase);
                else
                    match = RunMatch(pokemons[i - 1], pokemons[i], phase);
                dalManager.AddMatchToList(match);

                if (pokemons[i].ID == match.IdPokemonVainqueur)
                    pokemons.RemoveAt(i - 1);
                else
                    pokemons.RemoveAt(i);
            }

            return pokemons;
        }

        private Match RunMatch(Pokemon pokemon1, Pokemon pokemon2, EPhaseTournoi phase)
        {
            Caracteristiques newCaracP1 = new Caracteristiques(pokemon1.Caracteristiques);
            Caracteristiques newCaracP2 = new Caracteristiques(pokemon2.Caracteristiques);

            Match match = new Match(DalManager.LastId, phase, pokemon1, pokemon2);
            DalManager.LastId++;
            match.Stade = dalManager.GetAllStades()[rng.Next(0, 6)];

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

        private void BuffNerfPokemonByStade(ETypeElement type, Caracteristiques carac, Stade stade)
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