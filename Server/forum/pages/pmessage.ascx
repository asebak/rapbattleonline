<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.pmessage" Codebehind="pmessage.ascx.cs" %>
<%@ Register TagPrefix="uc1" TagName="smileys" Src="../controls/smileys.ascx" %>
<YAF:PageLinks runat="server" ID="PageLinks" />
<table class="content" width="100%" cellspacing="1" cellpadding="0">
	<tr>
		<td class="header1" colspan="2">
			<YAF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="title" />
		</td>
	</tr>
	<tr id="PreviewRow" runat="server" visible="false">
		<td class="postformheader" valign="top">
			<YAF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="previewtitle" />
		</td>
		<td class="post" valign="top" id="PreviewCell" runat="server">
			<YAF:MessagePost ID="PreviewMessagePost" runat="server" />
		</td>
	</tr>
	<tr id="ToRow" runat="server">
		<td style="width:210px" class="postformheader">
			<YAF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="to" />
		</td>
		<td class="post">
			<asp:TextBox ID="To" runat="server" />
			<asp:DropDownList runat="server" ID="ToList" Visible="false" />
			<asp:Button runat="server" ID="FindUsers" CssClass="pbutton" OnClick="FindUsers_Click" />
			<asp:Button runat="server" ID="AllUsers" CssClass="pbutton" OnClick="AllUsers_Click" />
            <asp:Button runat="server" ID="AllBuddies" CssClass="pbutton" OnClick="AllBuddies_Click" />
			<asp:Button runat="server" ID="Clear" CssClass="pbutton" OnClick="Clear_Click" Visible="false" />
			<asp:Label ID="MultiReceiverInfo" runat="server" Visible="false" />
		</td>
	</tr>
	<tr>
		<td class="postformheader">
			<YAF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="subject" />
		</td>
		<td class="post">
			<asp:TextBox ID="PmSubjectTextBox" style="width:99%" runat="server" />
		</td>
	</tr>
	<tr>
		<td class="postformheader" valign="top"  width="20%">
			<YAF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="message" />
			<uc1:smileys runat="server" OnClick="insertsmiley" ID="SmileysBox" />
		</td>
		<td id="EditorLine" class="post" runat="server">
			<!-- editor goes here -->
		</td>
	</tr>
	<tr>
		<td class="footer1">&nbsp;
		</td>
		<td class="footer1">
			<YAF:ThemeButton ID="Preview" runat="server" CssClass="yafcssbigbutton leftItem"
				TextLocalizedTag="PREVIEW" OnClick="Preview_Click" />
			<YAF:ThemeButton ID="Save" runat="server" CssClass="yafcssbigbutton leftItem" TextLocalizedTag="SAVE"
				OnClick="Save_Click" />
			<YAF:ThemeButton ID="Cancel" runat="server" CssClass="yafcssbigbutton leftItem" TextLocalizedTag="CANCEL"
				OnClick="Cancel_Click" />
		</td>
	</tr>
</table>
<div id="DivSmartScroller">
	<YAF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
