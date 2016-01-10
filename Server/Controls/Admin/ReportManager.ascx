<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportManager.ascx.cs" Inherits="FreestyleOnline.Controls.Admin.ReportManager" %>
<%@ Import Namespace="FreestyleOnline.classes.Core" %>
<%@ Import Namespace="YAF.Types.Extensions" %>
<%@ Register TagPrefix="RAP" TagName="Pager" Src="~/Controls/Generic/Pager.ascx" %>
<asp:Repeater ID="ReportManagerRepeater" runat="server" OnItemDataBound="ReportManagerRepeater_ItemDataBound">
    <ItemTemplate>
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="row clearfix">
                    <ext:Label ID="ReportedTrackID" runat="server"
                        Text='<%#this.Text("ADMIN", "REPORTMANAGER_SONG").FormatWith(MusicData.GetMusicTrackDetailsFromId(Convert.ToInt32(Eval("MusicID"))).SongName) %>' />
                </div>
                <div class="row clearfix">
                    <ext:Label ID="ReportedByID" runat="server" Text='<%#this.Text("ADMIN", "REPORTMANAGER_BY").FormatWith(UserMembershipHelper.GetDisplayNameFromID((int) Eval("UserID"))) %>' />
                </div>
                <div class="row clearfix">
                    <ext:Button Icon="Delete" Text='<%#this.Text("COMMON", "COMMON_DELETE") %>' ID="DeleteButton" AutoPostBack="true"
                        CommandArgument='<%#string.Format("{0};{1};{2}", Eval("ReportID"), Eval("MusicID"), Eval("UserID")) %>'
                        runat="server" OnCommand="DeleteButton_Command" />
                    <ext:Button Icon="Decline" Text='<%#this.Text("COMMON", "COMMON_DECLINE") %>' ID="DeclineButton" AutoPostBack="true"
                        CommandArgument='<%#string.Format("{0};{1};{2}", Eval("ReportID"), Eval("MusicID"), Eval("UserID")) %>'
                        runat="server" OnCommand="DeclineButton_Command" />
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<RAP:Pager ID="ReportManagerPager" runat="server" />
<asp:PlaceHolder runat="server" ID="NoReports" />
