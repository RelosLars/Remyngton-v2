using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace Remyngton_v2
{
    public partial class Contact : Page
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateTournament_Click(object sender, EventArgs e)
        {
            string savePath;

            string insertStatement = $"insert into tbl_Tournaments(TournamentName, StartDate, TeamlistLink, MappoolLink) \n values(@TournamentName, @StartDate, @savePath, @Mappool)";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(insertStatement, con);
            cmd.Parameters.AddWithValue("@TournamentName", TournamentName.Text);
            cmd.Parameters.AddWithValue("@StartDate", StartDate.SelectedDate);

            if (FileUploadTeamlist.HasFile)
            {
                savePath = Server.MapPath("~/Tournament Teamlists/" + TournamentName.Text + " Team List.json");
                cmd.Parameters.AddWithValue("@savePath", savePath);
                FileUploadTeamlist.SaveAs(savePath);
            }
            else
            {
                cmd.Parameters.AddWithValue("@savePath", "NULL");
            }

            if (FileUploadMappool.HasFile)
            {
                savePath = Server.MapPath("~/Tournament Mappools/" + TournamentName.Text + " Mappool.json");
                cmd.Parameters.AddWithValue("@Mappool", savePath);
                FileUploadMappool.SaveAs(savePath);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Mappool", "NULL");
            }

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                
            }
            catch (ArgumentNullException)
            {

            }
        }
    }
}