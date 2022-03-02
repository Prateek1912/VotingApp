using System;
using System.Data.SqlClient;
using System.Configuration;

namespace Voting_App
{
    internal class ExecuteQuery
    {
        private readonly static SqlConnection con;
        static ExecuteQuery()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["VotingApp"].ConnectionString);
            con.Open();
        }
        public static SqlDataReader ExecuteSelectQuery(string query)
        {
            SqlDataReader rdr = default;
            try
            {
                var cmd = new SqlCommand(query, con);
                rdr = cmd.ExecuteReader();
                return rdr;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return rdr;
            }
        }
        public static void ExecuteUpdateQuery(string query)
        {
            try
            {
                var adapter = new SqlDataAdapter();
                var cmd = new SqlCommand(query, con);
                adapter.UpdateCommand = new SqlCommand(query, con);
                adapter.UpdateCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void ExecuteInsertQuery(string query)
        {
            try
            {
                var adapter = new SqlDataAdapter();
                var cmd = new SqlCommand(query, con);
                adapter.InsertCommand = new SqlCommand(query, con);
                adapter.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void CloseConnection()
        {
            con.Close();
        }
    }
}
