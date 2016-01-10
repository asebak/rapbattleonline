<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchMusic.ascx.cs" Inherits="FreestyleOnline.Controls.SearchMusic" %>
<%@ Import Namespace="FreestyleOnline.classes.Core" %>
<%@ Register TagPrefix="RAP" TagName="MusicTrackBox" Src="~/Controls/MusicTrackBox.ascx" %>
<div class="panel panel-primary">
    <div class="panel-heading">
    </div>
    <div class="panel-body">
        <ext:RadioGroup AutoDataBind="true" Layout="ColumnLayout" ColumnsWidths="1" ID="SelectionList" runat="server" Vertical="false">
            <Items>
                <ext:Radio ID="Radio1" Checked="true" BoxLabel='<%#this.Text("COMMON", "COMMON_USER") %>' runat="server" InputValue="0" />
                <ext:Radio ID="Radio2" runat="server" BoxLabel='<%#this.Text("COMMON", "COMMON_Title") %>' InputValue="1" />
            </Items>
        </ext:RadioGroup>
        <label for="SearchParametersText"><%=this.Text("COMMON", "COMMON_VALUESELECT") %></label>
        <input class="form-control" type="text" id="SearchParametersText" runat="server" />
        <ext:Button Cls="btn btn-default" AutoDataBind="true" Text='<%#this.Text("COMMON", "COMMON_SUBMIT") %>' ID="OnSearchButton"
            runat="server" AutoPostBack="true" OnClick="OnSearchButton_Click" />
    </div>
</div>
<%-- TODO: Fix autopostback values not updating --%>
<%--<asp:UpdatePanel runat="server" ID="SearchResults" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="OnSearchButton" EventName="Click" />
    </Triggers>
    <ContentTemplate>--%>
<asp:GridView GridLines="None" ID="SearchMusicGrid" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="MusicID" ShowHeader="false">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <RAP:MusicTrackBox MusicId='<%#Eval("MusicID") %>' UserId='<%#Eval("UserID") %>' Title='<%#Eval("Title") %>'
                    ImageUrl='<%#Eval("Picture")%>' Date='<%#Eval("DateAdded")%>'
                    RatingEnabled='<%#this.GetCore<MusicData>().TrackRatingEnabled(Convert.ToInt32(Eval("UserID")), this.PageContext.PageUserID, Convert.ToInt32(Eval("MusicID"))) %>'
                    Rating='<%#this.GetCore<MusicData>().GetRatingForMusicTrack(Convert.ToInt32(Eval("MusicID"))) %>'
                    TotalVotes='<%#this.GetCore<MusicData>().GetTotalVotesForMusicTrack(Convert.ToInt32(Eval("MusicID"))) %>' CanDownload='<%#Eval("CanDownload")%>' runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<%--            </ContentTemplate>
    </asp:UpdatePanel>--%>
