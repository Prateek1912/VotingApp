using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Voting_App
{
    internal class Vote
    {
        private static string voterID;
        private static bool returnToMainMenu;
        public static void VoteFunc(SqlConnection con)
        {
            ConsoleKeyInfo info=default;
            SqlDataReader rdr=null;
            string sql="";
            returnToMainMenu=false;
            while (true)
            {
                Console.Clear();
                Console.Write("Enter your Voter ID (it must be a 6-digit number): ");
                voterID = Console.ReadLine();
                var isVoterIDValid = int.TryParse(voterID, out int x);
                if (isVoterIDValid && voterID.Length == 6)
                {
                    sql = "SELECT * from voters where voterid=" + voterID;
                    rdr = ExecuteQuery.ExecuteSelectQuery(sql, con);


                    if (rdr.HasRows)
                    {

                        while (true)
                        {
                            Console.WriteLine("Choose the election in which you want to vote:");
                            MenuPages.ElectionMenu();
                            var isValidChoice = int.TryParse(Console.ReadLine(), out int ch);
                            if (isValidChoice)
                            {
                                switch (ch)
                                {
                                    case 1:
                                        VoteInElection1(con);
                                        break;
                                    case 2:
                                        VoteInElection2(con);
                                        break;
                                    case 3:
                                        returnToMainMenu = true;
                                        break;
                                    default:
                                        Console.Clear();
                                        Console.WriteLine("Please enter a valid choice!!!!!");
                                        Console.WriteLine("\nPress any key to return to enter your choice again or press enter to return to the main menu...");
                                        info = Console.ReadKey(true);
                                        if (info.Key == ConsoleKey.Enter)
                                            returnToMainMenu = true;
                                        break;
                                }
                                if (returnToMainMenu)
                                    break;
                             
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Please enter a number between 1 to 3 !!!!!");
                                Console.WriteLine("\nPress any key to return to enter your choice again or press enter to return to the main menu...");
                                info = Console.ReadKey(true);
                                if (info.Key == ConsoleKey.Enter)
                                {
                                    returnToMainMenu = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.Clear ();
                        Console.WriteLine("Voter ID not registered!!!! Please register yourself first.");
                        Console.WriteLine("\nPress any key to enter Voter ID again or press enter to return to the main menu...");
                        info = Console.ReadKey(true);
                        if (info.Key == ConsoleKey.Enter)
                            returnToMainMenu = true;

                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a valid Voter ID!!!!");
                    Console.WriteLine("\nPress any key to enter Voter ID again or press enter to return to the main menu...");
                    info = Console.ReadKey(true);
                    if (info.Key == ConsoleKey.Enter)
                        returnToMainMenu = true;

                }

                if (returnToMainMenu)
                  break;

            }
                   
        }
        private static void UpdateTables(SqlConnection con, SqlCommand cmd,ConsoleKeyInfo info,string partyid,string electionid,string name)
        {

            string sql = @"update votingstatus set hasvoted=1 where voterid=" + voterID + "and electionid=" + electionid;
            ExecuteQuery.ExecuteUpdateQuery(sql, con);
            sql = @"update partystatus set votes=votes+1 where partyid=" + partyid + "and electionid=" + electionid;
            ExecuteQuery.ExecuteUpdateQuery(sql, con);
            Console.Clear();
            Console.WriteLine("Congratulations!!! You have successfully voted for {0}!!!",name);
            Console.WriteLine("\nPress any key to return to the previous menu or press enter to return to the main menu...");
            info = Console.ReadKey(true);
            if (info.Key == ConsoleKey.Enter)
                returnToMainMenu = true;

        }

        private static string GetPartyID(SqlConnection con,string name)
        {
            string partyID = "";
            string sql = @"select partyid from parties where name='" + name + "'";
            SqlDataReader rdr = ExecuteQuery.ExecuteSelectQuery(sql, con);
            if (rdr.Read())
                partyID = Convert.ToString(rdr.GetValue(0));
            return partyID;
        }

        private static void VoteInElection1(SqlConnection con)
        {
            ConsoleKeyInfo info = default;
            SqlCommand cmd = default;
            string sql = @"select hasvoted from votingstatus where electionid=1 and voterid=" + voterID;
            SqlDataReader rdr = ExecuteQuery.ExecuteSelectQuery(sql, con);
            if (rdr.Read())
            {
                if (rdr.GetBoolean(0))
                {
                    Console.Clear();
                    Console.WriteLine("You have already voted in Election 1!!!!");
                    Console.WriteLine("\nPress any key to return to the previous menu or press enter to return to the main menu...");
                    info = Console.ReadKey(true);
                    if (info.Key == ConsoleKey.Enter)
                        returnToMainMenu = true;
                }
                else
                {
                    while (true)
                    {
                        MenuPages.Election1Menu();
                        bool returnToPreviousMenu = false;
                        var isValidCh = int.TryParse(Console.ReadLine(), out int choice);
                        if (!isValidCh)
                        {
                            Console.Clear();
                            Console.WriteLine("Please enter a number between 1-4 !!!!!");
                            Console.WriteLine("\nPress any key to enter again or press enter to return to the previous menu...");
                            info = Console.ReadKey(true);
                            if (info.Key == ConsoleKey.Enter)
                                break;
                        }
                        else
                        {
                            string partyid = "";
                            switch (choice)
                            {
                                case 1:
                                    partyid = GetPartyID(con, "Party A");
                                    UpdateTables(con, cmd, info, partyid, "1", "Party A");
                                    returnToPreviousMenu = true;
                                    break;
                                case 2:
                                    partyid = GetPartyID(con, "Party B");
                                    UpdateTables(con, cmd, info, partyid, "1", "Party B");
                                    returnToPreviousMenu = true;
                                    break;
                                case 3:
                                    partyid = GetPartyID(con, "Party F");
                                    UpdateTables(con, cmd, info, partyid, "1", "Party F");
                                    returnToPreviousMenu = true;
                                    break;
                                case 4:
                                    partyid = GetPartyID(con, "NOTA");
                                    UpdateTables(con, cmd, info, partyid, "1", "NOTA");
                                    returnToPreviousMenu = true;
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Please enter a valid choice!!!");
                                    Console.WriteLine("\nPress any key to return to enter your choice again or press enter to the previous menu...");
                                    info = Console.ReadKey(true);
                                    if (info.Key == ConsoleKey.Enter)
                                        returnToPreviousMenu = true;
                                    break;
                            }
                            if (returnToMainMenu)
                                break;
                        }
                        if (returnToPreviousMenu)
                            break;
                    }
                }
            }
        }

        private static void VoteInElection2(SqlConnection con)
        {
            ConsoleKeyInfo info = default;
            SqlCommand cmd = default;
            string sql = @"select hasvoted from votingstatus where electionid=2 and voterid=" + voterID;
            SqlDataReader rdr = ExecuteQuery.ExecuteSelectQuery(sql, con);
            if (rdr.Read())
            {
                if (rdr.GetBoolean(0))
                {
                    Console.Clear();
                    Console.WriteLine("You have already voted in Election 2!!!!");
                    Console.WriteLine("\nPress any key to return to the previous menu or press enter to return to the main menu...");
                    info = Console.ReadKey(true);
                    if (info.Key == ConsoleKey.Enter)
                        returnToMainMenu = true;
                }
                else
                {
                    bool returnToPreviousMenu = false;
                    while (true)
                    {
                        MenuPages.Election2Menu();
                        var isValidCh = int.TryParse(Console.ReadLine(), out int choice);
                        if (!isValidCh)
                        {
                            Console.Clear();
                            Console.WriteLine("Please enter a number between 1-6 !!!!!");
                            Console.WriteLine("\nPress any key to enter again or press enter to return to the previous menu...");
                            info = Console.ReadKey(true);
                            if (info.Key == ConsoleKey.Enter)
                                break;
                        }
                        else
                        {
                            string partyid = "";
                            switch (choice)
                            {
                                case 1:
                                    partyid = GetPartyID(con, "Party A");
                                    UpdateTables(con, cmd, info, partyid, "2", "Party A");
                                    returnToPreviousMenu = true;
                                    break;
                                case 2:
                                    partyid = GetPartyID(con, "Party B");
                                    UpdateTables(con, cmd, info, partyid, "2", "Party B");
                                    returnToPreviousMenu = true;
                                    break;
                                case 3:
                                    partyid = GetPartyID(con, "Party C");
                                    UpdateTables(con, cmd, info, partyid, "2", "Party C");
                                    returnToPreviousMenu = true;
                                    break;
                                case 4:
                                    partyid = GetPartyID(con, "Party D");
                                    UpdateTables(con, cmd, info, partyid, "2", "Party D");
                                    returnToPreviousMenu = true;
                                    break;
                                case 5:
                                    partyid = GetPartyID(con, "Party E");
                                    UpdateTables(con, cmd, info, partyid, "2", "Party E");
                                    returnToPreviousMenu = true;
                                    break;
                                case 6:
                                    partyid = GetPartyID(con, "NOTA");
                                    UpdateTables(con, cmd, info, partyid, "2", "NOTA");
                                    returnToPreviousMenu = true;
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Please enter a valid choice!!!");
                                    Console.WriteLine("\nPress any key to return to enter your choice again or press enter to return to the previous menu...");
                                    info = Console.ReadKey(true);
                                    if (info.Key == ConsoleKey.Enter)
                                        returnToPreviousMenu = true;
                                    break;
                            }
                            if (returnToMainMenu)
                                break;
                        }
                        if (returnToPreviousMenu)
                            break;
                    }
                }
            }
        }
      
    }
}
