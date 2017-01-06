using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentEntities
{
    public enum EPhaseTournoi
    {
        SeiziemeFinale, HuitimeFinale, QuartFinale, DemiFinale, Finale
    }

    public class Tournoi : EntityObject
    {
        public string Nom { get; set; }
        public List<Match> Matchs { get; set; }

        public Tournoi(int id) : base(id)
        {

        }
    }
}
