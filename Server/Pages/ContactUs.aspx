<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="FreestyleOnline.Pages.ContactUs" %>
<%@ Register TagPrefix="RAP" TagName="ContactForm" Src="~/Controls/ContactForm.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <RAP:ContactForm ID="ContactFormID" runat="server" />
</asp:Content>