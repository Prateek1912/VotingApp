using System;
using System.Data.SqlClient;
using System.Configuration;

namespace Voting_App
{
    internal class Program
    {
        static void Main()
        {
            while (true)
            {
                MenuPages.MainMenu();
                bool exitFromApp = false;
                var isValidChoice = int.TryParse(Console.ReadLine(), out int ch);
                if (!isValidChoice)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a number between 1-5 !!!!!");
                    Console.WriteLine("\nPress any key to return to the main menu...");
                    Console.ReadKey(true);
                }
                else
                {
                    switch (ch)
                    {
                        case 1:
                            Vote.VoteFunc();
                            break;
                        case 2:
                            LeadingParty.LeadingPartyFunc();
                            break;
                        case 3:
                            var obj = new AllPartyVotes();
                            obj.AllPartyVotesFunc();
                            break;
                        case 4:
                            Registration.RegistrationFunc();
                            break;
                        case 5:
                            Console.WriteLine("\nThank you for using this app!!! Hope to see you soon!!!\n");
                            exitFromApp = true;
                            break;
                        default:
                            Console.WriteLine("Please enter a valid choice!!!!");
                            Console.WriteLine("\nPress any key to return to the main menu...");
                            Console.ReadKey(true);
                            break;
                    }
                    if (exitFromApp)
                    {
                        ExecuteQuery.CloseConnection();
                        break;
                    }
                }
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
                        password = password.Substring(0, password.Length - 1);
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info=Console.ReadKey(true);
            }
            return password;
        }
    }
}
