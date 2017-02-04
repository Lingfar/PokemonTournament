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
        public Pokemon Vainqueur { get; set; }
        public List<Match> Matches { get; set; }
        public List<Pokemon> Pokemons { get; set; }
        private List<Pokemon> PokemonsRestants { get; set; }
        public List<Stade> Stades { get; set; }
        private Random rng { get; set; }

        public Tournoi()
        {
            Matches = new List<Match>();
            Pokemons = new List<Pokemon>();
            PokemonsRestants = new List<Pokemon>();
            Stades = new List<Stade>();
            rng = new Random();
        }

        public Tournoi(string nom)
        {
            Nom = nom;
            Matches = new List<Match>();
            Pokemons = new List<Pokemon>();
            PokemonsRestants = new List<Pokemon>();
            Stades = new List<Stade>();
            rng = new Random();
        }

        public void SetPokemonsAndStades(List<Pokemon> allPokemons, List<Stade> allStades)
        {
            int i = 0;
            while(i < 32)
            {
                Pokemon poke = allPokemons[rng.Next(0, allPokemons.Count)];
                if(!Pokemons.Contains(poke))
                {
                    Pokemons.Add(poke);
                    i++;
                }
            }

            int nbStades = rng.Next(6, 13);
            i = 0;

            while(i < nbStades)
            {
                Stade stade = allStades[rng.Next(0, allStades.Count)];
                if (!Stades.Contains(stade))
                {
                    Stades.Add(stade);
                    i++;
                }
            }
        }

        public void Run()
        {
            PokemonsRestants.AddRange(Pokemons);
            for (int i = 0; i < 5; i++)
            {
                RunPhaseOfTournament((EPhaseTournoi)i);
            }
            Vainqueur = PokemonsRestants.First();
        }

        public void Run(List<Pokemon> allPokemons, List<Stade> allStades)
        {
            Pokemons = allPokemons;
            Stades = allStades;
            PokemonsRestants.AddRange(allPokemons);
            for (int i = 0; i < 5; i++)
            {
                RunPhaseOfTournament((EPhaseTournoi)i);
            }
            Vainqueur = PokemonsRestants.First();
        }

        private void RunPhaseOfTournament(EPhaseTournoi phase)
        {
            for (int i = PokemonsRestants.Count - 1; i >= 0; i -= 2)
            {
                Match match;
                if (FastestPokemon(PokemonsRestants[i], PokemonsRestants[i - 1]))
                    match = RunMatch(PokemonsRestants[i], PokemonsRestants[i - 1], phase);
                else
                    match = RunMatch(PokemonsRestants[i - 1], PokemonsRestants[i], phase);

                Matches.Add(match);           

                if (PokemonsRestants[i].ID == match.IdPokemonVainqueur)
                    PokemonsRestants.RemoveAt(i - 1);
                else
                    PokemonsRestants.RemoveAt(i);
            }            
        }

        private Match RunMatch(Pokemon pokemon1, Pokemon pokemon2, EPhaseTournoi phase)
        {
            Caracteristique newCaracP1 = new Caracteristique(pokemon1.Caracteristiques);
            Caracteristique newCaracP2 = new Caracteristique(pokemon2.Caracteristiques);

            Match match = new Match(this, phase, pokemon1, pokemon2);
            
            match.Stade = Stades[rng.Next(0, Stades.Count)];

            BuffNerfPokemonByStade(pokemon1.Type, newCaracP1, match.Stade);
            BuffNerfPokemonByStade(pokemon2.Type, newCaracP2, match.Stade);

            decimal multiplicatorP1 = GetMultiplicatorBetweenType(pokemon1.Type, pokemon2.Type);
            decimal multiplicatorP2 = GetMultiplicatorBetweenType(pokemon2.Type, pokemon1.Type);
            while (newCaracP1.PV > 0 && newCaracP2.PV > 0)
            {
                if (!EsquiveOrNot(pokemon2))
                    newCaracP2.PV -= (int)Math.Ceiling(multiplicatorP1 * (decimal)newCaracP1.Attaque / (decimal)newCaracP2.Defense * 4m);
                if (!EsquiveOrNot(pokemon1))
                    newCaracP1.PV -= (int)Math.Ceiling(multiplicatorP2 * (decimal)newCaracP2.Attaque / (decimal)newCaracP1.Defense * 4m);
            }

            if (newCaracP1.PV <= 0 && newCaracP2.PV <= 0)
                match.IdPokemonVainqueur = pokemon1.ID;
            else if (newCaracP1.PV <= 0)
                match.IdPokemonVainqueur = pokemon2.ID;
            else
                match.IdPokemonVainqueur = pokemon1.ID;

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

        private void BuffNerfPokemonByStade(ETypeElement type, Caracteristique carac, Stade stade)
        {
            if (type == stade.Type)
            {
                carac.Attaque += stade.Attaque;
                carac.Defense += stade.Defense;
            }
            else if (GetMultiplicatorBetweenType(type, stade.Type) == 0.5m)
            {
                carac.Attaque -= stade.Attaque;
                carac.Defense -= stade.Defense;
            }
        }

        private bool FastestPokemon(Pokemon pokemon1, Pokemon pokemon2)
        {
            bool firstPokemon1 = true;
            if (pokemon1.Caracteristiques.Vitesse > pokemon2.Caracteristiques.Vitesse)
                firstPokemon1 = true;
            else if (pokemon1.Caracteristiques.Vitesse == pokemon2.Caracteristiques.Vitesse)
            {
                if (rng.Next(0, 1) > 0.5)
                    firstPokemon1 = true;
                else
                    firstPokemon1 = false;
            }
            else
                firstPokemon1 = false;

            return firstPokemon1;
        }

        private bool EsquiveOrNot(Pokemon pokemon1)
        {
            bool esquive = true;
            if (rng.Next(0, 101) > pokemon1.Caracteristiques.Esquive)
                esquive = false;
            return esquive;
        }
    }
}
