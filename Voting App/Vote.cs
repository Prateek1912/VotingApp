using System;
using System.Data.SqlClient;

namespace Voting_App
{
    internal class Vote
    {
        private static string voterID;
        private static bool returnToMainMenu;
        public static void VoteFunc()
        {
            ConsoleKeyInfo info;
            string sql;
            returnToMainMenu = false;
            while (true)
            {
                Console.Clear();
                Console.Write("Enter your Voter ID (it must be a 6-digit number): ");
                voterID = Console.ReadLine();
                var isVoterIDValid = int.TryParse(voterID, out int x);
                if (isVoterIDValid && voterID.Length == 6)
                {
                    sql = "SELECT * from voters where voterid=" + voterID;
                    SqlDataReader detailsOfVoterID = ExecuteQuery.ExecuteSelectQuery(sql);
                    if (detailsOfVoterID.HasRows)
                    {
                        while (true)
                        {
                            MenuPages.ElectionMenu();
                            var isValidChoice = int.TryParse(Console.ReadLine(), out int ch);
                            if (isValidChoice)
                            {
                                sql = @"select count(distinct(electionid)) from partystatus";
                                SqlDataReader countOfElections = ExecuteQuery.ExecuteSelectQuery(sql);
                                int noOfElections = 0;

                                if (countOfElections.Read())
                                    noOfElections = countOfElections.GetInt32(0);

                                if (ch > noOfElections || ch < 1)
                                {
                                    if (ch == noOfElections + 1)
                                    {
                                        returnToMainMenu = true;
                                        break;
                                    }
                                    Console.Clear();
                                    Console.WriteLine("Please enter a valid choice!!!!");
                                    Console.WriteLine("\nPress any key to enter again or press enter to return to the main menu...");
                                    info = Console.ReadKey(true);
                                    if (info.Key == ConsoleKey.Enter)
                                        returnToMainMenu = true;
                                }
                                else
                                {
                                    sql = @"select distinct(electionid) from partystatus";
                                    SqlDataReader electionID = ExecuteQuery.ExecuteSelectQuery(sql);
                                    int row = 1;
                                    while (electionID.Read())
                                    {
                                        if (ch == row)
                                        {
                                            VoteInElection(electionID.GetInt32(0));
                                            break;
                                        }
                                        row++;
                                    }
                                }
                                if (returnToMainMenu)
                                    break;

                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Please enter a valid choice!!!!!");
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
                        Console.Clear();
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
        private static void UpdateTables(int partyid, int electionid)
        {
            ConsoleKeyInfo info = default;
            string sql = @"update votingstatus set hasvoted=1 where voterid=" + voterID + "and electionid=" + electionid;
            ExecuteQuery.ExecuteUpdateQuery(sql);
            sql = @"update partystatus set votes=votes+1 where partyid=" + partyid + "and electionid=" + electionid;
            ExecuteQuery.ExecuteUpdateQuery(sql);
            Console.Clear();
            sql = @"select name from parties where partyid=" + partyid;
            SqlDataReader partyName = ExecuteQuery.ExecuteSelectQuery(sql);
            if (partyName.Read())
                Console.WriteLine("Congratulations!!! You have successfully voted for {0}!!!", partyName.GetString(0));
            Console.WriteLine("\nPress any key to return to the previous menu or press enter to return to the main menu...");
            info = Console.ReadKey(true);
            if (info.Key == ConsoleKey.Enter)
                returnToMainMenu = true;

        }
        private static void VoteInElection(int electionid)
        {
            ConsoleKeyInfo info;
            string sql = @"select hasvoted from votingstatus where electionid=" + electionid + "and voterid=" + voterID;
            SqlDataReader hasVoted = ExecuteQuery.ExecuteSelectQuery(sql);
            if (hasVoted.Read())
            {
                if (hasVoted.GetBoolean(0))
                {
                    Console.Clear();
                    Console.WriteLine("You have already voted in Election" + electionid + "!!!!");
                    Console.WriteLine("\nPress any key to return to the previous menu or press enter to return to the main menu...");
                    info = Console.ReadKey(true);
                    if (info.Key == ConsoleKey.Enter)
                        returnToMainMenu = true;
                }
                else
                {
                    while (true)
                    {
                        MenuPages.PartyMenu(electionid);
                        bool returnToPreviousMenu = false;
                        var isValidCh = int.TryParse(Console.ReadLine(), out int choice);
                        if (!isValidCh)
                        {
                            Console.Clear();
                            Console.WriteLine("Please enter a valid choice!!!!!");
                            Console.WriteLine("\nPress any key to enter again or press enter to return to the previous menu...");
                            info = Console.ReadKey(true);
                            if (info.Key == ConsoleKey.Enter)
                                break;
                        }
                        else
                        {
                            sql = @"select count(partyid) from partystatus where electionid=" + electionid;
                            SqlDataReader countOfParties = ExecuteQuery.ExecuteSelectQuery(sql);

                            int noOfParties = 0;
                            if (countOfParties.Read())
                                noOfParties = countOfParties.GetInt32(0);

                            if (choice > noOfParties || choice < 1)
                            {
                                Console.Clear();
                                Console.WriteLine("Please enter a valid choice!!!!");
                                Console.WriteLine("\nPress any key to enter again or press enter to return to the previous menu...");
                                info = Console.ReadKey(true);
                                if (info.Key == ConsoleKey.Enter)
                                    break;
                            }
                            else
                            {
                                sql = @"select partyid from partystatus where electionid=" + electionid;
                                SqlDataReader partyID = ExecuteQuery.ExecuteSelectQuery(sql);
                                int row = 1;
                                while (partyID.Read())
                                {
                                    if (choice == row)
                                    {
                                        UpdateTables(partyID.GetInt32(0), electionid);
                                        returnToPreviousMenu = true;
                                        break;
                                    }
                                    row++;
                                }
                            }
                            if (returnToMainMenu)
                                break;
                        }
                        if (returnToPreviousMenu)
                            break;
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You are not eligible to vote in election "+electionid+"!!!!");
                Console.WriteLine("\nPress any key to return to the previous menu or press enter to return to the main menu...");
                info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Enter)
                    returnToMainMenu = true;
            }
        }
    }
}
