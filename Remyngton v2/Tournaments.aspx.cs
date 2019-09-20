using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Remyngton_v2
{
    public partial class Contact : Page
    {
        public string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Lars Soler\Source\Repos\Remyngton-v2\Remyngton v2\App_Data\Remyngton.mdf; Integrated Security = True";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateTournament_Click(object sender, EventArgs e)
        {
            string savePath;
            if (FileUploadTeamlist.HasFile)
            {
                //FileUpload1.FileName = TournamentName.Text + " Team List";
                savePath = Server.MapPath("~/Tournament Teamlists/" + TournamentName.Text + " Team List.json");

                
                string insertStatement = $"insert into tbl_Tournaments(TournamentName, StartDate, TeamlistLink) \n values( '{TournamentName.Text}', @StartDate, '{savePath}')";
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(insertStatement, con);

                cmd.Parameters.AddWithValue("@StartDate", StartDate.SelectedDate);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    FileUploadTeamlist.SaveAs(savePath);
                }
                catch(Exception)
                {

                }
                
            }

            if(FileUploadMappool.HasFile)
            {
                savePath = Server.MapPath("~/Tournament Mappools/" + TournamentName.Text + " Mappool.json");
                string insertStatement = $"update tbl_Tournaments \n set MappoolLink='{savePath}' \n where TournamentName='{TournamentName.Text}'";

                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(insertStatement, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                FileUploadMappool.SaveAs(savePath);
            }


        }
    }
}