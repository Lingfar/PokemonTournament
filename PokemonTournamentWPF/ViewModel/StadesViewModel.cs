using PokemonTournamentEntities;
using PokemonTournamentWPF.Forms;
using System;
using System.Collections.Generic;
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

        private List<StadeViewModel> stades;
        public List<StadeViewModel> Stades
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
            Stades = new List<StadeViewModel>();
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
            return (SelectedItem == null);
        }
        private void Add()
        {

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
                SelectedItem = null;
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
            return (SelectedItem != null);
        }
        private void Remove()
        {
            if (SelectedItem != null)
                Stades.Remove(SelectedItem);
        }
    }
}
