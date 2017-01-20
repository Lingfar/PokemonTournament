using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonTournamentEntities;
namespace PokemonTournamentWPF.ViewModel
{
    class PokemonModel
    {
        private Pokemon poke { get; set; }
        public String Nom
        {
            get
            {
                return poke.Nom;
            }
            set
            {
                poke.Nom = value;
            }
        }
        public ETypeElement Type
        {
            get
            {
                return poke.Type;
            }
            set
            {
                poke.Type = value;
            }
        }
        public Caracteristiques Caracteristiques
        {
            get
            {
                return poke.Caracteristiques;
            }
            set
            {
                poke.Caracteristiques = value;
            }
        }


        public override string ToString()
        {
            return poke.ToString();
        }

    }
}
