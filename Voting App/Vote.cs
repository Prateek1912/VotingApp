﻿using System;
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
        private static bool flag = false;
        public static void VoteFunc(SqlConnection con)
        {
            ConsoleKeyInfo info=default;
            SqlDataReader rdr=null;
            SqlCommand cmd;
            string sql="";
    
            while (true)
            {
                Console.Clear();
                Console.Write("Enter your Voter ID (it must be a 6-digit number): ");
                voterID = Console.ReadLine();
                var isVoterIDValid = int.TryParse(voterID, out int x);
                if (isVoterIDValid && voterID.Length == 6)
                {
                    cmd = new SqlCommand("SELECT * from voters where voterid=" + voterID, con);
                    rdr = cmd.ExecuteReader();


                    if (rdr.HasRows)
                    {

                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Choose the election in which you want to vote:");
                            Console.WriteLine(@"1. Election 1
2. Election 2
3. Exit");
                            Console.Write("\nEnter your choice (1-3): ");
                            var isValidChoice = int.TryParse(Console.ReadLine(), out int ch);
                            if (isValidChoice)
                            {
                                switch (ch)
                                {
                                    case 1:
                                        sql = @"select hasvoted from votingstatus where electionid=1 and voterid=" + voterID;
                                        cmd=new SqlCommand(sql, con);
                                        rdr=cmd.ExecuteReader();
                                        if (rdr.Read())
                                        {
                                            if (rdr.GetBoolean(0))
                                            {
                                                Console.Clear();
                                                Console.WriteLine("You have already voted in Election 1!!!!");
                                                Console.WriteLine("\nPress any key to return to the previous menu or press enter to return to the main menu...");
                                                info = Console.ReadKey(true);
                                                if (info.Key == ConsoleKey.Enter)
                                                    flag = true;
                                            }
                                            else
                                            {
                                                while (true)
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("PARTIES:");
                                                    Console.WriteLine(@"
1. Party A
2. Party B
3. Party F
4. None of the above");
                                                    Console.WriteLine();
                                                    Console.Write("Enter your choice (1-4): ");
                                                    bool flag1 = false;
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
                                                                UpdateTables(con, cmd, info, partyid,"1");
                                                                break;
                                                            case 2:
                                                                partyid = GetPartyID(con, "Party B");
                                                                UpdateTables(con, cmd, info,partyid,"1");
                                                                break;
                                                            case 3:
                                                                partyid = GetPartyID(con, "Party F");
                                                                UpdateTables(con, cmd, info,partyid,"1");
                                                                break;
                                                            case 4:
                                                                partyid = GetPartyID(con, "NOTA");
                                                                UpdateTables(con, cmd, info, partyid,"1");
                                                                break;
                                                            default:
                                                                Console.Clear();
                                                                Console.WriteLine("Please enter a valid choice!!!");
                                                                Console.WriteLine("\nPress any key to return to enter your choice again or press enter to the previous menu...");
                                                                info = Console.ReadKey(true);
                                                                if (info.Key == ConsoleKey.Enter)
                                                                    flag1 = true;
                                                                break;
                                                        }
                                                        if (flag)
                                                            break;
                                                    }
                                                    if (flag1)
                                                        break;
                                                }
                                            }
                                        }
                                        break;
                                    case 2:
                                        sql = @"select hasvoted from votingstatus where electionid=2 and voterid=" + voterID;
                                        cmd = new SqlCommand(sql, con);
                                        rdr = cmd.ExecuteReader();
                                        if (rdr.Read())
                                        {
                                            if (rdr.GetBoolean(0))
                                            {
                                                Console.Clear();
                                                Console.WriteLine("You have already voted in Election 2!!!!");
                                                Console.WriteLine("\nPress any key to return to the previous menu or press enter to return to the main menu...");
                                                info = Console.ReadKey(true);
                                                if (info.Key == ConsoleKey.Enter)
                                                    flag = true;
                                            }
                                            else
                                            {
                                                bool flag2 = false;
                                                while (true)
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("PARTIES:");
                                                    Console.WriteLine(@"
1. Party A
2. Party B
3. Party C
4. Party D
5. Party E
6. None of the above");
                                                    Console.WriteLine();
                                                    Console.Write("Enter your choice (1-6): ");
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
                                                                UpdateTables(con, cmd, info, partyid,"2");
                                                                break;
                                                            case 2:
                                                                partyid = GetPartyID(con, "Party B");
                                                                UpdateTables(con, cmd,  info,partyid,"2");
                                                                break;
                                                            case 3:
                                                                partyid = GetPartyID(con, "Party C");
                                                                UpdateTables(con, cmd,info, partyid,"2");
                                                                break;
                                                            case 4:
                                                                partyid = GetPartyID(con, "Party D");
                                                                UpdateTables(con, cmd,info,partyid,"2");
                                                                break;
                                                            case 5:
                                                                partyid = GetPartyID(con, "Party E");
                                                                UpdateTables(con, cmd,info,partyid,"2");
                                                                break;
                                                            case 6:
                                                                partyid = GetPartyID(con, "Party F");
                                                                UpdateTables(con, cmd,info,partyid,"2");
                                                                break;
                                                            default:
                                                                Console.Clear();
                                                                Console.WriteLine("Please enter a valid choice!!!");
                                                                Console.WriteLine("\nPress any key to return to enter your choice again or press enter to return to the previous menu...");
                                                                info = Console.ReadKey(true);
                                                                if (info.Key == ConsoleKey.Enter)
                                                                    flag2 = true;
                                                                break;
                                                        }
                                                        if (flag)
                                                            break;
                                                    }
                                                    if (flag2)
                                                        break;
                                                }
                                            }
                                        }
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
                                if (flag)
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
                                    flag = true;
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
                            flag = true;

                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a valid Voter ID!!!!");
                    Console.WriteLine("\nPress any key to enter Voter ID again or press enter to return to the main menu...");
                    info = Console.ReadKey(true);
                    if (info.Key == ConsoleKey.Enter)
                        flag = true;

                }

                if (flag)
                  break;

            }
                   
        }
        public static void UpdateTables(SqlConnection con, SqlCommand cmd,ConsoleKeyInfo info,string partyid,string electionid)
        {
            
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                string sql = @"update votingstatus set hasvoted=1 where voterid=" + voterID +"and electionid="+electionid;
                cmd = new SqlCommand(sql, con);
                adapter.UpdateCommand = new SqlCommand(sql, con);
                adapter.UpdateCommand.ExecuteNonQuery();
                sql = @"update partystatus set votes=votes+1 where partyid="+partyid+"and electionid="+electionid;
                cmd = new SqlCommand(sql, con);
                adapter.UpdateCommand = new SqlCommand(sql, con);
                adapter.UpdateCommand.ExecuteNonQuery();
            }
            Console.Clear();
            Console.WriteLine("Congratulations!!! You have successfully voted!!!");
            Console.WriteLine("\nPress any key to return to the previous menu or press enter to return to the main menu...");
            info = Console.ReadKey(true);
            if (info.Key == ConsoleKey.Enter)
                flag = true;

        }

        public static string GetPartyID(SqlConnection con,string name)
        {
            string sql= @"select partyid from parties where name="+name;
            SqlCommand cmd = new SqlCommand(sql, con);
            string partyID = "";
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if(rdr.Read())
                    partyID=Convert.ToString(rdr.GetValue(0));

            }
            return partyID;
        }
      
    }
}