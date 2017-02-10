using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;

namespace PokemonTournamentWPF.ViewModel
{
    public class PokemonViewModel : ViewModelBase
    {
        public Pokemon poke { get; set; }

        public PokemonViewModel()
        {
            poke = new Pokemon();
            base.OnPropertyChanged("Nom");
            base.OnPropertyChanged("Type");
            base.OnPropertyChanged("Caracteristiques");
        }
        public PokemonViewModel (Pokemon orig)
        {
            poke = orig;
            base.OnPropertyChanged("Nom");
            base.OnPropertyChanged("Type");
            base.OnPropertyChanged("Caracteristiques");
        }

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
            get { return poke.Type; }
            set
            {
                if (value == poke.Type) return;
                poke.Type = value;
                base.OnPropertyChanged("Type");
            }
        }

        public IEnumerable<ETypeElement> ETypeElementValues
        {
            get
            {
                return Enum.GetValues(typeof(ETypeElement))
                    .Cast<ETypeElement>();
            }
        }

        public Caracteristique Caracteristiques
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

        public int ID
        {
            get
            {
                return poke.ID;
            }
            set
            {

            }
        }

        public override string ToString()
        {
            return poke.ToString();
        }

    }
}
