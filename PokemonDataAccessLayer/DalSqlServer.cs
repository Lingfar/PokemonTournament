using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;
using System.Data.SqlClient;
using System.IO;

namespace PokemonDataAccessLayer
{
    public class DalSqlServer : IDal
    {
        //A modifier en fonction du path de la bdd

        //???
        //protected string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=?????\PokemonTournament\PokemonDataAccessLayer\PokemonTournament.mdf;Integrated Security=True";

        //Gaetan
        protected string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Programmation\C#\ZZ2\ServicesWeb\PokemonTournament\PokemonDataAccessLayer\PokemonTournament.mdf;Integrated Security=True";

        public DalSqlServer()
        {

        }

        public DataTable Select(string request)
        {
            DataTable table = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(request, sqlConnection);
                sqlConnection.Open();
                table.Load(sqlCommand.ExecuteReader());
                sqlConnection.Close();
            }
            return table;
        }

        public bool InsertPokemon(Pokemon pokemon)
        {
            bool result = false;            
            if (InsertCaracteristique(pokemon.Caracteristiques))
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        string sql = "INSERT INTO Pokemon (Nom, Type, Caracteristiques) VALUES(@Nom, @Type, @Carac);  SELECT @@IDENTITY";
                        SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                        sqlCommand.Parameters.Add("@Nom", SqlDbType.VarChar, 50).Value = pokemon.Nom;
                        sqlCommand.Parameters.Add("@Type", SqlDbType.Int).Value = (int)pokemon.Type;
                        sqlCommand.Parameters.Add("@Carac", SqlDbType.Int).Value = pokemon.Caracteristiques.ID;
                        sqlCommand.CommandType = CommandType.Text;
                        pokemon.ID = Convert.ToInt32(sqlCommand.ExecuteScalar().ToString());
                        sqlConnection.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    result = false;
                }
            }            
            return result;
        }

        public bool InsertCaracteristique(Caracteristique carac)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string sql = "INSERT INTO Caracteristique VALUES(@PV, @Attaque, @Defense, @Vitesse, @Esquive); SELECT @@IDENTITY";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.Add("@PV", SqlDbType.Int).Value = carac.PV;
                    sqlCommand.Parameters.Add("@Attaque", SqlDbType.Int).Value = carac.Attaque;
                    sqlCommand.Parameters.Add("@Defense", SqlDbType.Int).Value = carac.Defense;
                    sqlCommand.Parameters.Add("@Vitesse", SqlDbType.Int).Value = carac.Vitesse;
                    sqlCommand.Parameters.Add("@Esquive", SqlDbType.Int).Value = carac.Esquive;
                    sqlCommand.CommandType = CommandType.Text;
                    carac.ID = Convert.ToInt32(sqlCommand.ExecuteScalar().ToString());
                    sqlConnection.Close();
                    result = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = false;
            }
            return result;
        }

        public bool InsertMatch(Match match)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string sql = "INSERT INTO Match VALUES(@IdPokemonVainqueur, @PhaseTournoi, @IdPokemon1, @IdPokemon2, @IdStade); SELECT @@IDENTITY";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.Add("@IdPokemonVainqueur", SqlDbType.Int).Value = match.IdPokemonVainqueur;
                    sqlCommand.Parameters.Add("@PhaseTournoi", SqlDbType.Int).Value = (int)match.PhaseTournoi;
                    sqlCommand.Parameters.Add("@IdPokemon1", SqlDbType.Int).Value = match.Pokemon1.ID;
                    sqlCommand.Parameters.Add("@IdPokemon2", SqlDbType.Int).Value = match.Pokemon2.ID;
                    sqlCommand.Parameters.Add("@IdStade", SqlDbType.Int).Value = match.Stade.ID;
                    sqlCommand.CommandType = CommandType.Text;
                    match.ID = Convert.ToInt32(sqlCommand.ExecuteScalar().ToString());
                    sqlConnection.Close();
                    result = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = false;
            }
            return result;
        }


        public List<Pokemon> GetAllPokemons()
        {
            List<Pokemon> listPokemons = new List<Pokemon>();
            DataTable dt = Select("select * from pokemon");
            foreach (DataRow item in dt.Rows)
            {
                listPokemons.Add(GetPokemon(item));
            }
            return listPokemons;
        }

        private Pokemon GetPokemon(DataRow item)
        {
            Pokemon poke = new Pokemon();
            if (item != null)
            {
                poke.ID = Convert.ToInt32(item["Id"]);
                poke.Nom = item["Nom"].ToString();
                poke.Type = (ETypeElement)Convert.ToInt32(item["Type"]);
                poke.Caracteristiques = GetCaracteristiqueById(Convert.ToInt32(item["Caracteristiques"]));
            }
            return poke;
        }

        private Pokemon GetPokemonById(int id)
        {
            Pokemon poke = new Pokemon();
            DataTable dt = Select("select * from pokemon where id=" + id.ToString());
            if (dt.Rows.Count > 0)
            {
                poke = GetPokemon(dt.Rows[0]);
            }
            return poke;
        }

        public List<Match> GetAllMatches()
        {
            List<Match> listMatches = new List<Match>();
            DataTable dt = Select("select * from match");
            foreach (DataRow item in dt.Rows)
            {
                listMatches.Add(GetMatch(item));
            }
            return listMatches;
        }

        private Match GetMatch(DataRow item)
        {
            Match match = new Match();
            if (item != null)
            {
                match.ID = Convert.ToInt32(item["Id"]);
                match.IdPokemonVainqueur = Convert.ToInt32(item["IdPokemonVainqueur"]);
                match.Pokemon1 = GetPokemonById(Convert.ToInt32(item["Pokemon1"]));
                match.Pokemon2 = GetPokemonById(Convert.ToInt32(item["Pokemon2"]));
                match.PhaseTournoi = (EPhaseTournoi)Convert.ToInt32(item["PhaseTournoi"]);
                match.Stade = new Stade();
            }
            return match;
        }

        public Caracteristique GetCaracteristiqueById(int id)
        {
            Caracteristique carac = new Caracteristique();
            DataTable dt = Select("select * from caracteristique where id=" + id.ToString());
            if (dt.Rows.Count > 0)
            {
                carac = GetCaracteristique(dt.Rows[0]);
            }
            return carac;
        }

        private Caracteristique GetCaracteristique(DataRow item)
        {
            Caracteristique carac = new Caracteristique();
            if (item != null)
            {
                carac.ID = Convert.ToInt32(item["Id"]);
                carac.PV = Convert.ToInt32(item["PV"]);
                carac.Attaque = Convert.ToInt32(item["Attaque"]);
                carac.Defense = Convert.ToInt32(item["Defense"]);
                carac.Vitesse = Convert.ToInt32(item["Vitesse"]);
                carac.Esquive = Convert.ToInt32(item["Esquive"]);
            }
            return carac;
        }
    }
}
