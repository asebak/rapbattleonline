using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using YAF.Classes;
using YAF.Classes.Data;
using YAF.Core;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.Interfaces;
using YAF.Utilities;
using YAF.Utils;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

namespace FreestyleOnline.forum.controls
{
    public partial class EditNewsFeed : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        //protected void DeleteNewsButtonID_Command(object sender, CommandEventArgs e)
        //{
        //    int rows;
        //    string NewsID = e.CommandArgument.ToString();

        //    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["yafnet"].ToString());

        //    connection.Open();

        //    SqlCommand command = new SqlCommand("DELETE FROM rap_NewsFeed WHERE NewsFeedID = @NewsFeedID", connection);
        //    command.Parameters.Add("@NewsFeedID", System.Data.SqlDbType.Int);
        //    command.Parameters["@NewsFeedID"].Value = NewsID;
        //    rows = command.ExecuteNonQuery();

        //    connection.Close();

        //    YafBuildLink.Redirect(ForumPages.admin_admin);
        //}

        protected void PostNewsButton_Click(object sender, EventArgs e)
        {
            int rows;
            TextBox title = EditNewsFeedID.Controls[EditNewsFeedID.Controls.Count - 1].FindControl("titletextboxid") as TextBox;
            TextBox news = EditNewsFeedID.Controls[EditNewsFeedID.Controls.Count - 1].FindControl("newscontentid") as TextBox;


            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["yafnet"].ToString());
            
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO rap_NewsFeed(UserID, Title, DatePosted, Information)VALUES(@UserID,@Title,@DatePosted,@Information)",connection);
            command.Parameters.Add("@UserID", System.Data.SqlDbType.Int);
            command.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar,50);
            command.Parameters.Add("@DatePosted", System.Data.SqlDbType.DateTime);
            command.Parameters.Add("@Information", System.Data.SqlDbType.NVarChar,int.MaxValue);
            command.Parameters["@UserID"].Value = this.PageContext.PageUserID;
            command.Parameters["@Title"].Value = title.Text;
            command.Parameters["@DatePosted"].Value = DateTime.Now;
            command.Parameters["@Information"].Value = news.Text;

            rows = command.ExecuteNonQuery();

            connection.Close();

            YafBuildLink.Redirect(ForumPages.admin_admin);
        }
    }
}