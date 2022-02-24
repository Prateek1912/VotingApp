using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Voting_App
{
    internal class ExecuteQuery
    {
        public static SqlDataReader ExecuteSelectQuery(string query, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            return rdr;
        }
        public static void ExecuteUpdateQuery(string query, SqlConnection con)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand(query, con);
            adapter.UpdateCommand = new SqlCommand(query, con);
            adapter.UpdateCommand.ExecuteNonQuery();
            
        }
    }
}
