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
    public class PokemonsViewModel : ViewModelBase
    {
        // Model encapsulé dans le ViewModel
        private ObservableCollection<PokemonViewModel> _pokemons;

        public ObservableCollection<PokemonViewModel> Pokemons
        {
            get { return _pokemons; }
            private set
            {
                _pokemons = value;
                OnPropertyChanged("Pokemons");
            }
        }
        
        private PokemonViewModel _selectedItem;
        public PokemonViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }

        }


        public PokemonsViewModel(IList<Pokemon> PokemonsModel)
        {
            SelectedItem = new PokemonViewModel();
            _pokemons = new ObservableCollection<PokemonViewModel>();
            foreach (Pokemon a in PokemonsModel)
            {
                _pokemons.Add(new PokemonViewModel(a));
            }
        }

        #region "Commandes du formulaire"


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
                Pokemons.Add(SelectedItem);
                PokemonBusinessLayer.BusinessManager.Instance.AddNewPokemon(SelectedItem.poke);
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
                SelectedItem = new PokemonViewModel();
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
                PokemonBusinessLayer.BusinessManager.Instance.DeletePokemon(SelectedItem.poke);
                Pokemons.Remove(SelectedItem);
                SelectedItem = new PokemonViewModel();
            }
        }
        #endregion
    }
}
