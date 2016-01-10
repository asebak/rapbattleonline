<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserOverviewStats.ascx.cs" Inherits="FreestyleOnline.Controls.Stats.UserOverviewStats" %>
<div class="panel panel-primary">
    <div class="panel-heading">
        <h3 class="panel-title"><%=this.Text("PROFILE", "OVERVIEW_STATS")%></h3>
    </div>
    <div class="panel-body">
        <div id="StatisticsTab">
            <table width="100%" cellspacing="1" cellpadding="0">
                <tr>
                    <td width="50%" class="postheader">
                        <YAF:LocalizedLabel ID="LocalizedLabel11" runat="server" LocalizedTag="joined" />
                    </td>
                    <td width="50%" class="post">
                        <asp:Label ID="Joined" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="postheader">
                        <YAF:LocalizedLabel ID="LocalizedLabel12" runat="server" LocalizedTag="lastvisit" />
                    </td>
                    <td class="post">
                        <asp:Label ID="LastVisit" runat="server" Visible="false" />
                        <YAF:DisplayDateTime ID="LastVisitDateTime" runat="server" Visible="false"></YAF:DisplayDateTime>
                    </td>
                </tr>
                <tr>
                    <td class="postheader">
                        <YAF:LocalizedLabel ID="LocalizedLabel13" runat="server" LocalizedTag="numposts" />
                    </td>
                    <td class="post" runat="server" id="Stats" />
                </tr>
                <tr id="divTF" runat="server" visible="<%# this.Get<YafBoardSettings>().EnableThanksMod %>">
                    <td class="postheader">
                        <YAF:LocalizedLabel ID="LocalizedLabel10" runat="server" LocalizedTag="THANKSFROM" />
                    </td>
                    <td class="post">
                        <asp:Label ID="ThanksFrom" runat="server" />
                        <asp:LinkButton ID="lnkThanks" runat="server" OnCommand="lnk_ViewThanks" />
                    </td>
                </tr>
                <tr id="divTTT" runat="server" visible="<%# this.Get<YafBoardSettings>().EnableThanksMod %>">
                    <td class="postheader">
                        <YAF:LocalizedLabel ID="LocalizedLabel20" runat="server" LocalizedTag="THANKSTOTIMES" />
                    </td>
                    <td class="post">
                        <asp:Label ID="ThanksToTimes" runat="server" />
                    </td>
                </tr>
                <tr id="divTTP" runat="server" visible="<%# this.Get<YafBoardSettings>().EnableThanksMod %>">
                    <td class="postheader">
                        <YAF:LocalizedLabel ID="LocalizedLabel21" runat="server" LocalizedTag="THANKSTOPOSTS" />
                    </td>
                    <td class="post">
                        <asp:Label ID="ThanksToPosts" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
