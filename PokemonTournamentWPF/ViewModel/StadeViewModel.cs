using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;
using System.Windows.Input;
using System.Windows;

namespace PokemonTournamentWPF.ViewModel
{
    public class StadeViewModel : ViewModelBase
    {
        // Model encapsulé dans le ViewModel
        private Stade stade;
        public Stade Stade
        {
            get { return stade; }
            set { stade = value; }
        }

        public StadeViewModel()
        {
            Stade = new Stade();
        }

        public StadeViewModel(Stade stadeModel)
        {
            if (stadeModel != null)
                Stade = stadeModel;
            else
                Stade = new Stade();
        }

        public int ID
        {
            get { return stade.ID; }
            set
            {
                if (value == stade.ID) return;
                stade.ID = value;
                base.OnPropertyChanged("ID");
            }
        }

        public string Nom
        {
            get { return stade.Nom; }
            set
            {
                if (value == stade.Nom) return;
                stade.Nom = value;
                base.OnPropertyChanged("Nom");
            }
        }

        public ETypeElement Type
        {
            get { return stade.Type; }
            set
            {
                if (value == stade.Type) return;
                stade.Type = value;
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

        public int NbPlaces
        {
            get { return stade.NbPlaces; }
            set
            {
                if (value == stade.NbPlaces) return;
                stade.NbPlaces = value;
                base.OnPropertyChanged("NbPlaces");
            }
        }

        public int Attaque
        {
            get { return stade.Attaque; }
            set
            {
                if (value == stade.Attaque) return;
                stade.Attaque = value;
                base.OnPropertyChanged("Attaque");
            }
        }

        public int Defense
        {
            get { return stade.Defense; }
            set
            {
                if (value == stade.Defense) return;
                stade.Defense = value;
                base.OnPropertyChanged("Defense");
            }
        }
    }
}