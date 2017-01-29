using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentEntities
{
    public class Stade : EntityObject
    {
        public static int NbStades = 0;

        public string Nom { get; set; }
        public ETypeElement Type { get; set; }
        public int NbPlaces { get; set; }

        private int attaque;
        public int Attaque
        {
            get { return attaque; }
            set
            {
                if (value > 5)
                    attaque = 5;
                else if (value <= 0)
                    attaque = 1;
                else
                    attaque = value;
            }
        }

        private int defense;
        public int Defense
        {
            get { return defense; }
            set
            {
                if (value > 5)
                    defense = 5;
                else if (value <= 0)
                    defense = 1;
                else
                    defense = value;
            }
        }

        public Stade()
        {

        }

        public Stade(int id) : base(id)
        {

        }

        public override string ToString()
        {
            return "Stade : " + base.ToString() + " Nom =" + Nom + " Attaque = " + Attaque + " Defense = " + Defense
                + " NbPlaces = " + NbPlaces + " Type = " + Type;
        }
    }
}
