<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tournament.aspx.cs" Inherits="FreestyleOnline.Pages.Tournament" %>
<%@ Register TagPrefix="RAP" TagName="TournamentBox" Src="~/Controls/TournamentBox.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="TournamentsPH" runat="server"/>
    <asp:GridView runat="server" ID="TournamentsGV" AutoGenerateColumns="False" Width="100%" Height="100%" GridLines="None" ShowHeader="false">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <RAP:TournamentBox ID="TournamentItem" TournamentId='<%#DataBinder.Eval(Container.DataItem, "TournamentId") %>'
                                       Status='<%#DataBinder.Eval(Container.DataItem, "TournamentStatus") %>'
                                       TotalChallengers='<%#DataBinder.Eval(Container.DataItem, "TotalChallengers") %>'
                                       Type='<%#DataBinder.Eval(Container.DataItem, "TournamentType") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>