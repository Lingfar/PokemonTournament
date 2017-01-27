using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonTournamentEntities;
using System.Data.SqlClient;

namespace PokemonDataAccessLayer
{
    public class DalSqlServer : IDal
    {
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

        public Pokemon GetPokemon(DataRow item)
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

        public Caracteristique GetCaracteristiqueById(int id)
        {
            Caracteristique carac = new Caracteristique();
            DataTable dt = Select("select * from caracteristique where id=" + id.ToString());            
            if(dt.Rows.Count > 0)
            {
                carac = GetCaracteristique(dt.Rows[0]);
            }
            return carac;
        }

        public Caracteristique GetCaracteristique(DataRow item)
        {
            Caracteristique carac = new Caracteristique();
            if(item != null)
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
