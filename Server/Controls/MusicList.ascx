<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MusicList.ascx.cs" Inherits="FreestyleOnline.Controls.MusicList" %>
<%@ Register TagPrefix="RAP" TagName="MusicTrack" Src="~/Controls/MusicTrackBox.ascx" %>
<%@ Register TagPrefix="RAP" TagName="Pager" Src="~/Controls/Generic/Pager.ascx" %>
<asp:Repeater ID="MusicTracks" runat="server">
    <ItemTemplate>
        <Rap:MusicTrack MusicId='<%#Eval("MusicID")%>' Date='<%#Eval("DateAdded") %>' UserId='<%#this.UserId%>' Title='<%#Eval("Title") %>'
             ImageUrl='<%#Eval("Picture")%>' CanDownload='<%#Eval("CanDownload")%>'
            runat="server"/>
    </ItemTemplate>
</asp:Repeater>
<RAP:Pager ID="MusicListPager" runat="server" />