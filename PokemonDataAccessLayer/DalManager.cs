using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;

namespace PokemonDataAccessLayer
{
    public class DalManager
    {
        private List<Pokemon> allPokemons { get; set; }
        private List<Match> allMatchs { get; set; }
        private List<Stade> allStades { get; set; }
        private List<Caracteristiques> allCaracteristiques { get; set; }

        private static List<Utilisateur> allUtilisateurs = new List<Utilisateur>
            { new Utilisateur {Login = "Lingfar", Nom = "Rubin", Prenom = "Gaëtan", Password = "azertyuiop" } };
        public static int LastId = 0;

        public DalManager()
        {
            allPokemons = new List<Pokemon>();
            allMatchs = new List<Match>();
            allStades = new List<Stade>();
            allCaracteristiques = new List<Caracteristiques>();

            //32 pokemons à générer (avec type + caracteristiques random) pour les seiziemes
            for(int i = 0; i < 32; i++)
            {
                Pokemon poke = Rand.GeneratePokemon(LastId);
                allPokemons.Add(poke);
                allCaracteristiques.Add(poke.Caracteristiques);
                LastId++;
            }

            //1 stade par type
            for (int i = 0; i < 6; i++)
            {
                allStades.Add(Rand.GenerateStade(LastId, (ETypeElement)i));
                LastId++;
            }
        }

        public void AddMatchToList(Match m)
        {
            allMatchs.Add(m);
        }

        public Pokemon GetWinner()
        {
            return GetAllPokemons()[allMatchs.Last().IdPokemonVainqueur];
        }

        public List<Pokemon> GetAllPokemons()
        {
            return allPokemons;
        }

        public List<Pokemon> GetAllPokemonsByType(ETypeElement type)
        {
            return GetAllPokemons().FindAll(p => p.Type == type);
        }

        public List<Match> GetAllMatchs()
        {
            return allMatchs;
        }

        public List<Stade> GetAllStades()
        {
            return allStades;
        }

        public List<Caracteristiques> GetAllCaracteristiques()
        {
            return allCaracteristiques;
        }

        public static Utilisateur GetUtilisateurByLogin(string login)
        {
            return allUtilisateurs.Find(u => u.Login.ToLower() == login.ToLower());
        }
    }
}
