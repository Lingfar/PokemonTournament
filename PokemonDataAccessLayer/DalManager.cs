using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;

namespace PokemonDataAccessLayer
{
    public class DalManager
    {
        private List<Pokemon> allPokemons { get; set; }
        private List<Match> allMatchs { get; set; }
        private List<Stade> allStades { get; set; }
        private List<Caracteristiques> allCaracteristiques { get; set; }

        public DalManager()
        {
            allPokemons = new List<Pokemon>();
            allMatchs = new List<Match>();
            allStades = new List<Stade>();
            allCaracteristiques = new List<Caracteristiques>();
            int id = 0;

            for(int i = 0; i < 32; i++)
            {
                Pokemon poke = Rand.GeneratePokemon(id);
                allPokemons.Add(poke);
                allCaracteristiques.Add(poke.Caracteristiques);
                id++;
            }

            //Eau, Feu, Terre, Insecte, Plante, Dragon
            for (int i = 0; i < 6; i++)
            {
                allStades.Add(Rand.GenerateStade(id, (ETypeElement)i));
                id++;
            }
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

        public List<Caracteristiques> GetAllCaracteristiques()
        {
            return allCaracteristiques;
        }
    }
}
