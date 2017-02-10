using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;

namespace PokemonTournamentWPF.ViewModel
{
    public class PlayViewModel : ViewModelBase
    {
        public Partie Play
        {
            get
            {
                return Play;
            }
            set
            {
                // Rien à faire
            }

        }
        private int self_hp { get; set; }
        private int other_hp { get; set; }

        PlayViewModel ()
        {
            self_hp = 3;
            other_hp = 3;
        }
        void Add ( int attpos, int defpos )
        {

        }

    }
}
