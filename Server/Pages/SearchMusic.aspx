<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchMusic.aspx.cs" Inherits="FreestyleOnline.Pages.SearchMusic" %>
<%@ Register TagPrefix="RAP" TagName="SearchMusic" Src="~/Controls/SearchMusic.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <RAP:SearchMusic runat="server" />
</asp:Content>