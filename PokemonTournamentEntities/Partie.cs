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
        public Partie()
        {
            self_hp = 3;
            other_hp = 3;
        }
        public void think ( int posatt, int posdef )
        {
            int IA_att = 0;
            int IA_def = 0;

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
