using PokemonTournamentEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonDataAccessLayer
{
    public interface IDal
    {
        List<Pokemon> GetAllPokemons();

        List<Match> GetAllMatches();

        bool InsertPokemon(Pokemon pokemon);

        bool InsertMatch(Match match);
    }
}
