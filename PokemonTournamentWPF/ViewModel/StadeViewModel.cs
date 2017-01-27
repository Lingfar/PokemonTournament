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
        public event EventHandler<EventArgs> CloseNotified;
        protected void OnCloseNotified(EventArgs e)
        {
            CloseNotified(this, e);
        }

        // Model encapsulé dans le ViewModel
        private Stade stade;
        public Stade Stade
        {
            get { return stade; }
            set { stade = value; }
        }

        public StadeViewModel()
        {
            stade = new Stade();
        }

        public StadeViewModel(Stade stadeModel)
        {
            if (stadeModel != null)
                stade = stadeModel;
            else
                stade = new Stade();
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

        public Caracteristique Caracteristiques
        {
            get { return stade.Caracteristiques; }
            set
            {
                if (value == stade.Caracteristiques) return;
                stade.Caracteristiques = value;
                base.OnPropertyChanged("Caracteristiques");
            }
        }
    }
}