using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentEntities
{
    [Serializable]
    public class Caracteristique : EntityObject
    {
        private static Random rand = new Random(42);

        private int pv;
        public int PV
        {
            get { return pv; }
            set
            {
                if (value > 200)
                    pv = 200;
                else
                    pv = value;
            }
        }

        private int attaque;
        public int Attaque
        {
            get { return attaque; }
            set
            {
                if (value > 20)
                    attaque = 20;
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
                if (value > 20)
                    defense = 20;
                else if (value <= 0)
                    defense = 1;
                else
                    defense = value;
            }
        }

        private int vitesse;
        public int Vitesse
        {
            get { return vitesse; }
            set
            {
                if (value > 20)
                    vitesse = 20;
                else if (value <= 0)
                    vitesse = 1;
                else
                    vitesse = value;
            }
        }

        private int esquive;
        public int Esquive
        {
            get { return esquive; }
            set
            {
                if (value > 50)
                    esquive = 50;
                else if (value <= 0)
                    esquive = 0;
                else
                    esquive = value;
            }
        }

        public Caracteristique()
        {
            
        }

        public Caracteristique(int attaque, int defense)
        {
            Attaque = attaque;
            Defense = defense;
        }

        public Caracteristique(Caracteristique c)
        {
            PV = c.PV;
            Attaque = c.Attaque;
            Defense = c.Defense;
            Vitesse = c.Vitesse;
            Esquive = c.Esquive;
        }

        public override string ToString()
        {
            return "PV = " + PV + " Attaque = " + Attaque + " Defense = " + Defense
                + " Vitesse = " + Vitesse + " Esquive = " + Esquive;
        }
    }
}
