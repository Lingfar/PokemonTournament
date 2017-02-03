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
    public class StadesViewModel : ViewModelBase
    {
        private StadeViewModel selectedItem;
        public StadeViewModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        private ObservableCollection<StadeViewModel> stades;
        public ObservableCollection<StadeViewModel> Stades
        {
            get { return stades; }
            private set
            {
                stades = value;
                OnPropertyChanged("Stades");
            }
        }

        public StadesViewModel(List<Stade> stadesModels)
        {
            SelectedItem = new StadeViewModel();
            Stades = new ObservableCollection<StadeViewModel>();
            foreach (Stade stade in stadesModels)
                Stades.Add(new StadeViewModel(stade));
        }

        // Commande Add
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
            if (SelectedItem != null)
            {
                if(PokemonBusinessLayer.BusinessManager.Instance.AddStade(SelectedItem.Stade))
                {
                    Stades.Add(SelectedItem);
                    System.Windows.Forms.MessageBox.Show("Ajout du stade réussi", "Succeed");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Error lors de l'ajout du stade", "Failed");
                }
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
            if(PokemonBusinessLayer.BusinessManager.Instance.UpdateStade(SelectedItem.Stade))
            {
                System.Windows.Forms.MessageBox.Show("Modification du stade effectuée", "Succeed");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Error lors de la modification du stade", "Failed");
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
                SelectedItem = new StadeViewModel();
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
                if (PokemonBusinessLayer.BusinessManager.Instance.DeleteStade(SelectedItem.Stade))
                {
                    Stades.Remove(SelectedItem);
                    System.Windows.Forms.MessageBox.Show("Supression du stade effectuée", "Succeed");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Error lors de la supression du stade", "Failed");
                }
                SelectedItem = new StadeViewModel();
            }
        }
    }
}
