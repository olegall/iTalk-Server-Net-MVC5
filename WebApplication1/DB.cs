using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;
namespace WebApplication1
{
    public class DB
    {
        private SqlConnection Connect()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString; ;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            return sqlConnection;
        }

        public PrivateConsultant GetPrivateConsultantById(int id)
        {
            PrivateConsultant privateConsultant = null;

            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM PrivateConsultants WHERE Id = " + id;
            cmd.CommandType = CommandType.Text;
            var sqlConnection = Connect();
            cmd.Connection = sqlConnection;

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                privateConsultant = new PrivateConsultant {
                    Id = (int)reader["Id"],
                    ModerationStatusId = (int)reader["ModerationStatusId"],
                    Phone = (string)reader["Phone"],
                    Email = (string)reader["Email"],
                    YandexWalletNum = (string)reader["YandexWalletNum"],
                    ITalkCommittee = (decimal)reader["ITalkCommittee"],
                    ITalkDebts = (decimal)reader["ITalkDebts"],
                    ITalkEarnedMoney = (decimal)reader["ITalkEarnedMoney"],
                    Name = (string)reader["Name"],
                    Surname = (string)reader["Surname"],
                    Patronymic = (string)reader["Patronymic"],
                    Rating = (decimal)reader["Rating"]
                };

            }
            sqlConnection.Close();
            return privateConsultant;
        }

        public IList<string> GetServicesTitles(int consultantId)
        {
            IList<string> titles = new List<string>();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT Title FROM Services WHERE ConsultantId = " + consultantId;
            cmd.CommandType = CommandType.Text;
            var sqlConnection = Connect();
            cmd.Connection = sqlConnection;

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                titles.Add((string)reader["Title"]);
            }
            sqlConnection.Close();
            return titles;
        }
    }
}