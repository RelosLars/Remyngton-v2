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
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> tournamentList = new List<string>() {"Remyngton", "OSCT 2018", "Switzerlan" }; //examples
            
            Tournaments.DataSource = tournamentList;
            Tournaments.DataBind();
        }


        protected void GetMatch(object sender, EventArgs e)
        {
            var url = new Uri(MpLink.Text);
            var mpID = url.Segments.Last(); //takes the last substring from the url (in this case the multiplayer ID)

            List<double> Points = new List<double>();

            int maxPlayers;

            if(PlayerCount.Text == "")
            {
                maxPlayers = 16;
            }
            else
            {
                maxPlayers = Convert.ToInt32(PlayerCount.Text);
            }
            //0-1-2-3  +1
            //5 - 7 - 9 - 11 +2
            //14 - 17 - 20 - 23 +3 
            //27 - 31 - 35 - 39 +4

            for (double i = 0; i < maxPlayers; i++)
            {
                double points = 0;
                //double stage = 1;
                double test = i;
                while (test != 0)
                {
                    if (test >= 12)
                    {
                        points += 4;
                        test -= 1;
                    }
                    else if (test >= 8)
                    {
                        points += 3;
                        test -= 1;
                    }
                    else if (test >= 4)
                    {
                        points += 2;
                        test -= 1;
                    }
                    else if (test < 4)
                    {
                        points += 1;
                        test -= 1;
                    }
                }

                Console.WriteLine($"Points: {points}");
                Points.Add(points);
                //Console.WriteLine(Points);
                //Console.WriteLine(i);

            }
            PointCalculation.PointDistribution = Points;

            //puts the multiplayer ID and the player number in the query string so that the values are accessible in the other page
            Response.Redirect($"About.aspx?mp={mpID}");

        }

        protected void isTournament_CheckedChanged(object sender, EventArgs e)
        {
            if (isTournament.Checked)
            {
                PlayerCount.Visible = true;
                Tournaments.Visible = true;
            }
            else
            {
                PlayerCount.Visible = false;
                Tournaments.Visible = false;
            }
        }
    }
}