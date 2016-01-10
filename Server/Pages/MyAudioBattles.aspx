<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyAudioBattles.aspx.cs" Inherits="FreestyleOnline.Pages.MyAudioBattles" %>
<%@ Register TagPrefix="RAP" TagName="AudioBattlesList" Src="~/Controls/AudioBattlesList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <RAP:AudioBattlesList ID="AudioBattlesList" runat="server" /> 
</asp:Content>