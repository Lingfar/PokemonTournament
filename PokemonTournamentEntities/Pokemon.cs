using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentEntities
{
    public enum ETypeElement
    {
        Eau/*0*/, Feu/*1*/, Sol/*2*/, Insecte/*3*/, Plante/*4*/, Dragon/*5*/
    }

    [Serializable]
    public class Pokemon : EntityObject
    {
        private static Random rand = new Random(42);
        public static int[,] TableFaiblesses = new int[,] { {-1, 1, 1, 0, -1, -1}, {-1, -1, 1, 1, 1, -1},
            {0, 1, 0, -1, -1, 0}, {0, -1, 0, 0, 1, 0}, {1, -1, 1, -1, -1, -1}, {0, 0, 0, 0, 0, 1} };

        public string Nom { get; set; }
        public ETypeElement Type { get; set; }
        public Caracteristique Caracteristiques { get; set; }

        public Pokemon()
        {
            Caracteristiques = new Caracteristique();
        }

        public Pokemon(int id) : base(id)
        {
            Caracteristiques = new Caracteristique();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        //public override string ToString()
        //{
        //    return "Pokemon : " + base.ToString() + " Nom = " + Nom + " Caracteristiques : " + Caracteristiques.ToString()
        //        + " Type = " + Type.ToString();
        //}

        public override string ToString()
        {
            return base.ToString() + " Nom = " + Nom + " Type = " + Type.ToString();
        }
    }
}
