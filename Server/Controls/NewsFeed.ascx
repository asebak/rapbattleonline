<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsFeed.ascx.cs" Inherits="FreestyleOnline.Controls.NewsFeed" %>
<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<%@ Register TagPrefix="RAP" TagName="CommentsBox" Src="~/Controls/CommentsBox.ascx" %>
<%@ Register TagPrefix="RAP" TagName="Pager" Src="~/Controls/Generic/Pager.ascx" %>
<asp:Repeater ID="NewsFeedID" runat="server">
    <ItemTemplate>
        <RAP:CommentsBox ID="NewsBox" UserID='<%#Convert.ToInt32(Eval("UserID")) %>' Message='<%#Server.HtmlDecode(Server.HtmlEncode(Eval("Information").ToString())) %>' 
                         Title='<%#Eval("Title") %>' TitleHyperLink='<%# FriendlyUrl.Href("~/Pages/NewsComments", Eval("NewsFeedID")) %>' 
                         Date='<%#Eval("DatePosted") %>' CommentsCount='<%#Convert.ToInt32(Eval("NumberOfComments"))%>'   runat="server" />
    </ItemTemplate>
</asp:Repeater>
<RAP:Pager ID="NewsFeedPager" runat="server" />