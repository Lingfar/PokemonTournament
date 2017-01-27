using PokemonTournamentEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PokemonDataAccessLayer
{
    class DalManager : IDal
    {
        private static DalManager instance;
        private static object syncRoot = new Object();

        private IDal dal { get; set; }

        private DalManager()
        {
            dal = new DalSqlServer();
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
            return dal.GetAllPokemons();
        }

        public Pokemon GetPokemon(DataRow item)
        {
            return dal.GetPokemon(item);
        }

        public Caracteristique GetCaracteristiqueById(int id)
        {
            return dal.GetCaracteristiqueById(id);
        }

        public Caracteristique GetCaracteristique(DataRow item)
        {
            return dal.GetCaracteristique(item);
        }
    }
}
