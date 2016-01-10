<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AudioBattleComments.ascx.cs" Inherits="FreestyleOnline.Controls.AudioBattleComments" %>
<<%@ Register TagPrefix="RAP" TagName="DisplaySitePost" Src="~/Controls/Generic/DisplaySitePost.ascx" %>
<%@ Register TagPrefix="RAP" TagName="PostEditor" Src="~/Controls/Generic/PostEditor.ascx" %>
<%@ Register TagPrefix="RAP" TagName="Pager" Src="~/Controls/Generic/Pager.ascx" %>

<asp:Repeater ID="AudioBattleFeed" runat="server" OnItemDataBound="AudioBattleFeed_ItemDataBound">
    <ItemTemplate>
        <RAP:DisplaySitePost  ID="PostEditor" UserId='<%#(int)Eval("UserID") %>' 
            PostInformation='<%#Server.HtmlDecode(Server.HtmlEncode(Eval("Comment").ToString())) %>'
             DatePosted='<%#Eval("DatePosted") %>' PostId='<%#Eval("AudioBattleCommentsID") %>' MessageId='<%#++this.PostNumber%>' runat="server"/>
    </ItemTemplate>
</asp:Repeater>
<RAP:Pager ID="AudioBattleCommentsPager" runat="server" />
<RAP:PostEditor runat="server" ID="PostEdit"/>
