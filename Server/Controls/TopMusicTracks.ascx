<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMusicTracks.ascx.cs" Inherits="FreestyleOnline.Controls.TopMusicTracks" %>
<%@ Import Namespace="FreestyleOnline.classes.Core" %>
<%@ Register TagPrefix="RAP" TagName="MusicTrackBox" Src="~/Controls/MusicTrackBox.ascx" %>
<%@ Register TagPrefix="RAP" TagName="Pager" Src="~/Controls/Generic/Pager.ascx" %>
<asp:GridView GridLines="None" ID="TopTracksGrid" runat="server" AutoGenerateColumns="False" Width="100%" ShowHeader="false">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <RAP:MusicTrackBox MusicID='<%#DataBinder.Eval(Container.DataItem, "MusicId") %>' UserID='<%#DataBinder.Eval(Container.DataItem, "UserId") %>'
                                   Title='<%#DataBinder.Eval(Container.DataItem, "SongName") %>' 
                                   ImageURL='<%#DataBinder.Eval(Container.DataItem, "PictureLocation") %>'
                                   RatingEnabled='<%#this.GetCore<MusicData>().TrackRatingEnabled((int) DataBinder.Eval(Container.DataItem, "UserId"),
                                                         this.PageContext.PageUserID, Convert.ToInt32(DataBinder.Eval(Container.DataItem, "MusicId"))) %>'
                                   Rating='<%# DataBinder.Eval(Container.DataItem, "Rating") %>' TotalVotes='<%# DataBinder.Eval(Container.DataItem, "TotalVotes") %>'
                                   Date='<%# DataBinder.Eval(Container.DataItem, "DateAdded") %>'  CanDownload='<%# DataBinder.Eval(Container.DataItem, "CanDownload") %>'  runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<RAP:Pager ID="TopMusicTracksPager" runat="server" />
<asp:PlaceHolder ID="NoTopMusic" runat="server"/>
