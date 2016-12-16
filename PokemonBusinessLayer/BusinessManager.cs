using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;
using PokemonDataAccessLayer;

namespace PokemonBusinessLayer
{
    public sealed class BusinessManager
    {
        private static BusinessManager instance;
        private static object syncRoot = new Object();

        public static DalManager DalManager;

        private BusinessManager()
        {
            DalManager = new DalManager();
        }

        public static BusinessManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new BusinessManager();
                    }
                }

                return instance;
            }
        }

        #region Display
        public List<string> DisplayAllPokemons()
        {
            return DalManager.GetAllPokemons().Select(p => p.ToString()).ToList();
        }

        public List<string> DisplayAllPokemonsByType(ETypeElement type)
        {
            return DalManager.GetAllPokemonsByType(type).Select(p => p.ToString()).ToList();
        }

        public List<string> DisplayAllPokemonsByStats(int attaque, int pv)
        {
            return DalManager.GetAllPokemons().Where(p => p.Caracteristiques.Attaque >= attaque
                && p.Caracteristiques.PV >= pv).Select(p => p.ToString()).ToList();
        }

        public List<string> DisplayAllMatchs()
        {
            return DalManager.GetAllMatchs().Select(m => m.ToString()).ToList();
        }

        public List<string> DisplayMatchsByPlaces(int nbPlaces)
        {
            return DalManager.GetAllMatchs().Where(m => m.Stade.NbPlaces >= nbPlaces).Select(m => m.ToString()).ToList();
        }

        public List<string> DisplayAllStades()
        {
            return DalManager.GetAllStades().Select(s => s.ToString()).ToList();
        }

        public List<string> DisplayAllCaracteristiques()
        {
            return DalManager.GetAllCaracteristiques().Select(c => c.ToString()).ToList();
        }

        public string DisplayWinner()
        {
            return DalManager.GetWinner().ToString();
        }
        #endregion

        public void RunTournament()
        {
            List<Pokemon> pokemons = new List<Pokemon>();
            pokemons.AddRange(DalManager.GetAllPokemons());
            for (int i = 0; i < 5; i++)
            {
                RunPhaseOfTournament(pokemons, (EPhaseTournoi)i);
            }
        }

        private List<Pokemon> RunPhaseOfTournament(List<Pokemon> pokemons, EPhaseTournoi phase)
        {
            for (int i = pokemons.Count - 1; i >= 0; i -= 2)
            {
                Match match;
                if(FasterPokemon(pokemons[i], pokemons[i - 1]))
                    match = RunMatch(pokemons[i], pokemons[i - 1], phase);
                else
                    match = RunMatch(pokemons[i - 1], pokemons[i], phase);
                DalManager.AddMatchToList(match);

                if (pokemons[i].ID == match.IdPokemonVainqueur)
                    pokemons.RemoveAt(i - 1);
                else
                    pokemons.RemoveAt(i);
            }

            return pokemons;
        }

        private Match RunMatch(Pokemon pokemon1, Pokemon pokemon2, EPhaseTournoi phase)
        {
            Caracteristiques oldCaracP1 = new Caracteristiques(pokemon1.Caracteristiques);
            Caracteristiques oldCaracP2 = new Caracteristiques(pokemon2.Caracteristiques);

            Match match = new Match(DalManager.LastId, phase, pokemon1, pokemon2);
            DalManager.LastId++;
            match.Stade = DalManager.GetAllStades()[Rand.rand.Next(0, 6)];

            BuffNerfPokemonByStade(pokemon1, match.Stade);
            BuffNerfPokemonByStade(pokemon2, match.Stade);
            
            decimal multiplicatorP1 = GetMultiplicatorBetweenType(pokemon1.Type, pokemon2.Type);
            decimal multiplicatorP2 = GetMultiplicatorBetweenType(pokemon2.Type, pokemon1.Type);
            while (pokemon1.Caracteristiques.PV > 0 && pokemon2.Caracteristiques.PV > 0)
            {
                pokemon2.Caracteristiques.PV -= (int)Math.Ceiling(multiplicatorP1 * (decimal)pokemon1.Caracteristiques.Attaque / (decimal)pokemon2.Caracteristiques.Defense * 4m);
                pokemon1.Caracteristiques.PV -= (int)Math.Ceiling(multiplicatorP2 * (decimal)pokemon2.Caracteristiques.Attaque / (decimal)pokemon1.Caracteristiques.Defense * 4m);
            }

            if (pokemon1.Caracteristiques.PV <= 0 && pokemon2.Caracteristiques.PV <= 0)
                match.IdPokemonVainqueur = pokemon1.ID;
            else if (pokemon1.Caracteristiques.PV <= 0)
                match.IdPokemonVainqueur = pokemon2.ID;
            else
                match.IdPokemonVainqueur = pokemon1.ID;

            pokemon1.Caracteristiques = oldCaracP1;
            pokemon2.Caracteristiques = oldCaracP2;

            return match;
        }

        private decimal GetMultiplicatorBetweenType(ETypeElement type1, ETypeElement type2)
        {
            decimal multiplicator = 1m;
            if (Pokemon.TableFaiblesses[(int)type1, (int)type2] == -1)            
                multiplicator = 0.5m;            
            else if (Pokemon.TableFaiblesses[(int)type1, (int)type2] == 1)
                multiplicator = 2m;
            return multiplicator;
        }

        private void BuffNerfPokemonByStade(Pokemon pokemon, Stade stade)
        {
            if (pokemon.Type == stade.Type)
            {
                pokemon.Caracteristiques.Attaque += stade.Caracteristiques.Attaque;
                pokemon.Caracteristiques.Defense += stade.Caracteristiques.Defense;
            }
            else if(GetMultiplicatorBetweenType(pokemon.Type, stade.Type) == 0.5m)
            {
                pokemon.Caracteristiques.Attaque -= stade.Caracteristiques.Attaque;
                pokemon.Caracteristiques.Defense -= stade.Caracteristiques.Defense;
            }
        }

        private bool FasterPokemon(Pokemon pokemon1, Pokemon pokemon2)
        {
            bool firstPokemon1;
            if (pokemon1.Caracteristiques.Vitesse > pokemon2.Caracteristiques.Vitesse)
                firstPokemon1 = true;
            else if (pokemon1.Caracteristiques.Vitesse == pokemon2.Caracteristiques.Vitesse)
            {
                if (Rand.rand.Next(0, 1) > 0.5)
                    firstPokemon1 = true;
                else
                    firstPokemon1 = false;
            }
            else
                firstPokemon1 = false;

            return firstPokemon1;
        }
    }
}