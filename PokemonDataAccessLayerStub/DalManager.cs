using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;

namespace PokemonDataAccessLayerStub
{
    public class DalManager
    {
        private static DalManager instance;
        private static object syncRoot = new Object();

        private List<Pokemon> allPokemons { get; set; }
        private List<Match> allMatchs { get; set; }
        private List<Stade> allStades { get; set; }
        private List<Caracteristique> allCaracteristiques { get; set; }

        private static List<Utilisateur> allUtilisateurs = new List<Utilisateur>
            { new Utilisateur {Login = "Lingfar", Nom = "Rubin", Prenom = "Gaëtan", Password = "azertyuiop" } };
        public static int LastId = 1;

        private DalManager()
        {
            allPokemons = new List<Pokemon>();
            allMatchs = new List<Match>();
            allStades = new List<Stade>();
            allCaracteristiques = new List<Caracteristique>();

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

        public static DalManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DalManager();
                    }
                }
                return instance;
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

        public List<Caracteristique> GetAllCaracteristiques()
        {
            return allCaracteristiques;
        }

        public void AddNewStade(Stade stade)
        {
            stade.ID = LastId++;         
            allStades.Add(stade);
        }

        public void DeleteNewStade(Stade stade)
        {
            int index = allStades.FindIndex(s => s.ID == stade.ID);
            allStades.RemoveAt(index);
        }


        public static Utilisateur GetUtilisateurByLogin(string login)
        {
            return allUtilisateurs.Find(u => u.Login.ToLower() == login.ToLower());
        }
    }
}
