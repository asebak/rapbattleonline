<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactManager.ascx.cs" Inherits="FreestyleOnline.Controls.Admin.ContactManager" %>
<%@ Import Namespace="YAF.Types.Extensions" %>
<asp:Repeater ID="ContactManagerRepeater" OnItemDataBound="ContactManagerID_ItemDataBound" runat="server">
    <ItemTemplate>
        <ext:Label runat="server" Text='<%#this.Text("ADMIN", "CONTACTMANAGER_SENTBY").FormatWith(UserMembershipHelper.GetDisplayNameFromID((int) Eval("UserID"))) %>' />
        <br />
        <ext:Label runat="server" Text='<%#this.Text("COMMON", "COMMON_TITLE3").FormatWith(Eval("Title")) %>'></ext:Label>
        <br />
        <ext:Label runat="server" Text='<%#Eval("Content") %>'></ext:Label>
        <br />
        <br />
        <textarea class="form-control" runat="server" rows="3" id="ContactMsg" />
        <table>
            <tr>
                <td style="width: 1%;">
                    <ext:Button Icon="CommentAdd" Text='<%#this.Text("ADMIN", "CONTACTMANAGER_RESPOND") %>' ID="RespondButton" AutoPostBack="true"
                        runat="server" OnCommand="RespondButton_Command" AutoDataBind="true" />
                </td>
                <td style="float: left;">
                    <ext:Button Icon="CommentDelete" Text='<%#this.Text("ADMIN", "CONTACTMANAGER_AVOID") %>' ID="IgnoreButton" AutoPostBack="true"
                        CommandArgument='<%#Eval("ContactID") %>' runat="server" OnCommand="IgnoreButton_Command" AutoDataBind="true" />
                </td>
            </tr>
        </table>
    </ItemTemplate>
</asp:Repeater>
<asp:PlaceHolder ID="NoContactMsgs" runat="server" />
