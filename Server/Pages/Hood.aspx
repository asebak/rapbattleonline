<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Hood.aspx.cs" Inherits="FreestyleOnline.Pages.Hood" %>
<%@ Register TagPrefix="RAP" TagName="HoodAdd" Src="~/Controls/HoodAdd.ascx" %>
<%@ Register TagPrefix="RAP" TagName="HoodList" Src="~/Controls/HoodList.ascx" %>
<%@ Register TagPrefix="RAP" TagName="MyHood" Src="~/Controls/MyHood.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <RAP:HoodAdd runat="server" />
    <RAP:HoodList runat="server" />
</asp:Content>