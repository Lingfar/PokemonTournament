using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentEntities
{
    public class Partie
    {
        public int self_hp { get; set; }
        public int other_hp { get; set; }

        private Random rng = new Random();

        public Partie()
        {
            self_hp = 3;
            other_hp = 3;
        }
        public void think ( int posatt, int posdef )
        {
            int IA_att = rng.Next(0, 4);
            int IA_def = rng.Next(0, 4);

            if ( posatt == IA_def )
            {
                other_hp--;
            }
            if (posdef == IA_att)
            {
                self_hp--;
            }
        }
    }
}
