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
                DeserializeMatch lobbyData = JsonConvert.DeserializeObject<DeserializeMatch>(MatchData());
            
            

            PointsResult pointsResult = new PointsResult();
            RemyngtonGeneral match = new RemyngtonGeneral();


            int teamType = Convert.ToInt32(lobbyData.games[0].team_type);
            if (teamType == 0) // Head to head = 0, Tag Co-op = 1, Team vs = 2, Tag Team vs = 3
            {
                teamVS = false;
            }
            else
            {
                teamVS = true;
            }

            match.ReadTotalPlayers(MatchData());
            match.CalculateAccuracies(lobbyData);
            match.CalculateMaxcombo(lobbyData);
            match.CalculateMisscount(lobbyData);
            match.CalculateScore(lobbyData);

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

        protected string MatchData()
        {
            var jsonUrlMP = "https://osu.ppy.sh/api/get_match?k=0db10863146202c12ca6f6987c98f1ec9d629421&mp=" + Request.QueryString["mp"]; //the link to the mp data in json format
            var jsonString = new WebClient().DownloadString(jsonUrlMP); //downloads the json data
            //JObject json = JObject.Parse(jsonString);   //converts json data to json object
            return jsonString;
        }

        protected void CalcScorePoints(JObject matchdata)
        {

        }
        
    }
}