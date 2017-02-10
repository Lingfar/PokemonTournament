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
        private Partie _play;
        public Partie Play
        {
            get
            {
                return _play;
            }
            set
            {
                // Rien à faire
            }

        }

        public string other_hp
        {
            get
            {
                return Play.other_hp.ToString();
            }
            set
            {
                // Rien à faire
            }
        }
        public string self_hp
        {
            get
            {
                return Play.self_hp.ToString();
            }
            set
            {
                // Rien à faire
            }
        }

        public bool self_changed
        {
            get
            {
                return self_last_hp == Play.self_hp;
            }
            set
            {
                // Pas de sens
            }
        }
        public bool other_changed
        {
            get
            {
                return other_last_hp == Play.other_hp;
            }
            set
            {
                // Pas de sens
            }
        }

        private int self_last_hp { get; set; }
        private int other_last_hp { get; set; }

        public PlayViewModel ()
        {
            _play = new Partie();
            self_last_hp = Play.self_hp;
            other_last_hp = Play.other_hp;
        }
        public void think ( int attpos, int defpos )
        {
            self_last_hp = Play.self_hp;
            other_last_hp = Play.other_hp;

            Console.WriteLine(attpos + " " + defpos);
            _play.think(attpos, defpos);

            OnPropertyChanged("self_pv");
            OnPropertyChanged("other_pv");
            OnPropertyChanged("self_changed");
            OnPropertyChanged("other_changed");
        }

    }
}
