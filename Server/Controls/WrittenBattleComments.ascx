<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WrittenBattleComments.ascx.cs" Inherits="FreestyleOnline.Controls.WrittenBattleComments" %>
<%@ Register TagPrefix="RAP" TagName="DisplaySitePost" Src="~/Controls/Generic/DisplaySitePost.ascx" %>
<%@ Register TagPrefix="RAP" TagName="PostEditor" Src="~/Controls/Generic/PostEditor.ascx" %>
<%@ Register TagPrefix="RAP" TagName="Pager" Src="~/Controls/Generic/Pager.ascx" %>

<asp:Repeater ID="WrittenBattleFeed" runat="server" OnItemDataBound="WrittenBattleFeed_ItemDataBound">
    <ItemTemplate>
        <RAP:DisplaySitePost  ID="PostEditor" UserId='<%#(int)Eval("UserID") %>' 
            PostInformation='<%#Server.HtmlDecode(Server.HtmlEncode(Eval("Comment").ToString())) %>'
             DatePosted='<%#Eval("DatePosted") %>' PostId='<%#Eval("WrittenBattleCommentsID") %>' MessageId='<%#++this.PostNumber%>' runat="server"/>
    </ItemTemplate>
</asp:Repeater>
<RAP:Pager ID="WrittenBattleCommentsPager" runat="server" />
<RAP:PostEditor runat="server" ID="PostEdit"/>
