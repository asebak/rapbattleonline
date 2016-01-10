<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WrittenBattle.aspx.cs" Inherits="FreestyleOnline.Pages.WrittenBattle" %>
<%@ Register TagPrefix="RAP" TagName="CreateWrittenBattle" Src="~/Controls/CreateWrittenBattle.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <RAP:CreateWrittenBattle runat="server" />
</asp:Content>