using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Remyngton_v2
{
    public static class AbsoluteRemyngton
    {
        public static List<double> PointDistribution = new List<double> {};

        public static void CalculatePointsAbsolute(string category, Dictionary<string, double> results, bool ascending)
        {
            List<KeyValuePair<string, double>> orderedRanking = getRanking(results, ascending);
            


            //if(category != "misscount")
            //{
            //    Console.WriteLine(About.Players);
            //    for(int i = 0; i < About.Players.Count(); i++)
            //    {
            //        if(orderedRanking[i].Key == About.Players[]) //convert about.players to listy
            //    }
            //}
            
        }

        public static List<KeyValuePair<string, double>> getRanking(Dictionary<string, double> results, bool ascending)
        {
            var list = results.ToList();
            var orderedAscending = results.OrderBy(x => x.Value); //somehow sorts the dictionary
            Console.WriteLine(orderedAscending);
            return orderedAscending.ToList();

        }
    }
}