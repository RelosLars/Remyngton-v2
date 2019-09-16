using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Remyngton_v2
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateTournament_Click(object sender, EventArgs e)
        {
            string savePath = Directory.GetCurrentDirectory();
            if (FileUploadTeamlist.HasFile)
            {
                //FileUpload1.FileName = TournamentName.Text + " Team List";
                savePath = Server.MapPath("~/Tournament Teamlists/" + TournamentName.Text + " Team List.json");

                FileUploadTeamlist.SaveAs(savePath);
            }
        }
    }
}