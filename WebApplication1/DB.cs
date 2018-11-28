using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


using WebApplication1.DAL;
using WebApplication1.BLL;
using System.Linq;
using WebApplication1.Models;
using WebApplication1;
using System.Collections.Specialized;

namespace Tests
{
    public class DB
    {
        private SqlConnection Connect()
        {
            string connectionString = "data source=OLEGALL-ПК\\SQLEXPRESS; initial catalog=iTalk; persist security info=True; Integrated Security=SSPI;";
            //string connectionString = "Data Source=ms-sql-8.in-solve.ru;Database=1gb_city-move;User ID=1gb_city-move;Password=a4bea42fsg;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            return sqlConnection;
        }

        public IList<Client> GetClients()
        {
            IList<Client> clients = new List<Client>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM dbo.Clients";
            cmd.CommandType = CommandType.Text;
            var sqlConnection = Connect();
            cmd.Connection = sqlConnection;

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Client client = new Client();
                client.Id = (long)reader["Id"];
                client.Name = (string)reader["Name"];
                clients.Add(client);
            }
            sqlConnection.Close();
            return clients;
        }
    }
}