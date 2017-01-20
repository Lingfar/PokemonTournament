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

        PokemonModel (Pokemon orig)
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
