using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonTournamentEntities;
namespace PokemonTournamentWPF.ViewModel
{
    class MatchModel : ViewModelBase
    {
        private Match match { get; set; }

        MatchModel(Match orig)
        {
            match = orig;
            base.OnPropertyChanged("Stade");
            base.OnPropertyChanged("Pokemon1");
            base.OnPropertyChanged("Pokemon2");
            base.OnPropertyChanged("PhaseTournoi");
            base.OnPropertyChanged("ID");
        }

        public Stade Stade
        {
            get
            {
                return match.Stade;
            }
            set
            {
                match.Stade = value;
                base.OnPropertyChanged("Stade");
            }
        }
        public Pokemon Pokemon1
        {
            get
            {
                return match.Pokemon1;
            }
            set
            {
                match.Pokemon1 = value;
                base.OnPropertyChanged("Pokemon1");
            }
        }
        public Pokemon Pokemon2
        {
            get
            {
                return match.Pokemon2;
            }
            set
            {
                match.Pokemon2 = value;
                base.OnPropertyChanged("Pokemon2");
            }
        }
        public EPhaseTournoi PhaseTournoi
        {
            get
            {
                return match.PhaseTournoi;
            }
            set
            {
                match.PhaseTournoi = value;
                base.OnPropertyChanged("PhaseTournoi");
            }
        }
        public int ID
        {
            get
            {
                return match.ID;
            }
            set
            {

            }
        }

        public override string ToString()
        {
            return match.ToString();
        }
    }
}
