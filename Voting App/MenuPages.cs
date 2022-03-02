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
            SqlDataReader electionID = ExecuteQuery.ExecuteSelectQuery(sql);
            while (electionID.Read())
            {
                Console.WriteLine((++i) + ". Election " + electionID.GetValue(0));
            }
            Console.WriteLine((++i) + ". Exit");
            Console.Write("\nEnter your choice (1-" + i + "): ");
        }
        public static void PartyMenu(int electionid)
        {
            Console.Clear();
            Console.WriteLine("PARTIES:\n");
            int i = 0;
            string sql = @"select partyid from partystatus where electionid=" + electionid;
            SqlDataReader partyID = ExecuteQuery.ExecuteSelectQuery(sql);
            while (partyID.Read())
            {
                sql = @"select name from parties where partyid=" + partyID.GetValue(0);
                SqlDataReader partyName = ExecuteQuery.ExecuteSelectQuery(sql);
                if (partyName.Read())
                    Console.WriteLine((++i) + ". " + partyName.GetString(0));
            }
            Console.WriteLine();
            Console.Write("Enter your choice (1-" + i + "): ");
        }
    }
}
