<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsManager.ascx.cs" Inherits="FreestyleOnline.forum.controls.EditNewsFeed" %>
<%@ Import Namespace="YAF.Core" %>
<%@ Import Namespace="YAF.Core.Services" %>
<%@ Import Namespace="YAF.Types.Constants" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<%@ Import Namespace="YAF.Utils" %>
<%@ Import Namespace="YAF.Types.Extensions" %>
<link href="../../Styles/TableStyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js"></script>
<script type="text/javascript" src="../../js/NewsManager.js"></script>
<table class="content" cellspacing="1" cellpadding="0" width="100%">
    <tr>
        <td class="header1">
            <asp:Label Text="News Manager" ID="NewsManagerID" runat="server"></asp:Label>
        </td>
    </tr>
    <asp:Repeater ID="EditNewsFeedID" runat="server" DataSourceID="NewsDBForEdit">
        <ItemTemplate>
            <table class="bordered">
                <thead>
                    <tr>
                        <th id="EditHeaderNews">
                            <asp:Label ID="EditTitleNews" runat="server" Text='<%#Eval("Title")%>'></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="EditDateNews" runat="server" Text='<%#Eval("DatePosted")%>'></asp:Label>
                            <br />
                            <asp:Label ID="EditUserNews" runat="server" Text='<%# String.Format ("Posted By: {0}", UserMembershipHelper.GetDisplayNameFromID((int)Eval("UserID"))) %>'></asp:Label>
                            <br />
                        </th>
                    </tr>
                </thead>
                <tr>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td id="EditNewsID">
                        <asp:Label ID="EditInformationNews" runat="server" Text='<%#Eval("Information")%>'></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <script type="text/javascript">
                CreateDeleteButton("<%# Eval("NewsFeedID") %>");
            </script>
            <div id="DeleteNewsButton<%# Eval("NewsFeedID") %>"></div>
            <br />
        </ItemTemplate>
        <FooterTemplate>
            <table class="content" cellspacing="1" cellpadding="0" width="100%">
                <tr>
                    <td class="header1">
                        <asp:Label Text="Post News" ID="Label2" runat="server"></asp:Label>
                </tr>
                </td>
        </thead>
            </table>
            <br />
            <br />
            <asp:Label Text="News Title" ID="NewsTitleID" runat="server"></asp:Label>
            <br />
            <br />
            <asp:TextBox ID="titletextboxid" runat="server" CssClass="bordered" Width="286px"></asp:TextBox>
            <br />
            <br />
            <asp:Label Text="News Content" ID="Label1" runat="server"></asp:Label>
            <br />
            <br />
            <asp:TextBox ID="newscontentid" runat="server" CssClass="bordered" TextMode="MultiLine" Height="145px" Width="563px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="PostNewsButton" runat="server" OnClick="PostNewsButton_Click" Text="Post News" />
        </FooterTemplate>
    </asp:Repeater>

</table>


<asp:SqlDataSource ID="NewsDBForEdit" runat="server" ConnectionString="<%$ ConnectionStrings:yafnet %>" SelectCommand="SELECT [NewsFeedID], [UserID], [Title], [DatePosted], [Information] FROM [rap_NewsFeed] ORDER BY [NewsFeedID] DESC"></asp:SqlDataSource>
