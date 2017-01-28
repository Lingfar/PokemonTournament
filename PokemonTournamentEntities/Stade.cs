using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentEntities
{
    public class Stade : EntityObject
    {
        public int NbStades = 0;

        public string Nom { get; set; }
        public ETypeElement Type { get; set; }
        public int NbPlaces { get; set; }
        public Caracteristique Caracteristiques { get; set; }

        public Stade()
        {
            Caracteristiques = new Caracteristique();
        }

        public Stade(int id) : base(id)
        {
            Caracteristiques = new Caracteristique();
        }

        public override string ToString()
        {
            return "Stade : " + base.ToString() + " Nom =" + Nom + " Caracteristiques = " + Caracteristiques.ToString()
                + " NbPlaces = " + NbPlaces + " Type = " + Type;
        }
    }
}
