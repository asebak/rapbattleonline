<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BattleLeaderboard.ascx.cs" Inherits="FreestyleOnline.Controls.Leaderboards.BattleLeaderboard" %>
<%@ Register TagPrefix="RAP" TagName="ProfileLink" Src="~/Controls/ProfileLink.ascx" %>

<table class="table table-bordered leaderboards">
    <thead>
        <tr>
            <th><%=this.Text("LEADERBOARDS", "RANK")%></th>
            <th><%=this.Text("LEADERBOARDS", "USER")%></th>
            <th><%=this.Text("LEADERBOARDS", "WINS")%></th>
            <th><%=this.Text("LEADERBOARDS", "LOSSES")%></th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater runat="server" ID="usersrankings">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("Position")%></td>
                    <td><RAP:ProfileLink ID="ProfileLink1" UserId='<%#Eval("Id")%>' runat="server"/></td>
                    <td><%#Eval("Wins")%></td>
                    <%-- TODO: Incoporate Battle Losses --%>
                    <td><%#Eval("Losses")%></td> 
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
<div class="bs-callout bs-callout-info" visible="false" id="noleaderboardstats" runat="server">
    <%=this.Text("LEADERBOARDS", "NONE")%>
</div>
