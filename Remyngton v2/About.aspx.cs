using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
                ArchiveMatch(lobbyData);
            }

            PlayerTracker = new Dictionary<string, double>(); //clears values from playertracker
        }

        private void ArchiveMatch(DeserializeMatch lobbyData)
        {
            
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