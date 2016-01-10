<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="FreestyleOnline.Pages.Chat" %>
<%@ Register TagPrefix="RAP" TagName="ChatRoom" Src="~/Controls/RealTime/ChatRoom.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <RAP:ChatRoom runat="server" />
</asp:Content>