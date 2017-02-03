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

        }

        public TournoiViewModel(Tournoi tournoiModel)
        {
            if (tournoiModel != null)
                Tournoi = tournoi;
            else
                Tournoi = new Tournoi();
        }

        public int ID
        {
            get {  return tournoi.ID; }
            set
            {
                if (value == tournoi.ID) return;
                tournoi.ID = value;
                base.OnPropertyChanged("ID");
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
        
        public List<Pokemon> Pokemons
        {
            get { return tournoi.AllPokemons; }
            set
            {
                if (value == tournoi.AllPokemons) return;
                tournoi.AllPokemons = value;
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
