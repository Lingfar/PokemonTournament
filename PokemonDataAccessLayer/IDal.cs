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
        List<Tournoi> GetAllTournois();
        List<Pokemon> GetAllPokemons();
        List<Pokemon> GetAllPokemonsByType(ETypeElement type);
        List<Match> GetAllMatches();
        List<Stade> GetAllStades();
        List<Caracteristique> GetAllCaracteristiques();

        bool InsertPokemon(Pokemon pokemon);
        bool InsertMatch(Match match);
        bool InsertStade(Stade stade);
        bool InsertTournoi(Tournoi tournoi);

        bool UpdateStade(Stade stade);
        bool UpdateTournoi(Tournoi tournoi);

        bool DeleteStade(Stade stade);
        bool DeleteTournoi(Tournoi tournoi);
    }
}
