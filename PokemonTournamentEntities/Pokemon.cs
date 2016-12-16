using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentEntities
{
    public enum ETypeElement
    {
        Eau, Feu, Sol, Insecte, Plante, Dragon
    }

    public class Pokemon : EntityObject
    {
        private static Random rand = new Random(42);
        public Caracteristiques Caracteristiques { get; set; }
        public string Nom { get; set; }
        public ETypeElement Type { get; set; }

        public Pokemon(int id) : base(id)
        {
            Caracteristiques = new Caracteristiques();
        }

        public override string ToString()
        {
            return "Pokemon : Id = " + base.ToString() + " Nom =" + Nom + " Caracteristiques : " + Caracteristiques.ToString()
                + " Type = " + Type.ToString();
        }
    }
}
