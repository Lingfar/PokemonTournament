using PokemonTournamentEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonDataAccessLayer
{
    class DalManager
    {
        private static DalManager instance;
        private static object syncRoot = new Object();

        private DalManager()
        {
        }

        public static DalManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DalManager();
                    }
                }
                return instance;
            }
        }

        public void AddMatchToList(Match m)
        {
            allMatchs.Add(m);
        }

        public Pokemon GetWinner()
        {
            return GetAllPokemons()[allMatchs.Last().IdPokemonVainqueur];
        }

        public List<Pokemon> GetAllPokemons()
        {
            return allPokemons;
        }

        public List<Pokemon> GetAllPokemonsByType(ETypeElement type)
        {
            return GetAllPokemons().FindAll(p => p.Type == type);
        }

        public List<Match> GetAllMatchs()
        {
            return allMatchs;
        }

        public List<Stade> GetAllStades()
        {
            return allStades;
        }

        public List<Caracteristique> GetAllCaracteristiques()
        {
            return allCaracteristiques;
        }

        public void AddNewStade(Stade stade)
        {
            stade.ID = LastId++;         
            allStades.Add(stade);
        }
        public void AddNewPokemon(Pokemon poke)
        {
            poke.ID = LastId++;
            allPokemons.Add(poke);
        }
        public void DeleteNewStade(Stade stade)
        {
            int index = allStades.FindIndex(s => s.ID == stade.ID);
            allStades.RemoveAt(index);
        }
        public void DeleteNewPokemon(Pokemon poke)
        {
            int index = allPokemons.FindIndex(s => s.ID == poke.ID);
            allPokemons.RemoveAt(index);
        }


        public static Utilisateur GetUtilisateurByLogin(string login)
        {
            return allUtilisateurs.Find(u => u.Login.ToLower() == login.ToLower());
        }
    }
}
