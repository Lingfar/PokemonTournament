using PokemonBusinessLayer;
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

namespace PokemonTournamentWPF.View
{
    /// <summary>
    /// Interaction logic for ModifStade.xaml
    /// </summary>
    public partial class StadeView : Window
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

        public StadeView(MainWindow mainWindow)
        {
            InitializeComponent();
            InitListType();
            MainWindow = mainWindow;
            stade = new Stade();
            btnDelete.Visibility = Visibility.Collapsed;
            btnSave.SetValue(Grid.ColumnSpanProperty, 2);
        }

        public StadeView(MainWindow mainWindow, Stade stade)
        {
            InitializeComponent();
            InitListType();
            MainWindow = mainWindow;
            this.stade = stade;
            cbType.SelectedIndex = (int)this.stade.Type;
            txtName.Text = this.stade.Nom;
            txtNbPlaces.Text = this.stade.NbPlaces.ToString();
            txtAtt.Text = this.stade.Caracteristiques.Attaque.ToString();
            txtDef.Text = this.stade.Caracteristiques.Defense.ToString();
            btnDelete.Visibility = Visibility.Visible;
            btnSave.SetValue(Grid.ColumnSpanProperty, 1);
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
            stade.Nom = txtName.Text;
            stade.Type = (ETypeElement)Enum.Parse(typeof(ETypeElement), cbType.SelectedValue.ToString());
            if (txtNbPlaces.Text == "")
                stade.NbPlaces = 0;
            else
                stade.NbPlaces = Convert.ToInt32(txtNbPlaces.Text);

            stade.Caracteristiques = new Caracteristiques();
            if (txtAtt.Text != "" && txtDef.Text != "")
            {
                stade.Caracteristiques = new Caracteristiques(Convert.ToInt32(txtAtt.Text),
                    Convert.ToInt32(txtDef.Text));
            }
            else if(txtAtt.Text != "" && txtDef.Text == "")
            {
                stade.Caracteristiques.Attaque = Convert.ToInt32(txtAtt.Text);
            }
            else
            {
                stade.Caracteristiques.Defense = Convert.ToInt32(txtDef.Text);
            }
            stade.Caracteristiques = stade.Caracteristiques;
            BusinessManager businessManager = BusinessManager.Instance;
            if (stade.ID == 0)
                businessManager.AddNewStade(stade);         
            Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            BusinessManager businessManager = BusinessManager.Instance;
            businessManager.DeleteStade(stade);
            Close();
        }
    }
}
