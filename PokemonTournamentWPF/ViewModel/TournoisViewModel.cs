using PokemonTournamentEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokemonTournamentWPF.ViewModel
{
    public class TournoisViewModel : ViewModelBase
    {
        private TournoiViewModel selectedItem;
        public TournoiViewModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        private ObservableCollection<TournoiViewModel> tournois;
        public ObservableCollection<TournoiViewModel> Tournois
        {
            get { return tournois; }
            private set
            {
                tournois = value;
                OnPropertyChanged("Tournois");
            }
        }

        public TournoisViewModel(List<Tournoi> tournoisModels)
        {
            SelectedItem = new TournoiViewModel();
            Tournois = new ObservableCollection<TournoiViewModel>();
            foreach (Tournoi tournoi in tournoisModels)
                Tournois.Add(new TournoiViewModel(tournoi));
        }

        // Commande New
        private RelayCommand newCommand;
        public ICommand NewCommand
        {
            get
            {
                if (newCommand == null)
                {
                    newCommand = new RelayCommand(
                        () => this.New(),
                        () => this.CanNew()
                        );
                }
                return newCommand;
            }
        }
        private bool CanNew()
        {
            return (SelectedItem != null && SelectedItem.ID == 0);
        }
        private void New()
        {
            if (SelectedItem != null && SelectedItem.Tournoi.Nom != null)
            {
                SelectedItem.Tournoi.SetPokemonsAndStades(PokemonBusinessLayer.BusinessManager.Instance.GetAllPokemons(),
                        PokemonBusinessLayer.BusinessManager.Instance.GetAllStades());
                SelectedItem.Tournoi.Run();
                if (PokemonBusinessLayer.BusinessManager.Instance.AddTournoi(SelectedItem.Tournoi))
                {                    
                    PokemonBusinessLayer.BusinessManager.Instance.AddMatches(SelectedItem.Tournoi.Matches);
                    System.Windows.Forms.MessageBox.Show("Génération du tournoi réussie", "Succeed");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("La génération du stade a échoué", "Failed");
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Veuillez renseigner un nom pour le tournoi", "Error");
            }
        }

        // Commande Clear
        private RelayCommand clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (clearCommand == null)
                {
                    clearCommand = new RelayCommand(
                        () => this.Clear(),
                        () => this.CanClear()
                        );
                }
                return clearCommand;
            }
        }
        private bool CanClear()
        {
            return (SelectedItem != null);
        }
        private void Clear()
        {
            if (SelectedItem != null)
                SelectedItem = new TournoiViewModel();
        }

        // Commande Remove
        private RelayCommand removeCommand;
        public ICommand RemoveCommand
        {
            get
            {
                if (removeCommand == null)
                {
                    removeCommand = new RelayCommand(
                        () => this.Remove(),
                        () => this.CanRemove()
                        );
                }
                return removeCommand;
            }
        }
        private bool CanRemove()
        {
            return (SelectedItem != null && SelectedItem.ID != 0);
        }
        private void Remove()
        {
            if (SelectedItem != null)
            {

            }
        }
    }
}
