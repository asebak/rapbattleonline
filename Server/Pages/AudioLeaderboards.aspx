<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AudioLeaderboards.aspx.cs" Inherits="FreestyleOnline.Pages.AudioLeaderboards" %>
<%@ Register TagPrefix="RAP" TagName="AudioLeaderboards" Src="~/Controls/Leaderboards/BattleLeaderboard.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <RAP:AudioLeaderboards ID="AudioLeader" runat="server" />
</asp:Content>