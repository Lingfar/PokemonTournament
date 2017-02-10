using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PokemonBusinessLayer;
using PokemonTournamentEntities;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Win32;
using PokemonTournamentWPF.ViewModel;
using PokemonTournamentWPF.View;

namespace PokemonTournamentWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusinessManager businessManager { get; set; }

        private PokemonsViewModel pokemonsViewModel { get; set; }
        private StadesViewModel stadesViewModel { get; set; }
        private MatchesViewModel matchesViewModel { get; set; }
        private CaracteristiquesViewModel caracteristiquesViewModel { get; set; }
        private TournoisViewModel tournoisViewModel { get; set; }
        private PokemonsViewModel bonusViewModel { get; set; }
        private PlayViewModel playViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            businessManager = BusinessManager.Instance;

            LoadAllViewModels();
            
            contentControl.Content = new TournoisView();
            contentControl.DataContext = new TournoisViewModel(businessManager.GetAllTournois());
        }

        private void LoadAllViewModels()
        {
            pokemonsViewModel = new PokemonsViewModel(businessManager.GetAllPokemons());
            stadesViewModel = new StadesViewModel(businessManager.GetAllStades());
            caracteristiquesViewModel = new CaracteristiquesViewModel(businessManager.GetAllCaracteristiques());
            bonusViewModel = new PokemonsViewModel(businessManager.GetAllPokemonsByType(ETypeElement.Dragon));
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Name)
            {
                case "btnTournois":
                    contentControl.Content = new TournoisView();
                    contentControl.DataContext = new TournoisViewModel(businessManager.GetAllTournois());
                    break;
                case "btnPokemons":
                    contentControl.Content = new PokemonsView();
                    contentControl.DataContext = pokemonsViewModel;
                    break;
                case "btnStades":
                    contentControl.Content = new StadesView();
                    contentControl.DataContext = stadesViewModel;
                    break;
                case "btnMatchs":
                    contentControl.Content = new MatchesView();
                    contentControl.DataContext = new MatchesViewModel(businessManager.GetAllMatchs());
                    break;
                case "btnCarac":
                    contentControl.Content = new CaracteristiquesView();
                    contentControl.DataContext = caracteristiquesViewModel;
                    break;
                case "btnBonus":
                    contentControl.Content = new PokemonsView();
                    contentControl.DataContext = bonusViewModel;
                    break;
                case "btnPlay":
                    contentControl.Content = new PlayView();
                    contentControl.DataContext = playViewModel;
                    break;
            }
        }

    }
}
