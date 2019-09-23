using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;

namespace Remyngton_v2
{
    public class RemyngtonGeneral
    {
        public static PointsResult pointHistory = new PointsResult();
        enum Category { Score, Maxcombo, Misscount, Accuracy };
        //StreamReader sr = new StreamReader(FileLoc);
        

        public DeserializeTeams teamList { get; set; }

        

        public void ReadTotalPlayers(string jsonString)
        {
            
            DeserializeMatch lobbyData = JsonConvert.DeserializeObject<DeserializeMatch>(jsonString);

            List<KeyValuePair<string, double>> Players = new List<KeyValuePair<string, double>>(); //only temporarily used for optimazation purposes

            for (int gameNumber = 0; gameNumber < lobbyData.games.Count(); gameNumber++) // loops through each map (game)
            {
                Map map = new Map();
                map.beatmap_id = lobbyData.games[gameNumber].beatmap_id;

                for (int playerNumber = 0; playerNumber < lobbyData.games[gameNumber].scores.Count(); playerNumber++) //in the selected map (game) it looks through each player who played in that map
                {
                    //optimization possibility, track which users have already been added and don't request their names if they have already been added to reduce api requests.
                    var userID = lobbyData.games[gameNumber].scores[playerNumber].user_id;

                    try
                    {

                        //these 3 codelines need to happen before adding the users to the playertracker, because that will throw an exception if the user already exists and thus skip the rest of the try block
                        User user = new User();
                        user.user_id = userID;
                        map.users.Add(user);

                        About.PlayerTracker.Add(userID, 0); //adds userID to the list, afterwards this list will be used to fill the About.Players list with Usernames

                        


                        


                        //Players.Add(new KeyValuePair<string, double>(userID, 0)); f
                    }
                    catch (System.ArgumentException)
                    {
                        //if the player already exists in the list, this exception will get thrown
                    }


                }
                pointHistory.map.Add(map);
                
            }

            //converts userID to username
            //foreach (KeyValuePair<string, double> player in Players)
            //{
            //    var userJsonString = "https://osu.ppy.sh/api/get_user?k=0db10863146202c12ca6f6987c98f1ec9d629421&u=" + player.Key;
            //    var jsonStringUser = new WebClient().DownloadString(userJsonString); //downloads the json data of the user
            //    JArray userJsonArray = JArray.Parse(jsonStringUser);
            //    var userName = (string)userJsonArray[0].SelectToken("username");

            //    About.Players.Add(new KeyValuePair<string, double>(userName, 0)); //adds player to the dictionary where all players of a match are stored

            //}

        }

        public Dictionary<string, double> ReadCurrentMapPlayers(string jsonString, int mapnumber)
        {
            Dictionary<string, double> currentMapPlayers = new Dictionary<string, double>();
            DeserializeMatch lobbyData = JsonConvert.DeserializeObject<DeserializeMatch>(jsonString);
            for (int playerNumber = 0; playerNumber < lobbyData.games[mapnumber].scores.Count(); playerNumber++) //in the selected map (game) it looks through each player who played in that map
            {
                //optimization possibility, track which users have already been added and don't request their names if they have already been added to reduce api requests.
                var userID = lobbyData.games[mapnumber].scores[playerNumber].user_id;

                try
                {
                    currentMapPlayers.Add(userID, 0); //adds userID to the list, afterwards this list will be used to fill the About.Players list with Usernames
                                                        //Players.Add(new KeyValuePair<string, double>(userID, 0)); 
                }
                catch (System.ArgumentException)
                {
                    //if the player already exists in the list, this exception will get thrown
                }


            }
            
            return currentMapPlayers;
        }

