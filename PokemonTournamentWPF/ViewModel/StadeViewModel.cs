using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;
using System.Windows.Input;
using PokemonBusinessLayer;
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

        public ETypeElementModel TypeModel
        {
            get { return new ETypeElementModel(stade.Type); }
            set
            {
                if (value.Type == stade.Type) return;
                stade.Type = value.Type;
                base.OnPropertyChanged("Type");
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

        public Caracteristiques Caracteristiques
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