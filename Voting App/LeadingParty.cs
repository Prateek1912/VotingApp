using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Voting_App
{
    internal class LeadingParty:AllPartyVotes
    {
        public static void LeadingPartyFunc(SqlConnection con)
        {
            bool flag = false;
            ConsoleKeyInfo info=default;
            SqlCommand cmd=null;
            AllPartyVotes obj=new LeadingParty();    //parent class reference with child class object
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
                                    obj.Print(con, cmd, info, "1");    //runtime polymorphism
                                    break;
                                case 2:
                                    obj.Print(con, cmd, info, "2");
                                    break;
                                case 3:
                                    flag = true;
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Please enter a valid choice!!!!!");
                                    Console.WriteLine("\nPress any key to return to enter your choice again or press enter to return to the main menu...");
                                    info = Console.ReadKey(true);
                                    if (info.Key == ConsoleKey.Enter)
                                        flag = true;
                                    break;
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Please enter a number between 1 to 3 !!!!!");
                            Console.WriteLine("\nPress any key to return to enter your choice again or press enter to return to the main menu...");
                            info = Console.ReadKey(true);
                            if (info.Key == ConsoleKey.Enter)
                            {
                                flag = true;
                                break;
                            }

                        }
                        if (flag)
                           break;
                    }
                        
                }
                
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter correct password!!!!!");
                    Console.WriteLine("\nPress any key to enter the password again or press enter to return to the main menu..");
                    info = Console.ReadKey(true);
                    if(info.Key== ConsoleKey.Enter)
                        flag = true;
                }
                if (flag)
                    break;
            }
        }

        public override void Print(SqlConnection con, SqlCommand cmd, ConsoleKeyInfo info, string electionid)
        {
            string query = @"select distinct(parties.name),partystatus.votes from parties,partystatus where parties.partyid=partystatus.partyid and partystatus.votes=(select max(votes) from partystatus where name!='NOTA' and electionid="+electionid+")";
            SqlDataReader reader = ExecuteQuery.ExecuteSelectQuery(query, con);
            Console.Clear();
            var dictionary = new Dictionary<string, string>();
            while (reader.Read())
            {
                dictionary.Add(reader.GetString(0), Convert.ToString(reader.GetValue(1)));
            }
            int i;
            if (dictionary.Count > 1)
            {
                for (i = 0; i < dictionary.Count - 1; i++)
                {
                    Console.Write("{0}, ", dictionary.ElementAt(i).Key);
                }
                Console.WriteLine("{0} are all currently drawn at {1} votes in Election {2}", dictionary.ElementAt(i).Key, dictionary.ElementAt(i).Value,electionid);
            }
            else
            {
                Console.WriteLine("{0} leads with a total of {1} votes in Election {2}!!!", dictionary.ElementAt(0).Key, dictionary.ElementAt(0).Value,electionid);
            }
            Console.WriteLine("\nPress any key to return to the previous menu...");
            info = Console.ReadKey(true);
        }
    }
}
