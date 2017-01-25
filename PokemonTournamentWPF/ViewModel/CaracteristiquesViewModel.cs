using PokemonTournamentEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentWPF.ViewModel
{
    class CaracteristiquesViewModel : ViewModelBase
    {
        private ObservableCollection<CaracteristiqueViewModel> caracteristiques;
        public ObservableCollection<CaracteristiqueViewModel> Caracteristiques
        {
            get { return caracteristiques; }
            private set
            {
                caracteristiques = value;
                OnPropertyChanged("Caracteristiques");
            }
        }

        public CaracteristiquesViewModel(List<Caracteristique> caracteristiquesModel)
        {
            Caracteristiques = new ObservableCollection<CaracteristiqueViewModel>();
            foreach (Caracteristique carac in caracteristiquesModel)
            {
                Caracteristiques.Add(new CaracteristiqueViewModel(carac));
            }
        }
    }
}
