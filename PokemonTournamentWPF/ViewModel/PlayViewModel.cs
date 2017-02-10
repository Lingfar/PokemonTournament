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
        }

        public string other_hp
        {
            get
            {
                return Play.other_hp.ToString();
            }
        }
        public string self_hp
        {
            get
            {
                return Play.self_hp.ToString();
            }
        }

        public bool self_changed
        {
            get
            {
                return self_last_hp == Play.self_hp;
            }
        }
        public bool other_changed
        {
            get
            {
                return other_last_hp == Play.other_hp;
            }
        }

        private int self_last_hp { get; set; }
        private int other_last_hp { get; set; }

        public PlayViewModel()
        {
            _play = new Partie();
            self_last_hp = Play.self_hp;
            other_last_hp = Play.other_hp;
        }
        public void think(int attpos, int defpos)
        {
            self_last_hp = Play.self_hp;
            other_last_hp = Play.other_hp;

            //Console.WriteLine(attpos + " " + defpos);
            _play.think(attpos, defpos);

            OnPropertyChanged("self_hp");
            OnPropertyChanged("other_hp");
            OnPropertyChanged("self_changed");
            OnPropertyChanged("other_changed");

            TestFinPartie();
        }

        private void TestFinPartie()
        {
            if (Play.other_hp == 0 && Play.self_hp == 0)
            {
                System.Windows.Forms.MessageBox.Show("Égalité !", "Égalité");
                _play = new Partie();
            }
            else if (Play.other_hp == 0)
            {
                System.Windows.Forms.MessageBox.Show("Vous avez gagné !", "Gagné");
                _play = new Partie();
            }
            else if (Play.self_hp == 0)
            {
                System.Windows.Forms.MessageBox.Show("Vous avez perdu !", "Perdu");
                _play = new Partie();
            }
            OnPropertyChanged("self_hp");
            OnPropertyChanged("other_hp");
            OnPropertyChanged("self_changed");
            OnPropertyChanged("other_changed");
        }
    }
}
