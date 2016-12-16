using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentEntities
{
    public class Caracteristiques
    {
        private static Random rand = new Random(42);
        public int PV { get; set; }
        public int Attaque { get; set; }
        public int Defense { get; set; }
        public int Vitesse { get; set; }

        public Caracteristiques()
        {
            
        }

        public override string ToString()
        {
            return "PV = " + PV + " Attaque = " + Attaque + " Defense = " + Defense
                + " Vitesse = " + Vitesse;
        }
    }
}
