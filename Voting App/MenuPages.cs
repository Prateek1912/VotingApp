using System;
using System.Data.SqlClient;

namespace Voting_App
{
    internal class MenuPages
    {
        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("VOTING APP");
            Console.WriteLine(@"
    1. Vote
    2. Leading Party
    3. All parties' votes
    4. Register Yourself
    5. Exit");
            Console.WriteLine();
            Console.Write("Enter your choice (1-5): ");
        }
        public static void ElectionMenu()
        {
            Console.Clear();
            int i = 0;
            Console.WriteLine("ELECTIONS:\n");
            string sql = @"select distinct(electionid) from partystatus";
            SqlDataReader rdr = ExecuteQuery.ExecuteSelectQuery(sql);
            while (rdr.Read())
            {
                Console.WriteLine((++i) + ". Election " + rdr.GetValue(0));
            }
            Console.WriteLine((++i) + ". Exit");
            Console.Write("\nEnter your choice (1-" + i + "): ");
        }
        public static void PartyMenu(int electionid)
        {
            Console.Clear();
            Console.WriteLine("PARTIES:\n");
            int i = 0;
            SqlDataReader rdr;
            string sql = @"select partyid from partystatus where electionid=" + electionid;
            rdr = ExecuteQuery.ExecuteSelectQuery(sql);
            while (rdr.Read())
            {
                sql = @"select name from parties where partyid=" + rdr.GetValue(0);
                SqlDataReader reader = ExecuteQuery.ExecuteSelectQuery(sql);
                if (reader.Read())
                    Console.WriteLine((++i) + ". " + reader.GetString(0));
            }
            Console.WriteLine();
            Console.Write("Enter your choice (1-" + i + "): ");
        }
    }
}
