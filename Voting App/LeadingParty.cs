using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Voting_App
{
    internal class LeadingParty : AllPartyVotes
    {
        public static void LeadingPartyFunc()
        {
            AllPartyVotes obj=new LeadingParty();
            obj.AllPartyVotesFunc();
        }
        public override void Print(int electionid)
        {
            string query = @"select distinct(parties.name),partystatus.votes from parties,partystatus where partystatus.electionid=" + electionid + " and parties.partyid=partystatus.partyid and parties.name!='NOTA' and partystatus.votes=(select max(votes) from partystatus where electionid=" + electionid + ")";
            SqlDataReader partyNameAndVotes = ExecuteQuery.ExecuteSelectQuery(query);
            Console.Clear();
            var dictionary = new Dictionary<string, string>();
            while (partyNameAndVotes.Read())
            {
                dictionary.Add(partyNameAndVotes.GetString(0), Convert.ToString(partyNameAndVotes.GetValue(1)));
            }
            int i;
            if (dictionary.Count > 1)
            {
                for (i = 0; i < dictionary.Count - 1; i++)
                {
                    Console.Write("{0}, ", dictionary.ElementAt(i).Key);
                }
                Console.WriteLine("{0} are all currently drawn at {1} votes in Election {2}", dictionary.ElementAt(i).Key, dictionary.ElementAt(i).Value, electionid);
            }
            else
            {
                Console.WriteLine("{0} leads with a total of {1} votes in Election {2}!!!", dictionary.ElementAt(0).Key, dictionary.ElementAt(0).Value, electionid);
            }
            Console.WriteLine("\nPress any key to return to the previous menu...");
            Console.ReadKey(true);
        }
    }
}
