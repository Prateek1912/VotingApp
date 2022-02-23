using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace Voting_App
{
    internal class Registration
    {
        private static string aadhar;
        private static string name;
        private static int voterID;
        public static void RegistrationFunc(SqlConnection con)
        {
            ConsoleKeyInfo info;
            SqlCommand cmd;
            SqlDataReader reader;
            bool flag=false;
            while (true)
            {
                Console.Clear();
                Console.Write("Enter your Aadhar number(12-digit number): ");
                aadhar=Console.ReadLine();
                var isAadharValid = long.TryParse(aadhar, out long y);
                if (!isAadharValid || aadhar.Length != 12)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a valid aadhar number!!!!");
                    Console.WriteLine("\nPress any key to enter your aadhar number again or press enter to return to the main menu...");
                    info = Console.ReadKey(true);
                    if (info.Key == ConsoleKey.Enter)
                        flag = true;
                }
                else
                {
                    Console.Clear();
                    string query = @"select aadhar from voters";
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();
                    bool flag2 = false;
                    while (reader.Read())
                    {
                        if (reader.GetString(0) == aadhar)
                        {
                            Console.Clear();
                            Console.WriteLine("Aadhar number already registered!!!! Please enter some other aadhar number");
                            Console.WriteLine("\nPress any key to continue....");
                            info= Console.ReadKey(true);
                            flag2 = true;
                            break;
                        }
                    }
                    if (flag2 == false)
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
                                    flag = true;
                                    break;
                                }
                            }
                            else
                            {
                                string sql = @"select max(voterid) from voters";
                                cmd = new SqlCommand(sql, con);
                                reader = cmd.ExecuteReader();

                                if (reader.Read())
                                    voterID = Convert.ToInt32(reader.GetValue(0)) + 1;
                                
                                name = "'" + name + "'";
                                aadhar = "'" + aadhar + "'";
                                sql = @"insert into voters values (" + name + "," + aadhar + "," + (voterID) + ")" +@"
insert into votingstatus values(" + voterID + ",1,0)"+@"
insert into votingstatus values("+voterID+",2,0)";
                          
                                   
                                var adapter = new SqlDataAdapter();
                                cmd = new SqlCommand(sql, con);
                                adapter.InsertCommand = new SqlCommand(sql, con);
                                adapter.InsertCommand.ExecuteNonQuery();
                                Console.Clear();
                                Console.WriteLine("You have been successfully registered!!!!");
                                Console.WriteLine("Your Voter ID is: {0}. This will be used for voting!!!", voterID);
                                Console.WriteLine("\nPress any key to return to the main menu....");
                                info= Console.ReadKey(true);
                                flag = true;
                                break;
                               

                            }
                        }

                    }
                }
                if (flag)
                    break;
            }
        }

 
    }
}
