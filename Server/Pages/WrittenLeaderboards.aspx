<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WrittenLeaderboards.aspx.cs" Inherits="FreestyleOnline.Pages.WrittenLeaderboards" %>
<%@ Register TagPrefix="RAP" TagName="WrittenLeaderboards" Src="~/Controls/Leaderboards/BattleLeaderboard.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <RAP:WrittenLeaderboards ID="WrittenLeader" runat="server" />
</asp:Content>