<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsComments.ascx.cs" Inherits="FreestyleOnline.Controls.NewsComments" %>
<%@ Register TagPrefix="RAP" TagName="Pager" Src="~/Controls/Generic/Pager.ascx" %>
<%@ Register TagPrefix="RAP" TagName="PostEditor" Src="~/Controls/Generic/PostEditor.ascx" %>
<%@ Register TagPrefix="RAP" TagName="DisplaySitePost" Src="~/Controls/Generic/DisplaySitePost.ascx" %>

<asp:Repeater ID="NewsFeedID" runat="server" OnItemDataBound="NewsFeedID_ItemDataBound">
    <ItemTemplate>
        <RAP:DisplaySitePost  ID="PostEditor" UserId='<%#(int)Eval("UserID") %>' 
            PostInformation='<%#Server.HtmlDecode(Server.HtmlEncode(Eval("Comment").ToString())) %>'
             DatePosted='<%#Eval("DatePosted") %>' PostId='<%#Eval("NewsFeedCommentsID") %>' MessageId='<%#++this.PostNumber%>' runat="server"/>
    </ItemTemplate>
</asp:Repeater>
<RAP:Pager ID="NewsCommentsPager" runat="server" />
<RAP:PostEditor runat="server" ID="PostEdit"/>
