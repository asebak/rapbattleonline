<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyWrittenBattles.aspx.cs" Inherits="FreestyleOnline.Pages.MyWrittenBattles" %>
<%@ Register TagPrefix="RAP" TagName="WrittenBattlesList" Src="~/Controls/WrittenBattlesList.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <RAP:WrittenBattlesList ID="WrittenBattles" runat="server" />
</asp:Content>