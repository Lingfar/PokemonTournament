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
        public event EventHandler<EventArgs> CloseNotified;
        protected void OnCloseNotified(EventArgs e)
        {
            this.CloseNotified(this, e);
        }

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
                Stades.Add(SelectedItem);
                PokemonBusinessLayer.BusinessManager.Instance.AddNewStade(SelectedItem.Stade);
            }
        }

        // Commande Remove
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
                PokemonBusinessLayer.BusinessManager.Instance.DeleteStade(SelectedItem.Stade);
                Stades.Remove(SelectedItem);
                SelectedItem = new StadeViewModel();
            }
        }
    }
}
