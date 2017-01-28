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
        public List<Pokemon> AllPokemons { get; set; }
        public List<Pokemon> Pokemons { get; set; }
        public List<Stade> Stades { get; set; }
        private Random rng { get; set; }

        public Tournoi(string nom)
        {
            Nom = nom;
            Matchs = new List<Match>();
            AllPokemons = new List<Pokemon>();
            Pokemons = new List<Pokemon>();
            Stades = new List<Stade>();
            rng = new Random(5);
        }

        public void Run(List<Pokemon> allPokemons)
        {
            AllPokemons = allPokemons;
            Pokemons.AddRange(allPokemons);
            for (int i = 0; i < 5; i++)
            {
                RunPhaseOfTournament((EPhaseTournoi)i);
            }
        }

        private void RunPhaseOfTournament(EPhaseTournoi phase)
        {
            for (int i = Pokemons.Count - 1; i >= 0; i -= 2)
            {
                Match match;
                if (FastestPokemon(Pokemons[i], Pokemons[i - 1]))
                    match = RunMatch(Pokemons[i], Pokemons[i - 1], phase);
                else
                    match = RunMatch(Pokemons[i - 1], Pokemons[i], phase);

                Matchs.Add(match);           

                if (Pokemons[i].ID == match.IdPokemonVainqueur)
                    Pokemons.RemoveAt(i - 1);
                else
                    Pokemons.RemoveAt(i);
            }            
        }

        private Match RunMatch(Pokemon pokemon1, Pokemon pokemon2, EPhaseTournoi phase)
        {
            Caracteristique newCaracP1 = new Caracteristique(pokemon1.Caracteristiques);
            Caracteristique newCaracP2 = new Caracteristique(pokemon2.Caracteristiques);

            Match match = new Match(phase, pokemon1, pokemon2);
            match.Stade = new Stade();

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
                carac.Attaque += stade.Caracteristiques.Attaque;
                carac.Defense += stade.Caracteristiques.Defense;
            }
            else if (GetMultiplicatorBetweenType(type, stade.Type) == 0.5m)
            {
                carac.Attaque -= stade.Caracteristiques.Attaque;
                carac.Defense -= stade.Caracteristiques.Defense;
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
