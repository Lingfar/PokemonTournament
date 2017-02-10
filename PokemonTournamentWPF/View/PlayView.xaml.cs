using PokemonTournamentEntities;
using PokemonTournamentWPF.ViewModel;
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
    /// Logique d'interaction pour PlayView.xaml
    /// </summary>
    public partial class PlayView : UserControl
    {

        private int posDef { get; set; }
        private int posAtt { get; set; }
        private PlayViewModel log { get; set; }

        public PlayView()
        {
            log = new PlayViewModel();
            DataContext = log;
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Name)
            {
                case "att_left":
                    posAtt = 0;
                    break;
                case "att_midl":
                    posAtt = 1;
                    break;
                case "att_righ":
                    posAtt = 2;
                    break;
                case "def_left":
                    posDef = 0;
                    break;
                case "def_midl":
                    posDef = 1;
                    break;
                case "def_righ":
                    posDef = 2;
                    break;
                case "valider":
                    log.think(posAtt,posDef);
                    break;
            }
        }
    }
}
