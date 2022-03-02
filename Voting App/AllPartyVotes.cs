using System;
using System.Data.SqlClient;

namespace Voting_App
{
    internal class AllPartyVotes
    { 
        public void AllPartyVotesFunc()
        {
            bool returnToMainMenu = false;
            ConsoleKeyInfo info;
            string sql;
            
            while (true)
            {
                Console.Clear();
                Console.Write("Enter the password: ");
                var password = Program.ReadPassword();
                if (password == "hello@123")
                {
                    while (true)
                    {
                        MenuPages.ElectionMenu();
                        var isValidChoice = int.TryParse(Console.ReadLine(), out int ch);
                        if (isValidChoice)
                        {
                            sql = @"select count(distinct(electionid)) from partystatus";
                            SqlDataReader electionIDs = ExecuteQuery.ExecuteSelectQuery(sql);
                            int noOfElections = 0;

                            if (electionIDs.Read())
                                noOfElections = electionIDs.GetInt32(0);

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
                                        this.Print(electionID.GetInt32(0));
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
                            Console.WriteLine("\nPress any key to enter your choice again or press enter to return to the main menu...");
                            info = Console.ReadKey(true);
                            if (info.Key == ConsoleKey.Enter)
                                returnToMainMenu = true;
                        }
                        if (returnToMainMenu)
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter correct password!!!!!");
                    Console.WriteLine("\nPress any key to enter the password again or press enter to return to the main menu...");
                    info = Console.ReadKey(true);
                    if (info.Key == ConsoleKey.Enter)
                        returnToMainMenu = true;
                }
                if (returnToMainMenu)
                    break;
            }

        }
        public virtual void Print(int electionid)
        {
            Console.Clear();
            string query = @"select parties.name,partystatus.votes from parties,PartyStatus where parties.partyid=PartyStatus.partyid and electionid=" + electionid + "order by PartyStatus.votes desc";
            SqlDataReader partyNameAndVotes = ExecuteQuery.ExecuteSelectQuery(query);
            Console.WriteLine("ELECTION {0} STANDINGS:\n", electionid);
            Console.WriteLine("PARTY                VOTES\n");
            while (partyNameAndVotes.Read())
            {
                if (partyNameAndVotes.GetString(0) == "NOTA")
                    Console.WriteLine("None of the Above      {0}", Convert.ToString(partyNameAndVotes.GetValue(1)));
                else
                {
                    Console.WriteLine("{0}                {1}", partyNameAndVotes.GetString(0), Convert.ToString(partyNameAndVotes.GetValue(1)));
                }
            }
            Console.WriteLine("\nPress any key to return to the previous menu.....");
            Console.ReadKey(true);
        }
    }
}
