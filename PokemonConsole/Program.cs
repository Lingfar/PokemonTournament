using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonBusinessLayer;

namespace PokemonConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            BusinessManager businessManager = BusinessManager.Instance;            
            businessManager.RunTournament();

            List<string> allMatchs = businessManager.DisplayAllMatchs();
            foreach (string match in allMatchs)
                Console.WriteLine(match);

            Console.WriteLine("\nAnd the winner is " + businessManager.DisplayWinner());

            Console.ReadKey();
        }
    }
}
