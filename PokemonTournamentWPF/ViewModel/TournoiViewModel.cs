using PokemonTournamentEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentWPF.ViewModel
{
    public class TournoiViewModel : ViewModelBase
    {
        private Tournoi tournoi;
        public Tournoi Tournoi
        {
            get { return tournoi; }
            set { tournoi = value; }
        }

        public TournoiViewModel()
        {
            Tournoi = new Tournoi();
            MatchesViewModel = new MatchesViewModel();
        }

        public TournoiViewModel(Tournoi tournoiModel)
        {
            if (tournoiModel != null)
            {
                Tournoi = tournoiModel;
                MatchesViewModel = new MatchesViewModel(Tournoi.Matches);
            }
            else
            {
                Tournoi = new Tournoi();
                MatchesViewModel = new MatchesViewModel();
            }
        }

        public TournoiViewModel(TournoiViewModel t)
        {
            Tournoi = new Tournoi();
            Tournoi.Nom = t.Nom;
            MatchesViewModel = new MatchesViewModel();
        }

        public int ID
        {
            get { return tournoi.ID; }
            set
            {
                if (value == tournoi.ID) return;
                tournoi.ID = value;
                base.OnPropertyChanged("ID");
            }
        }

        public string Nom
        {
            get { return tournoi.Nom; }
            set
            {
                if (value == tournoi.Nom) return;
                tournoi.Nom = value;
                base.OnPropertyChanged("Nom");
            }
        }

        public Pokemon Vainqueur
        {
            get { return tournoi.Vainqueur; }
            set
            {
                if (value == tournoi.Vainqueur) return;
                tournoi.Vainqueur = value;
                base.OnPropertyChanged("Vainqueur");
            }
        }

        public List<Match> Matches
        {
            get { return tournoi.Matches; }
            set
            {
                if (value == tournoi.Matches) return;
                tournoi.Matches = value;
                base.OnPropertyChanged("Matches");
            }
        }

        public MatchesViewModel MatchesViewModel { get; set; }

        public List<Pokemon> Pokemons
        {
            get { return tournoi.Pokemons; }
            set
            {
                if (value == tournoi.Pokemons) return;
                tournoi.Pokemons = value;
                base.OnPropertyChanged("Pokemons");
            }
        }

        public List<Stade> Stades
        {
            get { return tournoi.Stades; }
            set
            {
                if (value == tournoi.Stades) return;
                tournoi.Stades = value;
                base.OnPropertyChanged("Stades");
            }
        }
    }
}
