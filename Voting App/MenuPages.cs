using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting_App
{
    internal class MenuPages
    {
        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("VOTING APP");
            Console.WriteLine(@"
    1. Vote
    2. Leading Party
    3. All parties' votes
    4. Register Yourself
    5. Exit");
            Console.WriteLine();
            Console.Write("Enter your choice (1-5): ");

        }

        public static void ElectionMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose the election in which you want to vote:");
            Console.WriteLine(@"1. Election 1
2. Election 2
3. Exit");
            Console.Write("\nEnter your choice (1-3): ");
        }

        public static void Election1Menu()
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
        }

        public static void Election2Menu()
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
        }
    }
}
