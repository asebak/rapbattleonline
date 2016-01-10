<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MusicTrackBox.ascx.cs" Inherits="FreestyleOnline.Controls.MusicTrackBox" %>
<%@ Import Namespace="FreestyleOnline.classes.Providers" %>
<%@ Import Namespace="FreestyleOnline.classes.Types" %>
<%@ Import Namespace="YAF.Types.Extensions" %>
<script type="text/javascript">
    $(function () {
        createMusicRating("<%= this.MusicId %>", "<%= this.PageContext.PageUserID %>", "<%= this.Rating %>", "<%= this.RatingEnabled %>" == "True");
    });
</script>
<div class="panel panel-default">
    <div class="panel-body">
        <div class="row clearfix">
            <div class="col-md-6 column">
                <asp:Image Height="75px" Width="75px" ImageUrl='<%#this.GetService<ResourceProvider>().GetClientPath(RapResource.MusicTracksPictures, this.ImageUrl) %>' CssClass="img-rounded" runat="server" />
            </div>
            <div class="col-md-6 column">
                <audio controls>
                    <source src='/api/music/filestream/<%=this.MusicId %>' type="audio/mpeg" />
                </audio>
            </div>
        </div>
        <div class="row clearfix">
            <div class="col-md-6 column">
                <ext:HyperLink ID="MusicProfile" AutoDataBind="true" NavigateUrl='<%#"/pages/profile/{0}".FormatWith(this.UserId)%>'
                    Text='<%#this.Text("MUSIC", "MUSIC_NAME").FormatWith(UserMembershipHelper.GetDisplayNameFromID(this.UserId)) %>' runat="server" />
            </div>
            <div class="col-md-6 column pull-right">
                <div id="RatingDIVGeneric<%=this.MusicId %>"></div>
                <ext:Label ID="TotalVote" AutoDataBind="true" runat="server" Text='<%#this.Text("MUSIC", "MUSIC_TOTALVOTES").FormatWith(this.TotalVotes) %>' />
            </div>
        </div>
        <div class="row clearfix">
            <div class="col-md-6 column">
                <ext:Label ID="MusicTitle" AutoDataBind="true" Text='<%#this.Text("COMMON", "COMMON_TITLE3").FormatWith(this.Title) %>' runat="server" />
            </div>
            <div class="col-md-6 column pull-right">
                <div class="row clearfix">
                <ext:Button AutoDataBind="true" runat="server" ID="DeleteButton" Visible='<%#this.UserId == this.PageContext.PageUserID %>'
                    Icon="Delete" OnCommand="DeleteButton_Command" AutoPostBack="true" CommandArgument='<%#this.MusicId %>' Text='<%#this.Text("COMMON", "COMMON_DELETE") %>' />
                <ext:Button AutoDataBind="true" runat="server" ID="DownloadButton" Visible='<%#this.CanDownload %>' Icon="DiskDownload"
                    OnClientClick='<%# "onDownloadClick(" + this.MusicId + ");" %>' Text='<%#this.Text("COMMON", "COMMON_DOWNLOAD") %>' />
                <ext:Button AutoDataBind="true" runat="server" ID="ReportButton" Visible='<%#!this.PageContext.IsGuest && this.PageContext.PageUserID != this.UserId %>'
                    Icon="Report" OnCommand="ReportButton_Command" OnClientClick="return handlepostback(#{ReportButton});"
                    AutoPostBack="true" CommandArgument='<%#this.MusicId %>' Text='<%#this.Text("COMMON", "COMMON_REPORT") %>' />
                    </div>
            </div>
        </div>
        <div class="row clearfix">
            <div class="col-md-6 column">
                <label><%= this.Text("COMMON", "COMMON_DATEADDED2") %></label>
                <YAF:DisplayDateTime runat="server" DateTime='<%#this.Date %>' />
            </div>
        </div>
    </div>
</div>
