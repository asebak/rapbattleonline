<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewsComments.aspx.cs" Inherits="FreestyleOnline.Pages.NewsComments" %>
<%@ Register TagPrefix="RAP" TagName="NewsComments" Src="~/Controls/NewsComments.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <RAP:NewsComments ID="NewsCommentsBox" runat="server" />
</asp:Content>