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
        protected string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + AppDomain.CurrentDomain.BaseDirectory + "PokemonTournament.mdf;Integrated Security=True";
        
        public DalSqlServer()
        {

        }

        public Utilisateur GetUtilisateurByLogin(string login)
        {
            Utilisateur user = new Utilisateur();
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string sql = "SELECT * FROM Utilisateur WHERE Login=@Login";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.Add("@Login", SqlDbType.VarChar, 50).Value = login;
                    sqlCommand.CommandType = CommandType.Text;
                    dt.Load(sqlCommand.ExecuteReader());
                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (dt.Rows.Count > 0)
            {
                user.Login = login;
                user.Nom = dt.Rows[0]["Nom"].ToString();
                user.Prenom = dt.Rows[0]["Prenom"].ToString();
                user.Password = dt.Rows[0]["Password"].ToString();
            }
            return user;
        }
        public bool RegisterLogin(Utilisateur user)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string sql = "INSERT INTO Utilisateur VALUES(@Login, @Nom, @Prenom, @Password);";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.Add("@Login", SqlDbType.VarChar, 50).Value = user.Login;

                    if (user.Nom != null)
                        sqlCommand.Parameters.Add("@Nom", SqlDbType.VarChar, 100).Value = user.Nom;
                    else
                        sqlCommand.Parameters.Add("@Nom", SqlDbType.VarChar, 100).Value = DBNull.Value;

                    if (user.Nom != null)
                        sqlCommand.Parameters.Add("@Prenom", SqlDbType.VarChar, 100).Value = user.Prenom;
                    else
                        sqlCommand.Parameters.Add("@Prenom", SqlDbType.VarChar, 100).Value = DBNull.Value;

                    sqlCommand.Parameters.Add("@Password", SqlDbType.VarChar, 200).Value = user.Password;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.ExecuteNonQuery();
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
                        result = true;
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
        public bool InsertStade(Stade stade)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string sql = "INSERT INTO Stade VALUES(@Nom, @Type, @NbPlaces, @Attaque, @Defense);  SELECT @@IDENTITY";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.Add("@Nom", SqlDbType.VarChar, 50).Value = stade.Nom;
                    sqlCommand.Parameters.Add("@Type", SqlDbType.Int).Value = (int)stade.Type;
                    sqlCommand.Parameters.Add("@NbPlaces", SqlDbType.Int).Value = stade.NbPlaces;
                    sqlCommand.Parameters.Add("@Attaque", SqlDbType.Int).Value = stade.Attaque;
                    sqlCommand.Parameters.Add("@Defense", SqlDbType.Int).Value = stade.Defense;
                    sqlCommand.CommandType = CommandType.Text;
                    stade.ID = Convert.ToInt32(sqlCommand.ExecuteScalar().ToString());
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
                    string sql = "INSERT INTO Match VALUES(@IdTournoi, @IdPokemonVainqueur, @PhaseTournoi, @IdPokemon1, @IdPokemon2, @IdStade); SELECT @@IDENTITY";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.Add("@IdTournoi", SqlDbType.Int).Value = match.Tournoi.ID;
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
        public bool InsertTournoi(Tournoi tournoi)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string sql = "INSERT INTO Tournoi VALUES(@Nom, @IdPokemonVainqueur); SELECT @@IDENTITY";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.Add("@Nom", SqlDbType.VarChar, 50).Value = tournoi.Nom;
                    sqlCommand.Parameters.Add("@IdPokemonVainqueur", SqlDbType.Int).Value = tournoi.Vainqueur.ID;
                    sqlCommand.CommandType = CommandType.Text;
                    tournoi.ID = Convert.ToInt32(sqlCommand.ExecuteScalar().ToString());
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


        public bool UpdatePokemon(Pokemon pokemon)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string sql = "UPDATE Pokemon SET Nom=@Nom, Type=@Type WHERE ID=@Id;";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = pokemon.ID;
                    sqlCommand.Parameters.Add("@Nom", SqlDbType.VarChar, 50).Value = pokemon.Nom;
                    sqlCommand.Parameters.Add("@Type", SqlDbType.Int).Value = (int)pokemon.Type;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.ExecuteReader();
                    sqlConnection.Close();
                    result = UpdateCaracteristique(pokemon.Caracteristiques);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = false;
            }
            return result;
        }
        public bool UpdateStade(Stade stade)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string sql = "UPDATE Stade SET Nom=@Nom, Type=@Type, NbPlaces=@NbPlaces, Attaque=@Attaque, Defense=@Defense WHERE ID=@Id;";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = stade.ID;
                    sqlCommand.Parameters.Add("@Nom", SqlDbType.VarChar, 50).Value = stade.Nom;
                    sqlCommand.Parameters.Add("@Type", SqlDbType.Int).Value = (int)stade.Type;
                    sqlCommand.Parameters.Add("@NbPlaces", SqlDbType.Int).Value = stade.NbPlaces;
                    sqlCommand.Parameters.Add("@Attaque", SqlDbType.Int).Value = stade.Attaque;
                    sqlCommand.Parameters.Add("@Defense", SqlDbType.Int).Value = stade.Defense;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.ExecuteReader();
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
        public bool UpdateTournoi(Tournoi tournoi)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string sql = "UPDATE Tournoi SET Nom=@Nom WHERE ID=@Id;";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = tournoi.ID;
                    sqlCommand.Parameters.Add("@Nom", SqlDbType.VarChar, 50).Value = tournoi.Nom;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.ExecuteReader();
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
        private bool UpdateCaracteristique(Caracteristique carac)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string sql = "UPDATE Caracteristique SET PV=@PV, Attaque=@Attaque, Defense=@Defense, Vitesse=@Vitesse, Esquive=@Esquive WHERE ID=@Id;";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = carac.ID;
                    sqlCommand.Parameters.Add("@PV", SqlDbType.Int).Value = carac.PV;
                    sqlCommand.Parameters.Add("@Attaque", SqlDbType.Int).Value = carac.Attaque;
                    sqlCommand.Parameters.Add("@Defense", SqlDbType.Int).Value = carac.Defense;
                    sqlCommand.Parameters.Add("@Vitesse", SqlDbType.Int).Value = carac.Vitesse;
                    sqlCommand.Parameters.Add("@Esquive", SqlDbType.Int).Value = carac.Esquive;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.ExecuteReader();
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


        private bool Delete(string request, int id)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand(request, sqlConnection);
                    sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.ExecuteReader();
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
        public bool DeletePokemon(Pokemon pokemon)
        {
            if (Delete("DELETE FROM Pokemon WHERE ID=@Id;", pokemon.ID))
            {
                return DeleteCaracteristique(pokemon.Caracteristiques);
            }
            return false;
        }
        public bool DeleteStade(Stade stade)
        {
            return Delete("DELETE FROM Stade WHERE ID=@Id;", stade.ID);
        }
        public void DeleteMatch(Match match)
        {
            Delete("DELETE FROM Match WHERE ID=@Id;", match.ID);
        }
        public bool DeleteTournoi(Tournoi tournoi)
        {
            foreach (Match match in tournoi.Matches)
            {
                DeleteMatch(match);
            }
            return Delete("DELETE FROM Tournoi WHERE ID=@Id;", tournoi.ID);
        }
        private bool DeleteCaracteristique(Caracteristique carac)
        {
            return Delete("DELETE FROM Caracteristique WHERE ID=@Id;", carac.ID);
        }


        private DataTable Select(string request)
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

        public List<Tournoi> GetAllTournois()
        {
            List<Tournoi> listTournois = new List<Tournoi>();
            DataTable dt = Select("select * from tournoi");
            foreach (DataRow item in dt.Rows)
            {
                listTournois.Add(GetTournoi(item));
            }
            return listTournois;
        }
        private Tournoi GetTournoi(DataRow item)
        {
            Tournoi tournoi = new Tournoi();
            if (item != null)
            {
                tournoi.ID = Convert.ToInt32(item["Id"]);
                tournoi.Nom = item["Nom"].ToString();
                tournoi.Vainqueur = GetPokemonById(Convert.ToInt32(item["IdPokemonVainqueur"]));
                tournoi.Matches = GetMatchesByIdTournoi(tournoi.ID).OrderByDescending(p => p.ID).ToList();
                tournoi.Pokemons = GetPokemonsByMatches(tournoi.Matches).OrderBy(p => p.ID).ToList();
                tournoi.Stades = GetStadesByMatches(tournoi.Matches).OrderBy(p => p.ID).ToList();
            }
            return tournoi;
        }
        private Tournoi GetTournoiById(int id)
        {
            Tournoi t = new Tournoi();
            DataTable dt = Select("select * from Tournoi where id=" + id.ToString());
            if (dt.Rows.Count > 0)
            {
                t.Nom = dt.Rows[0]["Nom"].ToString();
            }
            return t;
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
        public List<Pokemon> GetAllPokemonsByType(ETypeElement type)
        {
            List<Pokemon> listPokemons = new List<Pokemon>();
            DataTable dt = Select("select * from pokemon where type=" + (int)type);
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
        private List<Pokemon> GetPokemonsByMatches(List<Match> matches)
        {
            matches = matches.OrderBy(m => m.ID).ToList();
            List<Pokemon> listPokemons = new List<Pokemon>();
            if (matches.Count > 0)
            {
                int length = matches.Count / 2 + 1;
                for (int i = 0; i < length; i++)
                {
                    DataTable dt = Select("select * from pokemon where id=" + matches[i].Pokemon1.ID.ToString()
                        + " or id=" + matches[i].Pokemon2.ID.ToString());
                    listPokemons.Add(GetPokemon(dt.Rows[0]));
                    listPokemons.Add(GetPokemon(dt.Rows[1]));
                }
            }
            return listPokemons;
        }

        public List<Stade> GetAllStades()
        {
            List<Stade> listStades = new List<Stade>();
            DataTable dt = Select("select * from Stade");
            foreach (DataRow item in dt.Rows)
            {
                listStades.Add(GetStade(item));
            }
            return listStades;
        }
        private Stade GetStade(DataRow item)
        {
            Stade stade = new Stade();
            if (item != null)
            {
                stade.ID = Convert.ToInt32(item["Id"]);
                stade.Nom = item["Nom"].ToString();
                stade.Type = (ETypeElement)Convert.ToInt32(item["Type"]);
                stade.NbPlaces = Convert.ToInt32(item["NbPlaces"]);
                stade.Attaque = Convert.ToInt32(item["Attaque"]);
                stade.Defense = Convert.ToInt32(item["Defense"]);
            }
            return stade;
        }
        private Stade GetStadeById(int id)
        {
            Stade stade = new Stade();
            DataTable dt = Select("select * from Stade where id=" + id.ToString());
            if (dt.Rows.Count > 0)
            {
                stade = GetStade(dt.Rows[0]);
            }
            return stade;
        }
        private List<Stade> GetStadesByMatches(List<Match> matches)
        {
            List<Stade> listStades = new List<Stade>();
            for (int i = 0; i < matches.Count; i++)
            {
                DataTable dt = Select("select * from Stade where id=" + matches[i].Stade.ID.ToString());
                Stade stade = GetStade(dt.Rows[0]);
                if (!listStades.Contains(stade))
                    listStades.Add(stade);
            }
            return listStades;
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
                match.Tournoi = GetTournoiById(Convert.ToInt32(item["IdTournoi"]));
                match.IdPokemonVainqueur = Convert.ToInt32(item["IdPokemonVainqueur"]);
                match.Pokemon1 = GetPokemonById(Convert.ToInt32(item["Pokemon1"]));
                match.Pokemon2 = GetPokemonById(Convert.ToInt32(item["Pokemon2"]));
                match.PhaseTournoi = (EPhaseTournoi)Convert.ToInt32(item["PhaseTournoi"]);
                match.Stade = GetStadeById(Convert.ToInt32(item["Stade"]));
            }
            return match;
        }
        private List<Match> GetMatchesByIdTournoi(int idTournoi)
        {
            List<Match> listMatches = new List<Match>();
            DataTable dt = Select("select * from match where idTournoi=" + idTournoi.ToString());
            foreach (DataRow item in dt.Rows)
            {
                listMatches.Add(GetMatch(item));
            }
            return listMatches;
        }

        public List<Caracteristique> GetAllCaracteristiques()
        {
            List<Caracteristique> listCaracteristiques = new List<Caracteristique>();
            DataTable dt = Select("select * from Caracteristique");
            foreach (DataRow item in dt.Rows)
            {
                listCaracteristiques.Add(GetCaracteristique(item));
            }
            return listCaracteristiques;
        }
        private Caracteristique GetCaracteristiqueById(int id)
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
