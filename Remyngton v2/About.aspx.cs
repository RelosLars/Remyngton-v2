using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Remyngton_v2
{
    public partial class About : Page
    {
        //public static List<KeyValuePair<string, double>> Players = new List<KeyValuePair<string, double>>();
        public static Dictionary<string, double> PlayerTracker = new Dictionary<string, double>(); //keeps track of all players and their points who have played a map in this lobby, player who leave stay in this dictionary
        public static bool teamVS;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DeserializeMatch lobbyData = JsonConvert.DeserializeObject<DeserializeMatch>(MatchDataJsonString());
                
            

                PointsResult pointsResult = new PointsResult();
                RemyngtonGeneral match = new RemyngtonGeneral();

                if (Tournament.CustomTeams)
                {
                    string jsonString = GetJsonStringFromFile(Tournament.TeamlistLink);
                    match.teamList = JsonConvert.DeserializeObject<DeserializeTeams>(jsonString);
                }

                Console.WriteLine(match.teamList);

                int teamType = Convert.ToInt32(lobbyData.games[0].team_type);
                if (teamType == 0) // Head to head = 0, Tag Co-op = 1, Team vs = 2, Tag Team vs = 3
                {
                    teamVS = false;
                }
                else
                {
                    teamVS = true;
                }

                match.ReadTotalPlayers(MatchDataJsonString());
                match.CalculateAccuracies(lobbyData);
                match.CalculateMaxcombo(lobbyData);
                match.CalculateMisscount(lobbyData);
                match.CalculateScore(lobbyData);

                DisplaySimplyfiedPoints(match.GetSimplifiedPoints(RemyngtonGeneral.pointHistory)); 
                
                Console.WriteLine(PlayerTracker);
                if (lobbyData.match.end_time != null) //if the match has ended
                {
                    ArchiveMatch(RemyngtonGeneral.pointHistory, lobbyData.match.match_id, lobbyData.match.name);
                }

                PlayerTracker = new Dictionary<string, double>(); //clears values from playertracker

            }
            catch (JsonSerializationException)
            {
                Response.Redirect("Default.aspx");
            }
        }

        public string GetJsonStringFromFile(string path)
        {
            string jsonString = "";
            StreamReader sr = new StreamReader(path);
            jsonString = sr.ReadToEnd();
            return jsonString;
        }

        private void ArchiveMatch(PointsResult pointHistory, string matchNumber, string matchName)
        {
            var jsonString = JsonConvert.SerializeObject(pointHistory);
            var test = jsonString.Replace(@"\", "");
            //JObject jsonObject = new JObject(pointHistory);


            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(Server.MapPath($"~/Past Matches/{ matchNumber } {matchName}.json")))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, pointHistory);
            }

            Console.WriteLine();
        }

        protected string MatchDataJsonString()
        {
            var jsonUrlMP = "https://osu.ppy.sh/api/get_match?k=0db10863146202c12ca6f6987c98f1ec9d629421&mp=" + Request.QueryString["mp"]; //the link to the mp data in json format
            var jsonString = new WebClient().DownloadString(jsonUrlMP); //downloads the json data
            //JObject json = JObject.Parse(jsonString);   //converts json data to json object
            return jsonString;
        }

        protected void DisplaySimplyfiedPoints(SimplifiedPoints points)
        {
            //on page refresh the html code gets duplicated for some reason, pls fix
            HtmlGenericControl divcontrol = new HtmlGenericControl();
            //divcontrol.Attributes["class"] = "some class";
            divcontrol.TagName = "div";
            for (int beatmap = 0; beatmap < points.beatmap.Count; beatmap++)
            {
                
                HtmlTable MapResultTable = new HtmlTable();
                Label MapName = new Label();

                // Set the table's formatting-related properties.
                MapResultTable.Border = 1;
                MapResultTable.CellPadding = 3;
                MapResultTable.CellSpacing = 3;
                MapResultTable.BorderColor = "black";

                MapResultTable.ID = "Map " + beatmap.ToString();

                // Start adding content to the table.
                HtmlTableRow row;
                HtmlTableCell cell;
                for (int i = -2; i < points.beatmap[beatmap].Participant.Count; i++)
                {
                    // Create a new row and set its background color.
                    row = new HtmlTableRow();
                    if(i == -2)
                    {
                        
                        MapName.Text = points.beatmap[beatmap].beatmapName;

                    }
                    else if(i == -1)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            cell = new HtmlTableCell();
                            if (j == 0)
                            {
                                cell.InnerHtml = "Team name";
                            }
                            else
                            {
                                cell.InnerHtml = "Total Points";
                            }

                            row.Cells.Add(cell);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            cell = new HtmlTableCell();
                            if (j == 0)
                            {
                                cell.InnerHtml = points.beatmap[beatmap].Participant[i].name;
                            }
                            else
                            {
                                cell.InnerHtml = points.beatmap[beatmap].Participant[i].totalPoints;
                            }



                            row.Cells.Add(cell);
                        }
                    }
                    

                    // Add the row to the table.
                    if(row != null)
                    {
                        MapResultTable.Rows.Add(row);
                    }
                    
                }

                

                // Add the table to the page.
                divcontrol.Controls.Add(MapName);
                divcontrol.Controls.Add(MapResultTable);
                divcontrol.Controls.Add(new Literal() { ID = "row" + beatmap, Text = "<hr/>" }); //adds an extra linebreak between table
                this.Controls.Add(divcontrol);
                
            }
        }
        
    }
}