using PokemonTournamentEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonDataAccessLayer
{
    interface IDal
    {
        List<Pokemon> GetAllPokemons();

        Pokemon GetPokemon(DataRow item);

        Caracteristique GetCaracteristiqueById(int id);

        Caracteristique GetCaracteristique(DataRow item);
    }
}
