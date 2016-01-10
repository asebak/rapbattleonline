<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AudioBattle.aspx.cs" Inherits="FreestyleOnline.Pages.AudioBattle" %>
<%@ Register TagPrefix="RAP" TagName="CreateAudioBattle" Src="~/Controls/CreateAudioBattle.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <RAP:CreateAudioBattle runat="server" />
</asp:Content>