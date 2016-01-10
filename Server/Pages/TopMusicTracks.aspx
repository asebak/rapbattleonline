<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TopMusicTracks.aspx.cs" Inherits="FreestyleOnline.Pages.TopMusicTracks" %>
<%@ Register TagPrefix="RAP" TagName="TopMusicTracks" Src="~/Controls/TopMusicTracks.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <RAP:TopMusicTracks ID="TopMusic" runat="server" />
</asp:Content>