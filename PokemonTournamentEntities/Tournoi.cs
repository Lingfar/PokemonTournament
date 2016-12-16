using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentEntities
{
    public enum EPhaseTournoi
    {
        QuartFinale, HuitimeFinale, DemiFinale, Finale
    }

    public class Tournoi : EntityObject
    {
        public List<Match> Matchs { get; set; }
        public string Nom { get; set; }

        public Tournoi(int id) : base(id)
        {

        }
    }
}
