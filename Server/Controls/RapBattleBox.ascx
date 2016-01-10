<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RapBattleBox.ascx.cs" Inherits="FreestyleOnline.Controls.RapBattleBox" %>
<%@ Import Namespace="Common.Types" %>
<%@ Register TagPrefix="RAP" TagName="CountDown" Src="~/Controls/CountDown.ascx" %>
<%@ Register TagPrefix="RAP" TagName="UserLink" Src="~/Controls/Generic/UserLink.ascx" %>

<script type="text/javascript">
    $(function () {
        createBattleRating('<%=this.BattleId%><%=this.Type%>', '<%=(this.User1Overall != null).ToString().ToLower()%>', '<%=(this.User2Overall != null).ToString().ToLower()%>', '<%=(this.User1Overall ?? 0)%>', '<%=this.User2Overall ?? 0%>');
    });
</script>
<div class="panel panel-default">
    <div class="panel-body">
        <div class="row clearfix">
            <div class="col-md-4 column">
                <RAP:UserLink ID="ProfileLink" runat="server" UserID='<%# (int)this.UserId1 %>' BlankTarget="true" />
            </div>
            <div class="col-md-4 column">
                <ext:Label ID="Label1" runat="server" Text='<%#this.Text("COMMON", "VS") %>' />
            </div>
            <div class="col-md-4 column">
                <RAP:UserLink ID="UserLink1" runat="server" UserID='<%# this.UserId2 ?? -1 %>' BlankTarget="true" />
                <label runat="server" visible='<%#this.UserId2 == null%>'><%=this.Text("BATTLES", "BATTLE_NOCHALLENGER")%></label>
            </div>
        </div>
        <div class="row clearfix">
            <div class="col-md-4 column">
                <div id="user1rating<%#this.BattleId%><%#this.Type%>"></div>
            </div>
            <div class="col-md-4 column">
            </div>
            <div class="col-md-4 column">
                <div id="user2rating<%#this.BattleId%><%#this.Type%>"></div>
            </div>
        </div>
        <div class="row clearfix">
            <RAP:CountDown ID="CountDown1" runat="server" TimeToEnd='<%#this.EndDate %>' Visible='<%#!RapGlobalHelpers.IsDateExpired(this.EndDate) %>' />
            <ext:Label ID="BattleStatusLabel" Icon='<%#RapGlobalHelpers.IsDateExpired(this.EndDate.AddDays(this.VoteDays))  ? Icon.Stop : Icon.Star %>'
                Text='<%#RapGlobalHelpers.IsDateExpired(this.EndDate.AddDays(this.VoteDays)) ? this.Text("BATTLES", "BATTLE_ENDED") : this.Text("BATTLES", "BATTLE_VOTETIME") %>'
                Visible='<%#RapGlobalHelpers.IsDateExpired(this.EndDate) %>' runat="server" />
        </div>
        <ext:HyperLink ID="BattleHyperLink" Icon="ApplicationGo" Text='<%#this.Text("BATTLES", "BATTLE_VIEW") %>' NavigateUrl='<%#this.BattleUrl %>' runat="server" />
    </div>
</div>
