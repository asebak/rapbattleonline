<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewTournaments.ascx.cs" Inherits="FreestyleOnline.Controls.Featured.NewTournaments" %>
<%@ Register TagPrefix="RAP" TagName="TournamentBox" Src="~/Controls/TournamentBox.ascx" %>

<div id="activeTournaments" class="jcarousel-skin-tango">
    <ul>
        <asp:Repeater ID="TournamentsRepeater" runat="server">
            <ItemTemplate>
                <li class="sapUiTf sapUiTfBack sapUiTfBrd sapUiTfStd sapUiTxtA">
                   <RAP:TournamentBox ID="TournamentItem" 
                                       TournamentId='<%#DataBinder.Eval(Container.DataItem, "TournamentId") %>'
                                       Status='<%#DataBinder.Eval(Container.DataItem, "TournamentStatus") %>'
                                       TotalChallengers='<%#DataBinder.Eval(Container.DataItem, "TotalChallengers") %>'
                                       Type='<%#DataBinder.Eval(Container.DataItem, "TournamentType") %>'
                                       runat="server" />
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>