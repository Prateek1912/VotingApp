using System;
using System.Data.SqlClient;

namespace Voting_App
{
    internal class Registration
    {
        private static string aadhar;
        private static string name;
        private static int voterID;
        public static void RegistrationFunc()
        {
            ConsoleKeyInfo info;
            SqlDataReader reader;
            bool returnToMainMenu = false;
            while (true)
            {
                Console.Clear();
                Console.Write("Enter your Aadhar number(12-digit number): ");
                aadhar = Console.ReadLine();
                aadhar = aadhar.TrimStart('0');

                var isAadharValid = long.TryParse(aadhar, out long y);
                if (!isAadharValid || aadhar.Length != 12 || y < 0)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a valid aadhar number!!!!");
                    Console.WriteLine("\nPress any key to enter your aadhar number again or press enter to return to the main menu...");
                    info = Console.ReadKey(true);
                    if (info.Key == ConsoleKey.Enter)
                        returnToMainMenu = true;
                }
                else
                {
                    Console.Clear();
                    string query = @"select aadhar from voters where aadhar='" + aadhar + "'";
                    reader = ExecuteQuery.ExecuteSelectQuery(query);
                    if (reader.HasRows)
                    {
                        Console.Clear();
                        Console.WriteLine("Aadhar number already registered!!!! Please enter some other aadhar number");
                        Console.WriteLine("\nPress any key to continue....");
                        Console.ReadKey(true);
                        break;
                    }
                    else
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.Write("Enter your name: ");
                            name = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(name))
                            {
                                Console.Clear();
                                Console.WriteLine("Name cannot be empty!!!!!");
                                Console.WriteLine("\nPress any key to enter name again or press enter to return to main menu");
                                info = Console.ReadKey(true);
                                if (info.Key == ConsoleKey.Enter)
                                {
                                    returnToMainMenu = true;
                                    break;
                                }
                            }
                            else
                            {
                                string sql = @"select max(voterid) from voters";
                                reader = ExecuteQuery.ExecuteSelectQuery(sql);
                                if (reader.Read())
                                    voterID = Convert.ToInt32(reader.GetValue(0)) + 1;

                                name = "'" + name + "'";
                                aadhar = "'" + aadhar + "'";
                                InsertData(name, aadhar, voterID);
                                Console.Clear();
                                Console.WriteLine("You have been successfully registered!!!!");
                                Console.WriteLine("Your Voter ID is: {0}. This will be used for voting!!!", voterID);
                                Console.WriteLine("\nPress any key to return to the main menu....");
                                Console.ReadKey(true);
                                returnToMainMenu = true;
                                break;
                            }
                        }
                    }
                }
                if (returnToMainMenu)
                    break;
            }
        }

        private static void InsertData(string name, string aadhar, int voterID)
        {
            string sql = @"insert into voters values (" + name + "," + aadhar + "," + (voterID) + ")";
            ExecuteQuery.ExecuteInsertQuery(sql);
            sql = @"select distinct(electionid) from partystatus";
            SqlDataReader reader = ExecuteQuery.ExecuteSelectQuery(sql);
            while (reader.Read())
            {
                sql = @"insert into votingstatus values(" + voterID + "," + reader.GetInt32(0) + ",0)";
                ExecuteQuery.ExecuteInsertQuery(sql);
            }
        }
    }
}
