using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;

namespace PokemonDataAccessLayer
{
    public class Rand
    {
        public static Random rand = new Random(5);

        public static Pokemon GeneratePokemon(int id)
        {
            Pokemon poke = new Pokemon(id);
            poke.Caracteristiques = GenerateCaracteristiques();
            poke.Type = (ETypeElement)rand.Next(0, 6);
            return poke;
        }

        public static Caracteristiques GenerateCaracteristiques()
        {
            Caracteristiques carac = new Caracteristiques();
            carac.PV = rand.Next(50, 201);
            carac.Attaque = rand.Next(1, 21);
            carac.Defense = rand.Next(1, 21);
            carac.Vitesse = rand.Next(1, 21);
            return carac;
        }

        public static Stade GenerateStade(int id, ETypeElement type)
        {
            Stade stade = new Stade(id);
            stade.Type = type;

            Caracteristiques carac = new Caracteristiques();
            carac.Attaque = rand.Next(1, 6);
            carac.Defense = carac.Attaque;

            stade.Caracteristiques = carac;
            stade.NbPlaces = rand.Next(50, 400);
            return stade;
        }
    }
}
