#region Using

using System;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using FreestyleOnline.classes.Types;
using Microsoft.AspNet.FriendlyUrls;

#endregion

namespace FreestyleOnline
{
    public class Global : HttpApplication
    {
        #region Application

        /// <summary>
        ///     Handles the Start event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void Application_Start(object sender, EventArgs e)
        {
            //For URL Routing
            RouteTable.Routes.Add(new Route("ClientBin/{resource}.xap", new StopRoutingHandler()));
            RouteTable.Routes.Add(new Route("controls/{resource}.ascx", new StopRoutingHandler()));
            RouteTable.Routes.Add(new Route("Pages/{resource}.ascx", new StopRoutingHandler()));
            RouteTable.Routes.Add(new Route("js/{resource}.js", new StopRoutingHandler()));
            RouteTable.Routes.Add(new Route("webservices/{resource}.asmx", new StopRoutingHandler()));
            RouteTable.Routes.Add(new Route("{exclude}/{extnet}/ext.axd", new StopRoutingHandler()));
            RouteTable.Routes.Add(new Route("Mobile/{resource}.ascx", new StopRoutingHandler()));
            RouteTable.Routes.Add(new Route("Mobile/{resource}.js", new StopRoutingHandler()));
            RouteTable.Routes.Add(new Route("Mobile/{resource}.css", new StopRoutingHandler()));
            //For Web API v1
            RouteTable.Routes.MapHttpRoute("ActionApi", "api/{controller}/{action}/{id}",
                new {id = RouteParameter.Optional}
                );
            RouteTable.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional}
                );
            //For FriendlyUrls DLL
            RouteTable.Routes.EnableFriendlyUrls();
            RouteTable.Routes.MapPageRoute("hoodroute", "Community/Hoods", "~/Pages/Hood.aspx");
            RouteTable.Routes.MapPageRoute("indexroute", "", "~/Pages/Default.aspx");
            RouteTable.Routes.MapPageRoute("searchmusicroute", "Search/Music", "~/Pages/SearchMusic.aspx");
            RouteTable.Routes.MapPageRoute("chatroute", "RealTime/Chat", "~/Pages/Chat.aspx");
            RouteTable.Routes.MapPageRoute("cyphersroute", "RealTime/Cyphers", "~/Pages/Cypher.aspx");
            RouteTable.Routes.MapPageRoute("startwrittenbattleroute", "WrittenBattles/Start",
                "~/Pages/WrittenBattle.aspx");
            RouteTable.Routes.MapPageRoute("startaudiobattleroute", "AudioBattles/Start", "~/Pages/AudioBattle.aspx");
            RouteTable.Routes.MapPageRoute("mywrittenbattleroute", "WrittenBattles/Mine",
                "~/Pages/MyWrittenBattles.aspx");
            RouteTable.Routes.MapPageRoute("myaudiobattleroute", "AudioBattles/Mine", "~/Pages/MyAudioBattles.aspx");
            RouteTable.Routes.MapPageRoute("tournamentsroute", "Tournaments", "~/Pages/Tournament.aspx");
            RouteTable.Routes.MapPageRoute("writtenbattlesleaderboardroute", "Leaderboards/WrittenBattles",
                "~/Pages/WrittenLeaderboards.aspx");
            RouteTable.Routes.MapPageRoute("audiobattlesleaderboardroute", "Leaderboards/AudioBattles",
                "~/Pages/AudioLeaderboards.aspx");
            RouteTable.Routes.MapPageRoute("musicleaderboardroute", "Leaderboards/Music", "~/Pages/TopMusicTracks.aspx");
            RouteTable.Routes.MapPageRoute("helproute", "Help", "~/Pages/Help.aspx");
            RouteTable.Routes.MapPageRoute("adminroute", "Admin", "~/Pages/Admin.aspx");
            //RouteTable.Routes.MapPageRoute("hoodsroute", "Community/Hoods/{hoodId}", "~/Pages/Hoods.aspx");
            //For SignalR DLL
            RouteTable.Routes.MapHubs();
            //To Start Listening to Databse
            //SqlDependency.Start(RapHelpers.DataBaseConnection);
        }

        /// <summary>
        ///     Handles the End event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void Application_End(object sender, EventArgs e)
        {
            //To Stop Listening to Databse
            //SqlDependency.Stop(RapHelpers.DataBaseConnection);
        }

        /// <summary>
        ///     Handles the Error event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            if (ex != null)
            {
                //to avoid signal r exception for logged out users
                if (!ex.ToString().Contains("connection id"))
                {
                    if (!HttpContext.Current.IsDebuggingEnabled)
                    {
                        RapExceptionLogger.Log(ex);
                    }
                    HttpContext.Current.AddError(ex);
                    Server.Transfer("~/Pages/Error.aspx");
                }
            }
        }

        /// <summary>
        ///     Handles the Start event of the Session control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void Session_Start(object sender, EventArgs e)
        {
            //For Mobile Site
            //var httpContext = HttpContext.Current;
            //if (httpContext.Request.Browser.IsMobileDevice)
            //{
            //    var path = httpContext.Request.Url.PathAndQuery;
            //    var isOnMobilePage = path.StartsWith("/Mobile/",
            //        StringComparison.OrdinalIgnoreCase);
            //    if (!isOnMobilePage)
            //    {
            //        httpContext.Response.Redirect("~/Mobile/");
            //    }
            //}
        }

        /// <summary>
        ///     e
        ///     Handles the End event of the Session control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }

        #endregion
    }
}
