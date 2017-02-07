using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonDataAccessLayer;
using PokemonTournamentEntities;
using System.Collections.Generic;

namespace PokemonTournamentTests
{
    [TestClass]
    public class PokemonTest
    {
        [TestMethod]
        public void TestGetAllPokemons()
        {
            List<Pokemon> allPokemons = DalManager.Instance.GetAllPokemons();
            Assert.AreEqual(allPokemons.Count, 100);
        }

        [TestMethod]
        public void TestGetSpecificPokemon()
        {
            List<Pokemon> allPokemons = DalManager.Instance.GetAllPokemons();
            Pokemon pokeTest = new Pokemon();
            pokeTest.ID = 1;
            pokeTest.Nom = "Bulbizarre";
            pokeTest.Type = ETypeElement.Plante;
            pokeTest.Caracteristiques.PV = 101;
            pokeTest.Caracteristiques.Attaque = 6;
            pokeTest.Caracteristiques.Defense = 6;
            pokeTest.Caracteristiques.Vitesse = 13;
            pokeTest.Caracteristiques.Esquive = 24;
            Assert.AreEqual(pokeTest, allPokemons[0]);
        }

        [TestMethod]
        public void TestInsertPokemon()
        {
            Pokemon pokeTest = new Pokemon();
            pokeTest.Nom = "Pokemon Test";
            pokeTest.Type = ETypeElement.Insecte;

            Caracteristique caracTest = new Caracteristique();
            caracTest.PV = 150;
            caracTest.Attaque = 15;
            caracTest.Defense = 5;
            caracTest.Vitesse = 10;
            caracTest.Esquive = 40;
            pokeTest.Caracteristiques = caracTest;

            DalManager.Instance.InsertPokemon(pokeTest);

            List<Pokemon> allPokemons = DalManager.Instance.GetAllPokemons();
            Assert.AreEqual(pokeTest, allPokemons[allPokemons.Count - 1]);
        }

        [TestMethod]
        public void TestUpdatePokemon()
        {
            List<Pokemon> allPokemons = DalManager.Instance.GetAllPokemons();
            Pokemon pokeTest = DalManager.Instance.GetAllPokemons()[allPokemons.Count - 1];
            pokeTest.Nom = "Pokemon Test Update";
            pokeTest.Type = ETypeElement.Feu;
            pokeTest.Caracteristiques.PV = 179;
            pokeTest.Caracteristiques.Attaque = 12;
            pokeTest.Caracteristiques.Defense = 14;
            pokeTest.Caracteristiques.Vitesse = 2;
            pokeTest.Caracteristiques.Esquive = 30;

            DalManager.Instance.UpdatePokemon(pokeTest);

            allPokemons = DalManager.Instance.GetAllPokemons();
            Assert.AreEqual(pokeTest, allPokemons[allPokemons.Count - 1]);
        }

        [TestMethod]
        public void TestDeletePokemon()
        {
            List<Pokemon> allPokemons = DalManager.Instance.GetAllPokemons();
            int ancienneTaille = allPokemons.Count;
            DalManager.Instance.DeletePokemon(allPokemons[allPokemons.Count - 1]);
            allPokemons = DalManager.Instance.GetAllPokemons();
            Assert.AreEqual(ancienneTaille - 1, allPokemons.Count);
        }
    }
}
