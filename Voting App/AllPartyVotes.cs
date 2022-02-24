﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Voting_App
{
    internal class AllPartyVotes
    {
        public static void AllPartyVotesFunc(SqlConnection con)
        {
            bool returnToMainMenu= false;
            ConsoleKeyInfo info=default;
            SqlCommand cmd=default;
            var obj=new AllPartyVotes();    
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
                            switch (ch)
                            {
                                case 1:
                                    obj.Print(con, cmd, info, "1");
                                    break;
                                case 2:
                                    obj.Print(con, cmd, info, "2");
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
                              
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Please enter a number between 1 to 3 !!!!!");
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

        public virtual void Print(SqlConnection con, SqlCommand cmd, ConsoleKeyInfo info, string electionid)
        {
            Console.Clear();
            string query = @"select parties.name,partystatus.votes from parties,PartyStatus where parties.partyid=PartyStatus.partyid and electionid="+electionid+"order by PartyStatus.votes desc";
            SqlDataReader reader = ExecuteQuery.ExecuteSelectQuery(query, con);
            Console.WriteLine("ELECTION {0} STANDINGS:\n", electionid);
            Console.WriteLine("PARTY                VOTES\n");
            while (reader.Read())
            {
                if (reader.GetString(0) == "NOTA")
                    Console.WriteLine("None of the Above      {0}", Convert.ToString(reader.GetValue(1)));
                else
                {
                    Console.WriteLine("{0}                {1}", reader.GetString(0), Convert.ToString(reader.GetValue(1)));
                }
            }
            Console.WriteLine("\nPress any key to return to the previous menu.....");
            info = Console.ReadKey(true);
        }
    }
}
