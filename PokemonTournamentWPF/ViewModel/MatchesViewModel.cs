using PokemonTournamentEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentWPF.ViewModel
{
    public class MatchesViewModel : ViewModelBase
    {
        private ObservableCollection<MatchViewModel> matches;
        public ObservableCollection<MatchViewModel> Matches
        {
            get { return matches; }
            private set
            {
                matches = value;
                OnPropertyChanged("Matches");
            }
        }

        public MatchesViewModel()
        {

        }

        public MatchesViewModel(List<Match> matchesModel)
        {
            Matches = new ObservableCollection<MatchViewModel>();
            foreach (Match match in matchesModel)
            {
                Matches.Add(new MatchViewModel(match));
            }
        }
    }
}
