using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonTournamentEntities;
namespace PokemonTournamentWPF.ViewModel
{
    public class MatchViewModel : ViewModelBase
    {
        private Match match;
        public Match Match
        {
            get { return match; }
            set { match = value; }
        }

        public MatchViewModel(Match matchModel)
        {
            Match = matchModel;
            if (Match.IdPokemonVainqueur == Pokemon1.ID)
                PokemonVainqueur = Pokemon1;
            else
                PokemonVainqueur = Pokemon2;
            base.OnPropertyChanged("Stade");
            base.OnPropertyChanged("Pokemon1");
            base.OnPropertyChanged("Pokemon2");
            base.OnPropertyChanged("PhaseTournoi");
            base.OnPropertyChanged("ID");
        }

        public int ID
        {
            get { return match.ID; }
            set
            {
                if (value == match.ID) return;
                match.ID = value;
                base.OnPropertyChanged("ID");
            }
        }

        public int IdPokemonVainqueur
        {
            get { return match.IdPokemonVainqueur; }
            set
            {
                if (value == match.IdPokemonVainqueur) return;
                match.IdPokemonVainqueur = value;                
                base.OnPropertyChanged("IdPokemonVainqueur");
            }
        }

        public Tournoi Tournoi
        {
            get { return match.Tournoi; }
            set
            {
                if (value == match.Tournoi) return;
                match.Tournoi = value;
                base.OnPropertyChanged("Tournoi");
            }
        }

        public Pokemon PokemonVainqueur { get; set; }

        public Stade Stade
        {
            get { return match.Stade; }
            set
            {
                if (value == match.Stade) return;
                match.Stade = value;
                base.OnPropertyChanged("Stade");
            }
        }

        public Pokemon Pokemon1
        {
            get { return match.Pokemon1; }
            set
            {
                if (value == match.Pokemon1) return;
                match.Pokemon1 = value;
                base.OnPropertyChanged("Pokemon1");
            }
        }

        public Pokemon Pokemon2
        {
            get { return match.Pokemon2; }
            set
            {
                if (value == match.Pokemon2) return;
                match.Pokemon2 = value;
                base.OnPropertyChanged("Pokemon2");
            }
        }

        public EPhaseTournoi PhaseTournoi
        {
            get { return match.PhaseTournoi; }
            set
            {
                if (value == match.PhaseTournoi) return;
                match.PhaseTournoi = value;
                base.OnPropertyChanged("PhaseTournoi");
            }
        }

        public override string ToString()
        {
            return match.ToString();
        }
    }
}
