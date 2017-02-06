using PokemonTournamentEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentWPF.ViewModel
{
    class CaracteristiqueViewModel : ViewModelBase
    {
        private Caracteristique caracteristique;
        public Caracteristique Caracteristique
        {
            get { return caracteristique; }
            set { caracteristique = value; }
        }

        public int ID
        {
            get { return Caracteristique.ID; }
            set
            {
                if (value == Caracteristique.ID) return;
                Caracteristique.ID = value;
                base.OnPropertyChanged("ID");
            }
        }

        public int Attaque
        {
            get { return Caracteristique.Attaque; }
            set
            {
                if (value == Caracteristique.Attaque) return;
                Caracteristique.Attaque = value;
                base.OnPropertyChanged("Attaque");
            }
        }

        public int Defense
        {
            get { return Caracteristique.Defense; }
            set
            {
                if (value == Caracteristique.Defense) return;
                Caracteristique.Defense = value;
                base.OnPropertyChanged("Defense");
            }
        }

        public int PV
        {
            get { return Caracteristique.PV; }
            set
            {
                if (value == Caracteristique.PV) return;
                Caracteristique.PV = value;
                base.OnPropertyChanged("PV");
            }
        }

        public int Vitesse
        {
            get { return Caracteristique.Vitesse; }
            set
            {
                if (value == Caracteristique.Vitesse) return;
                Caracteristique.Vitesse = value;
                base.OnPropertyChanged("Vitesse");
            }
        }

        public int Esquive
        {
            get { return Caracteristique.Esquive; }
            set
            {
                if (value == Caracteristique.Esquive) return;
                Caracteristique.Esquive = value;
                base.OnPropertyChanged("Esquive");
            }
        }

        public CaracteristiqueViewModel(Caracteristique carac)
        {
            if (carac != null)
                Caracteristique = carac;
            else
                Caracteristique = new Caracteristique();
        }
    }
}
