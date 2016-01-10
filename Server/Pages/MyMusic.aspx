<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyMusic.aspx.cs" Inherits="FreestyleOnline.Pages.MyMusic" %>
<%@ Register TagPrefix="RAP" TagName="MusicAdd" Src="~/Controls/MusicAdd.ascx" %>
<%@ Register TagPrefix="RAP" TagName="MusicList" Src="~/Controls/MusicList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <RAP:MusicAdd ID="MusicAddMainSite" runat="server" />
    <RAP:MusicList ID="MusicListMainSite" runat="server" />
</asp:Content>