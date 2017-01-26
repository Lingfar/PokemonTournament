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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace PokemonTournamentWPF.View
{
    /// <summary>
    /// Interaction logic for StadeView.xaml
    /// </summary>
    public partial class StadeView : UserControl
    {
        public StadeView()
        {
            InitializeComponent();
            //cbType.ItemsSource = Enum.GetNames(typeof(ETypeElement));
        }
    }



    public class IsNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value == null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("IsNullConverter can only be used OneWay.");
        }
    }
}
