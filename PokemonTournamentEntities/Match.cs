using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonTournamentEntities
{
    public class Match : EntityObject
    {
        public int IdPokemonVainqueur { get; set; }
        public EPhaseTournoi PhaseTournoi { get; set; }
        public Pokemon Pokemon1 { get; set; }
        public Pokemon Pokemon2 { get; set; }
        public Stade Stade { get; set; }

        public Match(int id) : base(id)
        {

        }

        public override string ToString()
        {
            return "Match : Id = " + base.ToString() + " IdPokemonVainqueur = " + IdPokemonVainqueur + " PhaseTournoi = " + PhaseTournoi
                + " Pokemon1 = " + Pokemon1 + " Pokemon2 = " + Pokemon2 + " Stade = " + Stade;
        }
    }
}
