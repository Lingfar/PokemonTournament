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

        public List<Pokemon> GetAllPokemons()
        {
            return dalDb.GetAllPokemons();
        }

        public List<Match> GetAllMatches()
        {
            return dalDb.GetAllMatches();
        }

        public List<Stade> GetAllStades()
        {
            return dalDb.GetAllStades();
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
    }
}
