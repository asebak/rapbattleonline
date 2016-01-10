<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="FreestyleOnline.Pages.Profile" %>

<%@ Register TagPrefix="RAP" TagName="Profile" Src="~/Controls/Profile.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <RAP:Profile runat="server" />
</asp:Content>
