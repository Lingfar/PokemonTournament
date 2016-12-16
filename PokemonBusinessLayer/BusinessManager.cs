using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;
using PokemonDataAccessLayer;

namespace PokemonBusinessLayer
{
    public class BusinessManager
    {
        public static DalManager DalManager;

        public BusinessManager()
        {
            DalManager = new DalManager();
        }

        public List<string> DisplayAllPokemons()
        {
            return DalManager.GetAllPokemons().Select(p => p.ToString()).ToList();
        }

        public List<string> DisplayAllPokemonsByType(ETypeElement type)
        {
            return DalManager.GetAllPokemonsByType(type).Select(p => p.ToString()).ToList();
        }

        public List<string> DisplayAllPokemonsByStats(int attaque, int pv)
        {
            return DalManager.GetAllPokemons().Where(p => p.Caracteristiques.Attaque >= attaque
                && p.Caracteristiques.PV >= pv).Select(p => p.ToString()).ToList();
        }

        public List<string> DisplayAllMatchs()
        {
            return DalManager.GetAllMatchs().Select(m => m.ToString()).ToList();
        }

        public List<string> DisplayMatchsByPlaces(int nbPlaces)
        {
            return DalManager.GetAllMatchs().Where(m => m.Stade.NbPlaces >= 200).Select(m => m.ToString()).ToList();
        }

        public List<string> DisplayAllStades()
        {
            return DalManager.GetAllStades().Select(s => s.ToString()).ToList();
        }

        public List<string> DisplayAllCaracteristiques()
        {
            return DalManager.GetAllCaracteristiques().Select(c => c.ToString()).ToList();
        }
    }
}
