using PokemonTournamentEntities;
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
using System.Windows.Shapes;

namespace PokemonTournamentWPF
{
    /// <summary>
    /// Interaction logic for ModifStade.xaml
    /// </summary>
    public partial class ModifStade : Window
    {
        private MainWindow mainWindow;
        public MainWindow MainWindow
        {
            get { return mainWindow; }
            set
            {
                mainWindow = value;
                mainWindow.otherWindowsOpened = true;
            }
        }

        private Stade stade { get; set; }

        public ModifStade(MainWindow mainWindow)
        {
            InitializeComponent();
            InitListType();
            MainWindow = mainWindow;
        }

        public ModifStade(MainWindow mainWindow, Stade stade)
        {
            InitializeComponent();
            InitListType();
            MainWindow = mainWindow;
            this.stade = stade;
            cbType.SelectedIndex = (int)this.stade.Type;
            txtName.Text = this.stade.Nom;
            txtNbPlaces.Text = this.stade.NbPlaces.ToString();
        }

        private void InitListType()
        {
            cbType.ItemsSource = Enum.GetNames(typeof(ETypeElement));
            cbType.SelectedIndex = 0;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.otherWindowsOpened = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
