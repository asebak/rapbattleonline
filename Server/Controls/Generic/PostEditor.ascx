<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostEditor.ascx.cs" Inherits="FreestyleOnline.Controls.Generic.PostEditor" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<table class="postcomment">
    <tr>
        <td valign="top" width="20%">
            <YAF:LocalizedLabel runat="server" LocalizedTag="message" />
            <br />
            <YAF:LocalizedLabel ID="LocalizedLblMaxNumberOfPost" runat="server" LocalizedTag="MAXNUMBEROF" />
        </td>
        <td id="EditorLine" runat="server" width="80%">
            <CKEditor:CKEditorControl ID="CkEditor" BasePath="/forum/editors/ckeditor/" Width="100%" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="tablePostFooter">
            <br />
        </td>
        <td class="tablePostFooter">
            <ext:Button AutoDataBind="true" ID="PostButton" Cls="btn btn-default" AutoPostBack="true" runat="server" Text='<%#this.Text("COMMON", "COMMON_SUBMIT") %>' />
        </td>
    </tr>

</table>
