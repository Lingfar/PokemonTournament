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
using PokemonTournamentWPF.View;
using PokemonTournamentEntities;
using PokemonTournamentWPF.ViewModel;

namespace PokemonTournamentWPF.Forms
{
    /// <summary>
    /// Interaction logic for StadeViewer.xaml
    /// </summary>
    public partial class StadeViewer : Window
    {
        private Stade stade { get; set; }

        public StadeViewer()
        {
            InitializeComponent();
        }

        public StadeViewer(Stade stade)
        {
            InitializeComponent();
            if (stade != null)
                this.stade = stade;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StadeViewModel stadeViewModel;
            if (stade != null)
                stadeViewModel = new StadeViewModel(stade);
            else
                stadeViewModel = new StadeViewModel();

            stadeViewModel.CloseNotified += CloseNotified;
            ucStadeView.DataContext = stadeViewModel;
        }

        private void CloseNotified(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
