using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Remyngton_v2
{
    public partial class DetailedPointsView : System.Web.UI.Page
    {
        public static PointsResult pointHistory { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(pointHistory == null)
            {
                Response.Redirect("About.aspx");
            }

        }

        public void ShowDetailedPoints()
        {
            ////on page refresh the html code gets duplicated for some reason, pls fix
            //HtmlGenericControl divcontrol = new HtmlGenericControl();
            ////divcontrol.Attributes["class"] = "some class";
            //divcontrol.TagName = "div";

            //HtmlTable TotalPointsTable = new HtmlTable();

            //TotalPointsTable.Border = 1;
            //TotalPointsTable.CellPadding = 3;
            //TotalPointsTable.CellSpacing = 3;
            //TotalPointsTable.BorderColor = "black";

            //Label TotalPointsLabel = new Label();
            //TotalPointsLabel.Text = "Total Points";

            //HtmlTableRow row1;
            //HtmlTableCell cell1;
            
            //for (int i = -1; i < pointHistory.map.Count; i++) 
            //{
            //    row1 = new HtmlTableRow();
            //    if (i == -1)
            //    {
            //        for (int j = 0; j < 2; j++)
            //        {
            //            cell1 = new HtmlTableCell();
            //            if (j == 0)
            //            {
            //                cell1.InnerHtml = "Team name";
            //            }
            //            else
            //            {
            //                cell1.InnerHtml = "Total Points";
            //            }

            //            row1.Cells.Add(cell1);
            //        }
            //    }
            //    else
            //    {
            //        for (int j = 0; j < 2; j++)
            //        {
            //            cell1 = new HtmlTableCell();
            //            if (j == 0)
            //            {
            //                cell1.InnerHtml = PlayerTrackerList[i].Key;
            //            }
            //            else
            //            {
            //                cell1.InnerHtml = PlayerTrackerList[i].Value.ToString();
            //            }
            //            row1.Cells.Add(cell1);
            //        }
            //    }

            //    if (row1 != null)
            //    {
            //        TotalPointsTable.Rows.Add(row1);
            //    }
            //}
            //divcontrol.Controls.Add(TotalPointsLabel);
            //divcontrol.Controls.Add(TotalPointsTable);


            //for (int beatmap = 0; beatmap < points.beatmap.Count; beatmap++)
            //{

            //    HtmlTable MapResultTable = new HtmlTable();
            //    Label MapName = new Label();

            //    // Set the table's formatting-related properties.
            //    MapResultTable.Border = 1;
            //    MapResultTable.CellPadding = 3;
            //    MapResultTable.CellSpacing = 3;
            //    MapResultTable.BorderColor = "black";

            //    MapResultTable.ID = "Map " + beatmap.ToString();

            //    // Start adding content to the table.
            //    HtmlTableRow row;
            //    HtmlTableCell cell;
            //    for (int i = -2; i < points.beatmap[beatmap].Participant.Count; i++)
            //    {
            //        // Create a new row and set its background color.
            //        row = new HtmlTableRow();
            //        if (i == -2)
            //        {

            //            MapName.Text = points.beatmap[beatmap].beatmapName;

            //        }
            //        else if (i == -1)
            //        {
            //            for (int j = 0; j < 2; j++)
            //            {
            //                cell = new HtmlTableCell();
            //                if (j == 0)
            //                {
            //                    cell.InnerHtml = "Team name";
            //                }
            //                else
            //                {
            //                    cell.InnerHtml = "Points";
            //                }

            //                row.Cells.Add(cell);
            //            }
            //        }
            //        else
            //        {
            //            for (int j = 0; j < 2; j++)
            //            {
            //                cell = new HtmlTableCell();
            //                if (j == 0)
            //                {
            //                    cell.InnerHtml = points.beatmap[beatmap].Participant[i].name;
            //                }
            //                else
            //                {
            //                    cell.InnerHtml = points.beatmap[beatmap].Participant[i].totalPoints;
            //                }



            //                row.Cells.Add(cell);
            //            }
            //        }


            //        // Add the row to the table.
            //        if (row != null)
            //        {
            //            MapResultTable.Rows.Add(row);
            //        }

            //    }



            //    // Add the table to the page.
            //    divcontrol.Controls.Add(MapName);
            //    divcontrol.Controls.Add(MapResultTable);
            //    divcontrol.Controls.Add(new Literal() { ID = "row" + beatmap, Text = "<hr/>" }); //adds an extra linebreak between table
            //    this.Controls.Add(divcontrol);

            //}
        }
    }
}