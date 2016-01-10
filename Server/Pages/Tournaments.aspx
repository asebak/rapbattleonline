<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tournaments.aspx.cs" Inherits="FreestyleOnline.Pages.Tournaments" %>

<%@ Import Namespace="YAF.Types.Extensions" %>
<%@ Import Namespace="Common.Types.Enums" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<link href="/forum/resources/css/jquery.hovercard.css" rel="Stylesheet" type="text/css" />--%>
    <%--<script type="text/javascript" src="/forum/resources/js/jquery.hovercard.js"></script>--%>
    <script type="text/javascript">
        $(function () {
            initializeTournamentBracketUI();
            createUserviewTournament('<%= this.PageContext.PageUserName %>');
            //TODO: Incoporate view battle button on hover
            //$('#tournamentTable tr').each(function () {
            //    var vsTag = $(this).find(".vs");
            //    vsTag.hover(function () {
            //        $(this).find('.viewbattlebtn').show();
            //    }, function () {
            //        $(this).find('.viewbattlebtn').hide();
            //    });
            //});



            //TODO: Incorporate hover cards
            //var hoverHTMLDemoBasic =
            //    'John Resig is an application developer at Khan Academy';
            //$('#tournamentTable tr').each(function () {
            //    var userLabel = $(this).find(".team").html();
            //    if (userLabel) {
            //        debugger;
            //        userLabel.hovercard({
            //            detailsHTML: hoverHTMLDemoBasic,
            //            width: 400,
            //            cardImgSrc: 'http://ejohn.org/files/short.sm.jpg'
            //        });
            //    }
            //});
        });
    </script>
    <div class="bs-callout bs-callout-danger" visible="false" id="tournamentdescription" runat="server">
        <%= this.Text("TOURNAMENTS", "DESCRIPTION").FormatWith((this.Tournament.TournamentType == RapBattleType.Audio) ? 
    this.Text("BATTLES", "AUDIO") : this.Text("BATTLES", "WRITTEN"),
    this.Tournament.GetTournamentChallengers().Count, this.Tournament.TotalChallengers)%>
        <ext:Button ID="JoinTourn" Text="Join" runat="server" OnClick="JoinTourn_Click" AutoPostBack="true" />
    </div>
    <div class="bs-callout bs-callout-info" visible="false" id="guestdescription" runat="server">
        <%=this.Text("TOURNAMENTS", "GUEST")%>
    </div>
    <div class="bs-callout bs-callout-info" visible="false" id="tournamentjoinedalready" runat="server">
        <%=this.Text("TOURNAMENTS", "ALREADY_JOINED")%>
    </div>
    <div class="viewbattlebtn">
   <button type="button" class="btn btn-primary btn-xs" style="display: none;">View Battle</button>
</div>
    <asp:Literal runat="server" ID="TournamentBracket"/>
</asp:Content>
