using PokemonTournamentEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
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
            SetTournois(tournoisModels);
        }

        private void SetTournois(List<Tournoi> listTournois)
        {
            Tournois = new ObservableCollection<TournoiViewModel>();
            foreach (Tournoi tournoi in listTournois)
                Tournois.Add(new TournoiViewModel(tournoi));
        }

        // Commande New
        private RelayCommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(
                        () => this.Add(),
                        () => this.CanAdd()
                        );
                }
                return addCommand;
            }
        }
        private bool CanAdd()
        {
            return (SelectedItem != null && SelectedItem.ID == 0);
        }
        private void Add()
        {
            if (SelectedItem != null && SelectedItem.Tournoi.Nom != null)
            {
                TournoiViewModel t = new TournoiViewModel(SelectedItem);
                t.Tournoi.SetPokemonsAndStades(PokemonBusinessLayer.BusinessManager.Instance.GetAllPokemons(),
                        PokemonBusinessLayer.BusinessManager.Instance.GetAllStades());
                t.Tournoi.Run();
                if (PokemonBusinessLayer.BusinessManager.Instance.AddTournoi(t.Tournoi))
                {
                    PokemonBusinessLayer.BusinessManager.Instance.AddMatches(t.Tournoi.Matches);
                    Tournois.Add(t);
                    System.Windows.Forms.MessageBox.Show("Génération du tournoi réussie", "Succeed");                                      
                    SetTournois(PokemonBusinessLayer.BusinessManager.Instance.GetAllTournois());
                    SelectedItem = Tournois.Last();  
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

        // Commande Modify
        private RelayCommand modifyCommand;
        public ICommand ModifyCommand
        {
            get
            {
                if (modifyCommand == null)
                {
                    modifyCommand = new RelayCommand(
                        () => this.Modify(),
                        () => this.CanModify()
                        );
                }
                return modifyCommand;
            }
        }
        private bool CanModify()
        {
            return (SelectedItem != null && SelectedItem.ID != 0);
        }
        private void Modify()
        {
            if (PokemonBusinessLayer.BusinessManager.Instance.UpdateTournoi(SelectedItem.Tournoi))
            {
                System.Windows.Forms.MessageBox.Show("Modification du tournoi effectuée", "Succeed");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Error lors de la modification du tournoi", "Failed");
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
                if (PokemonBusinessLayer.BusinessManager.Instance.DeleteTournoi(SelectedItem.Tournoi))
                {
                    Tournois.Remove(SelectedItem);
                    System.Windows.Forms.MessageBox.Show("Supression du tournoi effectuée", "Succeed");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Error lors de la supression du tournoi", "Failed");
                }
                SelectedItem = new TournoiViewModel();
            }
        }
    }
}
