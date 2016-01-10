<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchBattle.aspx.cs" Inherits="FreestyleOnline.Pages.SearchBattle" %>
<%@ Register TagPrefix="RAP" TagName="SearchBattles" Src="~/Controls/SearchBattles.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <RAP:SearchBattles runat="server" />
</asp:Content>