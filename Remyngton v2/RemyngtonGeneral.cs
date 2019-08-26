using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Remyngton_v2
{
    public static class RemyngtonGeneral
    {
        public static string Score = "score";
        public static string Combo = "combo";
        public static string Misscount = "misscount";
        public static string Accuracy = "acc";

        public static void ReadPlayers(string jsonString)
        {

            DeserializeMatch lobbyData = JsonConvert.DeserializeObject<DeserializeMatch>(jsonString);

            List<KeyValuePair<string, double>> Players = new List<KeyValuePair<string, double>>(); //only temporarily used for optimazation purposes

            for (int gameNumber = 0; gameNumber < lobbyData.games.Count(); gameNumber++) // loops through each map (game)
            {
                for (int playerNumber = 0; playerNumber < lobbyData.games[gameNumber].scores.Count(); playerNumber++) //in the selected map (game) it looks through each player who played in that map
                {
                    //optimization possibility, track which users have already been added and don't request their names if they have already been added to reduce api requests.
                    var userID = lobbyData.games[gameNumber].scores[playerNumber].user_id;

                    try
                    {
                        About.PlayerTracker.Add(userID, 0); //adds userID to the list, afterwards this list will be used to fill the About.Players list with Usernames
                        //Players.Add(new KeyValuePair<string, double>(userID, 0)); 
                    }
                    catch (System.ArgumentException)
                    {
                        //if the player already exists in the list, this exception will get thrown
                    }


                }
            }
            //works
            //foreach (KeyValuePair<string, double> player in Players)
            //{
            //    var userJsonString = "https://osu.ppy.sh/api/get_user?k=0db10863146202c12ca6f6987c98f1ec9d629421&u=" + player.Key;
            //    var jsonStringUser = new WebClient().DownloadString(userJsonString); //downloads the json data of the user
            //    JArray userJsonArray = JArray.Parse(jsonStringUser);
            //    var userName = (string)userJsonArray[0].SelectToken("username");

            //    About.Players.Add(new KeyValuePair<string, double>(userName, 0)); //adds player to the dictionary where all players of a match are stored

            //}

            About.Players = Players;
        }

        public static double[,] CalculateScore(DeserializeMatch lobbyData) 
        {
            double[,] Scores = new double[About.Players.Count, lobbyData.games.Count()]; // (Player | Score/Mapnumber) 
            for(int Player = 0; Player < About.Players.Count; Player++)
            {
                for (int Mapnumber = 0; Mapnumber < lobbyData.games.Count(); Mapnumber++)
                {
                    try
                    {
                        Scores[Player, Mapnumber] = Convert.ToDouble(lobbyData.games[Mapnumber].scores[Player].score);
                    }
                    catch(Exception)
                    {
                        Scores[Player, Mapnumber] = 0;
                    }
                }
            }
            return Scores;
        }

        public static double[,] CalculateMaxcombo(DeserializeMatch lobbyData)
        {
            double[,] Maxcombos = new double[About.Players.Count, lobbyData.games.Count()]; // (Player | Score/Mapnumber) 
            for (int Player = 0; Player < About.Players.Count; Player++)
            {
                for (int Mapnumber = 0; Mapnumber < lobbyData.games.Count(); Mapnumber++)
                {
                    try
                    {
                        Maxcombos[Player, Mapnumber] = Convert.ToDouble(lobbyData.games[Mapnumber].scores[Player].maxcombo);
                    }
                    catch (Exception)
                    {
                        Maxcombos[Player, Mapnumber] = 0;
                    }
                }
            }
            return Maxcombos;
        }

        public static double[,] CalculateMisscount(DeserializeMatch lobbyData)
        {
            double[,] Misscounts = new double[About.Players.Count, lobbyData.games.Count()]; // (Player | Score/Mapnumber) 
            for (int Player = 0; Player < About.Players.Count; Player++)
            {
                for (int Mapnumber = 0; Mapnumber < lobbyData.games.Count(); Mapnumber++)
                {
                    try
                    {
                        Misscounts[Player, Mapnumber] = Convert.ToDouble(lobbyData.games[Mapnumber].scores[Player].countmiss);
                    }
                    catch (Exception)
                    {
                        Misscounts[Player, Mapnumber] = 9999;
                    }
                }
            }
            return Misscounts;
        }

        public static double[,] CalculateAccuracies(DeserializeMatch lobbyData)
        {
            About.Players = About.PlayerTracker.ToList();
            double[,] Accuracies = new double[About.Players.Count, lobbyData.games.Count()]; // (Player | Score/Mapnumber) 
            for (int Mapnumber = 0; Mapnumber < lobbyData.games.Count(); Mapnumber++) 
            {
                List<KeyValuePair<string, double>> Accs = new List<KeyValuePair<string, double>>();
                for (int Player = 0; Player < About.Players.Count; Player++)
                {
                    try
                    {

                        var countmiss = Convert.ToDouble(lobbyData.games[Mapnumber].scores[Player].countmiss);
                        var count50 = Convert.ToDouble(lobbyData.games[Mapnumber].scores[Player].count50);
                        var count100 = Convert.ToDouble(lobbyData.games[Mapnumber].scores[Player].count100);
                        var count300 = Convert.ToDouble(lobbyData.games[Mapnumber].scores[Player].count300);

                        var objectsCount = countmiss + count50 + count100 + count300;
                        var acc = ((count300 * 3) + count100 + (count50 * 0.5)) / (objectsCount * 3); //calculates acc as percentage

                        Accuracies[Player, Mapnumber] = acc;
                        string userID = lobbyData.games[Mapnumber].scores[Player].user_id;
                        Accs.Add(new KeyValuePair<string, double>(userID, acc));


                    }
                    catch (Exception)
                    {
                        Accuracies[Player, Mapnumber] = 0;
                    }
                }
                //here the scoring type gets determined
                AbsoluteRemyngton.CalculatePointsAbsolute(Accuracy, Accs, true); //still needs to do sorting

            }
            return Accuracies;
        }


    }
}