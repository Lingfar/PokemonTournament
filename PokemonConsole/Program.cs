using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;
using PokemonBusinessLayer;

namespace PokemonConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            BusinessManager businessManager = BusinessManager.Instance;

            List<string> allPokemons = businessManager.DisplayAllPokemons();
            foreach (string pokemon in allPokemons)
                Console.WriteLine(pokemon);

            Console.WriteLine();
            List<string> allStades = businessManager.DisplayAllStades();
            foreach (string stade in allStades)
                Console.WriteLine(stade);

            businessManager.RunTournament();

            Console.ReadKey();
        }
    }
}