        public void CalculateScore(DeserializeMatch lobbyData)
        {
            //double[,] Scores = new double[About.PlayerTracker.Count, lobbyData.games.Count()]; // (Player | Score/Mapnumber) 
            for (int Mapnumber = 0; Mapnumber < lobbyData.games.Count(); Mapnumber++)
            {
                List<KeyValuePair<string, double>> scores = new List<KeyValuePair<string, double>>();
                List<KeyValuePair<string, double>> teamScores = new List<KeyValuePair<string, double>>();
                //calculate players
                for (int Player = 0; Player < About.PlayerTracker.Count; Player++)
                {
                    try
                    {
                        
                        //Scores[Player, Mapnumber] = Convert.ToDouble(lobbyData.games[Mapnumber].scores[Player].score);
                        var score = Convert.ToDouble(lobbyData.games[Mapnumber].scores[Player].score);

                        string userID = lobbyData.games[Mapnumber].scores[Player].user_id;
                        scores.Add(new KeyValuePair<string, double>(userID, score));


                        if (Tournament.CustomTeams)
                        {
                            for (int i = 0; i < teamList.Teams.Length; i++)
                            {
                                if (teamList.Teams[i].Player1 == userID || teamList.Teams[i].Player2 == userID)
                                {
                                    teamList.Teams[i].TeamScore += score;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        //Scores[Player, Mapnumber] = 0;
                    }
                }
                foreach(var team in teamList.Teams)
                {
                    teamScores.Add(new KeyValuePair<string, double>(team.Teamname, team.TeamScore));
                }

                List<KeyValuePair<string, double>> points;
                if (Tournament.CustomTeams)
                {
                    points = PointCalculation.CalculatePointsAbsolute(teamScores, true, (int)Category.Score).ToList(); //gets the points of each player for that specific map and puts that in the pointHistory object
                }
                else
                {
                    points = PointCalculation.CalculatePointsAbsolute(scores, true, (int)Category.Score).ToList(); //gets the points of each player for that specific map and puts that in the pointHistory object
                }
                Console.WriteLine(points);
                if (About.teamVS)
                {
                    double blue = 0;
                    double red = 0;
                    foreach (var score in scores)
                    {
                        blue += score.Value;
                    }
                    foreach (var score in scores)
                    {
                        red += score.Value;
                    }
                    if (red > blue) //this is to make sure that the first value is always the bigger one (in case of misscount and acc its the smaller one)
                    {
                        PointCalculation.CalculatePointsRelative(red, blue);
                    }
                    else
                    {
                        PointCalculation.CalculatePointsRelative(blue, red);
                    }

                }
                else
                {
                    for (int i = 0; i < points.Count; i++)
                    {
                        pointHistory.map[Mapnumber].users[i].scorePoints = points[i].Value.ToString();
                    }
                }
            }
        }

        public void CalculateMaxcombo(DeserializeMatch lobbyData)
        {
            //double[,] Maxcombos = new double[About.PlayerTracker.Count, lobbyData.games.Count()]; // (Player | Score/Mapnumber) 
            for (int Mapnumber = 0; Mapnumber < lobbyData.games.Count(); Mapnumber++)
            {
                List<KeyValuePair<string, double>> combos = new List<KeyValuePair<string, double>>();
                //calculate players
                for (int Player = 0; Player < About.PlayerTracker.Count; Player++)
                {
                    try
                    {
                        //Maxcombos[Player, Mapnumber] = Convert.ToDouble(lobbyData.games[Mapnumber].scores[Player].maxcombo);
                        var maxcombo = Convert.ToDouble(lobbyData.games[Mapnumber].scores[Player].maxcombo);

                        string userID = lobbyData.games[Mapnumber].scores[Player].user_id;
                        combos.Add(new KeyValuePair<string, double>(userID, maxcombo));
                    }
                    catch (Exception)
                    {
                        //Maxcombos[Player, Mapnumber] = 0;
                    }
                }
                var points = PointCalculation.CalculatePointsAbsolute(combos, true, (int)Category.Maxcombo).ToList();

                if (About.teamVS)
                {
                    double blue = 0;
                    double red = 0;
                    foreach (var score in combos)
                    {
                        blue += score.Value;
                    }
                    foreach (var score in combos)
                    {
                        red += score.Value;
                    }
                    if (red > blue) //this is to make sure that the first value is always the bigger one (in case of misscount and acc its the smaller one)
                    {
                        PointCalculation.CalculatePointsRelative(red, blue);
                    }
                    else
                    {
                        PointCalculation.CalculatePointsRelative(blue, red);
                    }

                }
                else
                {

                    for (int i = 0; i < points.Count; i++)
                    {
                        pointHistory.map[Mapnumber].users[i].maxcomboPoints = points[i].Value.ToString();
                    }
                }
            }
        }

        public void CalculateMisscount(DeserializeMatch lobbyData)
        {
            //double[,] Misscounts = new double[About.PlayerTracker.Count, lobbyData.games.Count()]; // (Player | Score/Mapnumber) 
            for (int Mapnumber = 0; Mapnumber < lobbyData.games.Count(); Mapnumber++)
            {
                List<KeyValuePair<string, double>> Misses = new List<KeyValuePair<string, double>>();
                //calculate players
                for (int Player = 0; Player < About.PlayerTracker.Count; Player++)
                {
                    try
                    {
                        //Misscounts[Player, Mapnumber] = Convert.ToDouble(lobbyData.games[Mapnumber].scores[Player].countmiss);

                        var misscount = Convert.ToDouble(lobbyData.games[Mapnumber].scores[Player].countmiss);

                        string userID = lobbyData.games[Mapnumber].scores[Player].user_id;
                        Misses.Add(new KeyValuePair<string, double>(userID, misscount));
                    }
                    catch (Exception)
                    {
                        //Misscounts[Player, Mapnumber] = 9999;
                    }
                }
                var points = PointCalculation.CalculatePointsAbsolute(Misses, false, (int)Category.Misscount).ToList(); //false because in the case of misscounts lower = better

                if (About.teamVS)
                {
                    double blue = 0;
                    double red = 0;
                    foreach (var score in Misses)
                    {
                        blue += score.Value;
                    }
                    foreach (var score in Misses)
                    {
                        red += score.Value;
                    }
                    if (blue > red) //this is to make sure that the first value is always the bigger one (in case of misscount and acc its the smaller one)
                    {
                        PointCalculation.CalculatePointsRelative(red, blue);
                    }
                    else
                    {
                        PointCalculation.CalculatePointsRelative(blue, red);
                    }

                }
                else
                {
                    for (int i = 0; i < points.Count; i++)
                    {
                        pointHistory.map[Mapnumber].users[i].countmissPoints = points[i].Value.ToString();
                    }
                }
            }
        }

        
        public void CalculateAccuracies(DeserializeMatch lobbyData)
        {
            //About.Players = About.PlayerTracker.ToList();

            //double[,] Accuracies = new double[About.PlayerTracker.Count, lobbyData.games.Count()]; // (Player | Score/Mapnumber) 
            for (int Mapnumber = 0; Mapnumber < lobbyData.games.Count(); Mapnumber++) 
            {
                List<KeyValuePair<string, double>> Accs = new List<KeyValuePair<string, double>>();
                /* Inaccuray points work like follows:
                misscount * 3
                50 count * 2
                100 count *1
                sum of those is inaccuracy points. Done to make the acc value absolute and not exponential (diff from 95 to 100 bigger than 70 to 75), makes relativ points calculation easier
                 */
                List<KeyValuePair<string, double>> InaccuracyPointsBlue = new List<KeyValuePair<string, double>>(); 
                List<KeyValuePair<string, double>> InaccuracyPointsRed = new List<KeyValuePair<string, double>>();
                //calculate players
                for (int Player = 0; Player < About.PlayerTracker.Count; Player++)
                {
                    try
                    {

                        var countmiss = Convert.ToDouble(lobbyData.games[Mapnumber].scores[Player].countmiss);
                        var count50 = Convert.ToDouble(lobbyData.games[Mapnumber].scores[Player].count50);
                        var count100 = Convert.ToDouble(lobbyData.games[Mapnumber].scores[Player].count100);
                        var count300 = Convert.ToDouble(lobbyData.games[Mapnumber].scores[Player].count300);

                        var objectsCount = countmiss + count50 + count100 + count300;
                        var acc = ((count300 * 3) + count100 + (count50 * 0.5)) / (objectsCount * 3); //calculates acc as percentage

                        //Accuracies[Player, Mapnumber] = acc;
                        string userID = lobbyData.games[Mapnumber].scores[Player].user_id;
                        if (About.teamVS)
                        {
                            var inaccuracyPoints = (countmiss * 3) + (count50 * 2) + count100;
                            if(lobbyData.games[Mapnumber].scores[Player].team == "1")
                            {
                                InaccuracyPointsBlue.Add(new KeyValuePair<string, double>(userID, inaccuracyPoints));
                            }
                            else
                            {
                                InaccuracyPointsRed.Add(new KeyValuePair<string, double>(userID, inaccuracyPoints));
                            }
                        }
                        else
                        {
                            Accs.Add(new KeyValuePair<string, double>(userID, acc));
                        }
                        


                    }
                    catch (Exception)
                    {
                        //Accuracies[Player, Mapnumber] = 0; //accuracies may not be needed, calculation still needs to get done in other categories.
                    }
                }
                //here the scoring type gets determined, needs to still be implemented in scores/maxcombo/misscount
                if(About.teamVS)
                {
                    double blue = 0;
                    double red = 0;
                    foreach (var score in InaccuracyPointsBlue)
                    {
                        blue += score.Value;
                    }
                    foreach (var score in InaccuracyPointsRed)
                    {
                        red += score.Value;
                    }
                    if(blue > red) //this is to make sure that the first value is always the bigger one (in case of misscount and acc its the smaller one)
                    {
                        PointCalculation.CalculatePointsRelative(red, blue);
                    }
                    else
                    {
                        PointCalculation.CalculatePointsRelative(blue, red);
                    }
                    
                }
                else
                {
                    var points = PointCalculation.CalculatePointsAbsolute(Accs, true, (int)Category.Accuracy).ToList();

                    for (int i = 0; i < points.Count; i++)
                    {
                        pointHistory.map[Mapnumber].users[i].accPoints = points[i].Value.ToString();
                    }
                }
            }
        }
    }
}