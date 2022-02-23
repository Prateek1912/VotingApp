using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;

namespace Voting_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-5UK5U70\SQLEXPRESS;Initial Catalog=VotingApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true");
            try {
                    ConsoleKeyInfo info;
                    while (true)
                    {
                        MenuPages.MainMenu();
                        int flag = 0;
                        var isValidChoice = int.TryParse(Console.ReadLine(), out int ch);
                        if (!isValidChoice)
                        {
                            Console.Clear();
                            Console.WriteLine("Please enter a number between 1-5 !!!!!");
                            Console.WriteLine("\nPress any key to return to the main menu...");
                            info = Console.ReadKey(true);
                        }
                        else
                        {
                            con.Open();

                                switch (ch)
                                {
                                    case 1:
                                        Vote.VoteFunc(con);
                                        break;
                                    case 2:
                                        LeadingParty.LeadingPartyFunc(con);
                                        break;
                                    case 3:
                                        AllPartyVotes.AllPartyVotesFunc(con);
                                        break;
                                    case 4:
                                        Registration.RegistrationFunc(con);
                                        break;
                                    case 5:
                                        Console.WriteLine("\nThank you for using this app!!! Hope to see you soon!!!\n");
                                        flag = 1;
                                        break;
                                    default:
                                        Console.WriteLine("Please enter a valid choice!!!!");
                                        Console.WriteLine("\nPress any key to return to the main menu...");
                                        info = Console.ReadKey(true);
                                        break;
                                }
                                if (flag == 1)
                                    break;
                        }
                    }
            }catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Something unexpected occurred!!!!");
            }
            finally
            {
                con.Close();
            }
        }

        
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            return password;
        }
    }
}
