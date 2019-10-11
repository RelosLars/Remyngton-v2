using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Remyngton_v2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public string connectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            connectionString = $@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {Server.MapPath("\\App_Data\\Remyngton.mdf")}; Integrated Security = True";
            List<string> tournamentList = new List<string>();

            string selectStatement = "select TournamentName from tbl_Tournaments";

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(selectStatement, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                tournamentList.Add(reader["TournamentName"].ToString());
            }
            con.Close();
            Tournament.DataSource = tournamentList;
            Tournament.DataBind();

        }

        protected void EditTournament_Click(object sender, EventArgs e)
        {
            string savePath;
            string updateStatement = $"UPDATE tbl_Tournaments SET TournamentName =@tournamentname WHERE TournamentName = '{Tournament.SelectedValue}'";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(updateStatement, con);

            cmd.Parameters.AddWithValue("@tournamentname", TournamentName.Text);

            if (FileUploadTeamlist.HasFile)
            {
                savePath = Server.MapPath("~/Tournament Teamlists/" + TournamentName.Text + " Team List.json");
                FileUploadTeamlist.SaveAs(savePath);
            }

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            //while (reader.Read())
            //{
            //    tournamentList.Add(reader["TournamentName"].ToString());
            //}
            con.Close();
        }
    }
}