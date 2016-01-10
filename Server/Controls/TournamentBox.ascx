<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TournamentBox.ascx.cs" Inherits="FreestyleOnline.Controls.TournamentBox" %>
<div class="panel panel-success">
    <div class="panel-heading">
        <h3 class="panel-title" runat="server" id="tournamentheader"></h3>
    </div>
    <div class="panel-body">
        <div class="row clear-fix">
            <ext:Label AutoDataBind="true" ID="ChallengersLbl" Icon="User" runat="server" />
        </div>
        <div class="row clear-fix">
            <ext:Label AutoDataBind="true" ID="TournamentType" runat="server" />
        </div>
        <div class="row clear-fix">
            <ext:HyperLink AutoDataBind="true" ID="TournamentHL" Icon="ApplicationGo" Text='<%#this.Text("TOURNAMENTS", "VIEW") %>'
                NavigateUrl='<%#string.Format("/Pages/Tournaments/{0}", this.TournamentId) %>' runat="server" />
        </div>
    </div>
</div>
