using PokemonTournamentEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Serialization;

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
                if (SelectedItem != null)
                {
                    if (PokemonBusinessLayer.BusinessManager.Instance.AddPokemon(SelectedItem.poke))
                    {
                        Pokemons.Add(SelectedItem);
                        System.Windows.Forms.MessageBox.Show("Ajout du pokemon réussi", "Succeed");
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Error lors de l'ajout du pokemon", "Failed");
                    }
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
            if (PokemonBusinessLayer.BusinessManager.Instance.UpdatePokemon(SelectedItem.poke))
            {
                System.Windows.Forms.MessageBox.Show("Modification du pokemon effectuée", "Succeed");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Error lors de la modification du pokemon", "Failed");
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
            if (PokemonBusinessLayer.BusinessManager.Instance.DeletePokemon(SelectedItem.poke))
            {
                Pokemons.Remove(SelectedItem);
                System.Windows.Forms.MessageBox.Show("Supression du pokémon effectuée", "Succeed");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Error lors de la supression du pokemon", "Failed");
            }
            SelectedItem = new PokemonViewModel();
        }


        // Commande Export
        private RelayCommand exportCommand;
        public ICommand ExportCommand
        {
            get
            {
                if (exportCommand == null)
                {
                    exportCommand = new RelayCommand(
                        () => this.Export(),
                        () => this.CanExport()
                        );
                }
                return exportCommand;
            }
        }
        private bool CanExport()
        {
            return true;
        }
        private void Export()
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Pokemon>));
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "Pokemons";
            save.DefaultExt = ".xml";
            save.Filter = "XML-File | *.xml";
            DialogResult result = save.ShowDialog();
            List<Pokemon> tmp_list = new List<Pokemon>();
            foreach ( PokemonViewModel a in Pokemons )
            {
                tmp_list.Add(a.poke);
            }
            if ((result == DialogResult.Yes) || (result == DialogResult.OK))
            {
                using (FileStream fs = new FileStream(save.FileName, FileMode.Create))
                {
                    ser.Serialize(fs, tmp_list);
                }
            }
        }

        #endregion
    }
}
