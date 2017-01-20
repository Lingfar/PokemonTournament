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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PokemonTournamentWPF
{
    /// <summary>
    /// Interaction logic for Pokemon.xaml
    /// </summary>
    public partial class PokemonControl : UserControl
    {
        public event EventHandler ButtonDeleteClick;
        public Pokemon Pokemon { get; set; }

        public PokemonControl()
        {
            InitializeComponent();
        }

        public PokemonControl(Pokemon pokemon)
        {
            InitializeComponent();
            Pokemon = pokemon;
            DataContext = Pokemon;
            ManageTextBlockBinding(tbId, "ID");
            ManageTextBlockBinding(tbName, "Nom");
            ManageTextBlockBinding(tbType, "Type");
            ManageTextBlockBinding(tbCarac, "Caracteristiques");
        }

        private void ManageTextBlockBinding(TextBlock tb, string propertyPath)
        {
            Binding tbBinding = new Binding();
            tbBinding.Path = new PropertyPath(propertyPath);
            tb.SetBinding(TextBlock.TextProperty, tbBinding);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ButtonDeleteClick?.Invoke(this, e);
        }
    }
}
