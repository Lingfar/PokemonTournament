using PokemonTournamentEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PokemonDataAccessLayer
{
    public class DalManager : IDal
    {
        private static DalManager instance;
        private static object syncRoot = new Object();

        private IDal dalDb { get; set; }

        private DalManager()
        {
            dalDb = new DalSqlServer();
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

        public Utilisateur GetUtilisateurByLogin(string login)
        {
            return dalDb.GetUtilisateurByLogin(login);
        }
        public bool RegisterLogin(Utilisateur user)
        {
            return dalDb.RegisterLogin(user);
        }

        public List<Tournoi> GetAllTournois()
        {
            return dalDb.GetAllTournois();
        }
        public List<Pokemon> GetAllPokemons()
        {
            return dalDb.GetAllPokemons();
        }
        public List<Pokemon> GetAllPokemonsByType(ETypeElement type)
        {
            return dalDb.GetAllPokemonsByType(type);
        }
        public List<Match> GetAllMatches()
        {
            return dalDb.GetAllMatches();
        }
        public List<Stade> GetAllStades()
        {
            return dalDb.GetAllStades();
        }
        public List<Caracteristique> GetAllCaracteristiques()
        {
            return dalDb.GetAllCaracteristiques();
        }


        public bool InsertPokemon(Pokemon pokemon)
        {
            return dalDb.InsertPokemon(pokemon);
        }
        public bool InsertMatch(Match match)
        {
            return dalDb.InsertMatch(match);
        }
        public bool InsertStade(Stade stade)
        {
            return dalDb.InsertStade(stade);
        }
        public bool InsertTournoi(Tournoi tournoi)
        {
            return dalDb.InsertTournoi(tournoi);
        }


        public bool UpdateStade(Stade stade)
        {
            return dalDb.UpdateStade(stade);
        }
        public bool UpdateTournoi(Tournoi tournoi)
        {
            return dalDb.UpdateTournoi(tournoi);
        }
        public bool UpdatePokemon(Pokemon pokemon)
        {
            return dalDb.UpdatePokemon(pokemon);
        }


        public bool DeleteStade(Stade stade)
        {
            return dalDb.DeleteStade(stade);
        }
        public bool DeleteTournoi(Tournoi tournoi)
        {
            return dalDb.DeleteTournoi(tournoi);
        }
        public bool DeletePokemon(Pokemon pokemon)
        {
            return dalDb.DeletePokemon(pokemon);
        }
    }
}
