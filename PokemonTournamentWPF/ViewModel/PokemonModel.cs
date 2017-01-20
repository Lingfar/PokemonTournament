using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonTournamentEntities;
namespace PokemonTournamentWPF.ViewModel
{
    public class PokemonModel : ViewModelBase
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
                base.OnPropertyChanged("Nom");
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
                base.OnPropertyChanged("Type");
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
                base.OnPropertyChanged("Caracteristiques");
            }
        }

        public override string ToString()
        {
            return poke.ToString();
        }

    }
}
