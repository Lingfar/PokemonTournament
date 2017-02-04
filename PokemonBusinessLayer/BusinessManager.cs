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
            List<Stade> allStades = dalManager.GetAllStades();
            Stade.NbStades = allStades.Count;
            return allStades;
            //return dalManagerStub.GetAllStades();
        }

        public List<Caracteristique> GetAllCaracteristiques()
        {
            return dalManagerStub.GetAllCaracteristiques();
        }
        #endregion
        
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

        public bool UpdateStade(Stade stade)
        {
            return dalManager.UpdateStade(stade);
        }
        public bool UpdateTournoi(Tournoi tournoi)
        {
            return dalManager.UpdateTournoi(tournoi);
        }


        public void DeletePokemon(Pokemon poke)
        {
            //dalManager.DeleteNewPokemon(poke);
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