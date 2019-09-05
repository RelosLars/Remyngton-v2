using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Remyngton_v2
{
    public static class PointCalculation
    {
        public static List<double> PointDistribution = new List<double> {}; //stores which rank gets how many points

        public static Dictionary<string, double> CalculatePointsAbsolute(List<KeyValuePair<string, double>> results, bool ascending, int category) 
        {
            List<string> orderedRanking = getOrderedResults(results, ascending);

            Dictionary<string, double> pointsPerMap = new Dictionary<string, double>(); //needs another dimension, atm only stores one map
            
            for(int i = 0; i < results.Count; i++) //loops through each player
            {
                
                int index = orderedRanking.IndexOf(results[i].Key); //checks which rank(rank = index) the player got
                About.PlayerTracker[results[i].Key] += PointDistribution[index]; //gives the player the points

                pointsPerMap.Add(results[i].Key, PointDistribution[index]);
            }
            Console.WriteLine(About.PlayerTracker);
            //if(category != "misscount")
            //{
            //    Console.WriteLine(About.Players);
            //    for(int i = 0; i < About.Players.Count(); i++)
            //    {
            //        if(orderedRanking[i].Key == About.Players[]) //convert about.players to listy
            //    }
            //}
            return pointsPerMap;
        }

        public static void CalculatePointsRelative(double score1, double score2) //score1 is bigger than score2 if more = better, (eg maxcombo)
        {
            bool moreIsBetter = true; //dumb variable name, cant think of a good one
            double pointsToDistribute = 10;

            if(score1 < score2)
            {
                moreIsBetter = false;
            }
            double percentDiff;
            
            if(moreIsBetter)
            {
                percentDiff = score2 / score1;
            }
            else
            {
                percentDiff = score1 / score2;
            }


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