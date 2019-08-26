using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Remyngton_v2
{
    public static class AbsoluteRemyngton
    {
        public static List<double> PointDistribution = new List<double> {}; //stores which rank gets how many points

        public static void CalculatePointsAbsolute(string category, List<KeyValuePair<string, double>> results, bool ascending)
        {
            List<string> orderedRanking = getOrderedResults(results, ascending);

            Console.WriteLine(orderedRanking);
            Console.WriteLine(About.Players);

            for(int i = 0; i < results.Count; i++)
            {
                
                int index = orderedRanking.IndexOf(results[i].Key); //checks which rank(rank = index) the player got
                About.PlayerTracker[results[i].Key] = PointDistribution[index]; //gives the player the points
                Console.WriteLine(About.PlayerTracker);

            }
            Console.WriteLine(About.PlayerTracker); //calc acc works but wird immer überschrieben, liegt vermutlich an RemyngtonGeneral 122
            //if(category != "misscount")
            //{
            //    Console.WriteLine(About.Players);
            //    for(int i = 0; i < About.Players.Count(); i++)
            //    {
            //        if(orderedRanking[i].Key == About.Players[]) //convert about.players to listy
            //    }
            //}

        }

        public static List<string> getOrderedResults(List<KeyValuePair<string, double>> results, bool ascending)
        {
            //var list = results.ToList();
            List<string> returnList = new List<string>();
            var orderedAscending = results.OrderBy(x => x.Value).ToList(); //somehow sorts the list
            Console.WriteLine(orderedAscending);
            foreach(var player in orderedAscending)
            {
                returnList.Add(player.Key);
            }
            return returnList;

        }
    }
}