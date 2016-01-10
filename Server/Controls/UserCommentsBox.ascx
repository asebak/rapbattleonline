<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserCommentsBox.ascx.cs" Inherits="FreestyleOnline.Controls.UserComments" %>
<%@ Register TagPrefix="RAP" TagName="DisplaySitePost" Src="~/Controls/Generic/DisplaySitePost.ascx" %>
<%@ Register TagPrefix="RAP" TagName="PostEditor" Src="~/Controls/Generic/PostEditor.ascx" %>
<%@ Register TagPrefix="RAP" TagName="Pager" Src="~/Controls/Generic/Pager.ascx" %>

<asp:Repeater ID="CommentsFeed" runat="server" OnItemDataBound="CommentsFeed_ItemDataBound">
    <ItemTemplate>
        <RAP:DisplaySitePost  ID="PostEditor" UserId='<%#(int)Eval("CommenterID") %>' 
            PostInformation='<%#Server.HtmlDecode(Server.HtmlEncode(Eval("Comment").ToString())) %>'
             DatePosted='<%#Eval("DatePosted") %>' PostId='<%#Eval("UserCommentsID") %>' MessageId='<%#++this.PostNumber%>' runat="server"/>
    </ItemTemplate>
</asp:Repeater>
<RAP:Pager ID="ProfileCommentsPager" runat="server" />
<RAP:PostEditor runat="server" ID="PostEdit"/>