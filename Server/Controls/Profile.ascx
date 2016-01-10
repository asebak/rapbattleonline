<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Profile.ascx.cs" Inherits="FreestyleOnline.Controls.Profile" %>
<%@ Register TagPrefix="RAP" TagName="MusicList" Src="~/Controls/MusicList.ascx" %>
<%@ Register TagPrefix="RAP" TagName="MusicAdd" Src="~/Controls/MusicAdd.ascx" %>
<%@ Register TagPrefix="RAP" TagName="WrittenBattlesList" Src="~/Controls/WrittenBattlesList.ascx" %>
<%@ Register TagPrefix="RAP" TagName="MyHood" Src="~/Controls/MyHood.ascx" %>
<%@ Register TagPrefix="YAF" TagName="AlbumList" Src="~/forum/controls/AlbumList.ascx" %>
<%@ Register TagPrefix="RAP" TagName="AudioBattlesList" Src="~/Controls/AudioBattlesList.ascx" %>
<%@ Register TagPrefix="RAP" TagName="UserCommentsBox" Src="~/Controls/UserCommentsBox.ascx" %>
<%@ Register TagPrefix="RAP" TagName="AddWrittenVerse" Src="~/Controls/Verses/AddWrittenVerse.ascx" %>
<%@ Register TagPrefix="RAP" TagName="AddAudioVerse" Src="~/Controls/Verses/AddAudioVerse.ascx" %>
<%@ Register TagPrefix="RAP" TagName="WrittenVerses" Src="~/Controls/Verses/WrittenVerses.ascx" %>
<%@ Register TagPrefix="RAP" TagName="AudioVerses" Src="~/Controls/Verses/AudioVerses.ascx" %>
<%@ Register TagPrefix="RAP" TagName="OverviewStats" Src="~/Controls/Stats/UserOverviewStats.ascx" %>
<%@ Register TagPrefix="RAP" TagName="WrittenBattleStats" Src="~/Controls/Stats/UserWrittenBattleStats.ascx" %>
<%@ Register TagPrefix="RAP" TagName="AudioBattleStats" Src="~/Controls/Stats/UserAudioBattleStats.ascx" %>
<%@ Register TagPrefix="RAP" TagName="UserLink" Src="~/Controls/Generic/UserLink.ascx" %>

<script type="text/javascript">
    var pageUserId = '<%=this.PageContext.PageUserID%>';
</script>
<div class="row">
    <div class="col-sm-3">
        <ext:Image Height="60px" Width="60px" runat="server" ID="ProfileImage" />
        <RAP:UserLink runat="server" ID="UserProfileLink" BlankTarget="true" />
                <br />
        <ext:HyperLink AutoDataBind="true" NavigateUrl='/forum/cp_editavatar' runat="server" Text='<%#this.Text("PROFILE", "EDIT_AVATAR")%>'
            Visible='<%#this.PageContext.PageUserID == this.UserId%>' />
        <br />
        <div class="sidebar-nav">
            <div class="navbar navbar-default" role="navigation">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".sidebar-navbar-collapse">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <span class="visible-xs navbar-brand"><%=this.Text("NAVIGATION", "SUB_PROFILE")%></span>
                </div>
                <div class="navbar-collapse collapse sidebar-navbar-collapse">
                    <ul class="nav navbar-nav" data-tabs="tabs">
                        <li>
                            <asp:LinkButton ID="lnkBuddy" runat="server" AutoPostBack="true" Icon="UserEarth" OnCommand="lnkBuddy_Command" />
                            <asp:Literal ID="ltrApproval" runat="server" Text='<%# this.GetText("BUDDY", "AWAIT_BUDDY_APPROVAL") %>' Visible="false" />
                        </li>
                        <li class="active"><a data-toggle="tab" href="#profile"><%=this.Text("NAVIGATION", "SUB_PROFILE")%></a></li>
                        <li><a data-toggle="tab" href="#profilemusic"><%=this.Text("NAVIGATION", "MENU_MUSIC")%><span class="badge"><%=this.ProfileMusicList.MusicCount %></span></a></li>
                        <li><a data-toggle="tab" href="#profileHoods"><%=this.Text("NAVIGATION", "MENU_HOOD2")%><span class="badge"><%=this.ProfileHood.HoodCount %></span></a></li>
                        <li><a data-toggle="tab" href="#profileAudioBattles"><%=this.Text("NAVIGATION", "MENU_MYAUDIOBATTLES2")%><span class="badge"><%=this.AudioBattlesList.AudioBattlesCount %></span></a></li>
                        <li><a data-toggle="tab" href="#profileWrittenBattles"><%=this.Text("NAVIGATION", "MENU_MYWRITTENBATTLES2")%><span class="badge"><%=this.WrittenBattlesList.WrittenBattlesCount %></span></a></li>
                        <li><a data-toggle="tab" href="#profilePictures"><%=this.Text("NAVIGATION", "SUB_ALBUMS")%><span class="badge"></span></a></li>
                        <li><a data-toggle="tab" href="#profileVerses"><%=this.Text("NAVIGATION", "SUB_VERSES")%><span class="badge"><%= this.UsersWrittenVerses.WrittenVersesCount + this.UsersAudioVerses.AudioVersesCount %></span></a></li>
                        <li><a data-toggle="tab" href="#profileStatistics"><%=this.Text("NAVIGATION", "SUB_STATS")%></a></li>
                    </ul>
                </div>
                <!--/.nav-collapse -->
            </div>
        </div>
    </div>
    <div class="tab-content">
        <div class="tab-pane active" id="profile">
            <div class="col-sm-9">
                <div class="row">
                    <div class="col-sm-7">
                        <ext:Image AutoDataBind="true" ID="HeaderImage" runat="server" />
                    </div>
                    <div class="col-sm-2">
                        <YAF:ThemeButton ID="PM" runat="server" CssClass="profileButton" Visible="false" TextLocalizedPage="POSTS"
                            TextLocalizedTag="PM" ImageThemeTag="PM" TitleLocalizedTag="PM_TITLE" TitleLocalizedPage="POSTS" />
                        <YAF:ThemeButton ID="Email" runat="server" CssClass="profileButton" Visible="false" TextLocalizedPage="POSTS"
                            TextLocalizedTag="EMAIL" ImageThemeTag="EMAIL" TitleLocalizedTag="EMAIL_TITLE" TitleLocalizedPage="POSTS" />
                        <YAF:ThemeButton ID="Home" runat="server" CssClass="profileButton" Visible="false" TextLocalizedPage="POSTS"
                            TextLocalizedTag="HOME" ImageThemeTag="HOME" TitleLocalizedTag="HOME_TITLE" TitleLocalizedPage="POSTS" />
                        <YAF:ThemeButton ID="Blog" runat="server" CssClass="profileButton" Visible="false" TextLocalizedPage="POSTS"
                            TextLocalizedTag="BLOG" ImageThemeTag="BLOG" TitleLocalizedTag="BLOG_TITLE" TitleLocalizedPage="POSTS" />
                        <YAF:ThemeButton ID="MSN" runat="server" CssClass="profileButton" Visible="false" TextLocalizedPage="POSTS"
                            TextLocalizedTag="MSN" ImageThemeTag="MSN" TitleLocalizedTag="MSN_TITLE" TitleLocalizedPage="POSTS" />
                        <YAF:ThemeButton ID="AIM" runat="server" CssClass="profileButton" Visible="false" TextLocalizedPage="POSTS"
                            TextLocalizedTag="AIM" ImageThemeTag="AIM" TitleLocalizedTag="AIM_TITLE" TitleLocalizedPage="POSTS" />
                        <YAF:ThemeButton ID="YIM" runat="server" CssClass="profileButton" Visible="false" TextLocalizedPage="POSTS"
                            TextLocalizedTag="YIM" ImageThemeTag="YIM" TitleLocalizedTag="YIM_TITLE" TitleLocalizedPage="POSTS" />
                        <YAF:ThemeButton ID="ICQ" runat="server" CssClass="profileButton" Visible="false" TextLocalizedPage="POSTS"
                            TextLocalizedTag="ICQ" ImageThemeTag="ICQ" TitleLocalizedTag="ICQ_TITLE" TitleLocalizedPage="POSTS" />
                        <YAF:ThemeButton ID="XMPP" runat="server" CssClass="profileButton" Visible="false" TextLocalizedPage="POSTS"
                            TextLocalizedTag="XMPP" ImageThemeTag="XMPP" TitleLocalizedTag="XMPP_TITLE" TitleLocalizedPage="POSTS" />
                        <YAF:ThemeButton ID="Skype" runat="server" CssClass="profileButton" Visible="false" TextLocalizedPage="POSTS"
                            TextLocalizedTag="SKYPE" ImageThemeTag="SKYPE" TitleLocalizedTag="SKYPE_TITLE" TitleLocalizedPage="POSTS" />
                        <YAF:ThemeButton ID="Facebook" runat="server" CssClass="profileButton" Visible="false" TextLocalizedPage="POSTS"
                            TextLocalizedTag="FACEBOOK" ImageThemeTag="Facebook2" TitleLocalizedTag="FACEBOOK_TITLE" TitleLocalizedPage="POSTS" />
                        <YAF:ThemeButton ID="Twitter" runat="server" CssClass="profileButton" Visible="false" TextLocalizedPage="POSTS"
                            TextLocalizedTag="TWITTER" ImageThemeTag="Twitter2" TitleLocalizedTag="TWITTER_TITLE" TitleLocalizedPage="POSTS" />
                        <YAF:ThemeButton ID="Google" runat="server" CssClass="profileButton" Visible="false" TextLocalizedPage="POSTS"
                            TextLocalizedTag="GOOGLE" ImageThemeTag="Google2" TitleLocalizedTag="GOOGLE_TITLE" TitleLocalizedPage="POSTS" />
                    </div>
                </div>
                <br />
                <ext:FileUploadField ID="HeaderUploader" Width="500" AutoDataBind="true" FieldLabel='<%#this.Text("COMMON", "COMMON_UPLOADPICTURE") %>'
                    Visible='<%#this.PageUserId == this.UserId %>' Cls="form-control" runat="server" />
                <br />
                <ext:Button AutoDataBind="true" AutoPostBack="true" ID="UploadBtn" Text='<%#this.Text("PROFILE", "SAVE") %>' OnClick="HeaderUpload_OnClick" Cls="btn btn-default"
                    Visible='<%#this.PageUserId == this.UserId %>' runat="server" />
                <br />
                <a href="#" class="formattedLabel" id="usersbiography" data-type="textarea"><%=this.Bio%></a>
                <RAP:UserCommentsBox ID="ProfileUserComments" runat="server" />
            </div>
        </div>
        <div class="tab-pane" id="profilemusic">
            <div class="col-sm-9">
                <RAP:MusicAdd ID="ProfileMusicAdd" runat="server" />
                <RAP:MusicList ID="ProfileMusicList" runat="server" />
            </div>
        </div>
        <div class="tab-pane" id="profileHoods">
            <div class="col-sm-9">
                <RAP:MyHood ID="ProfileHood" runat="server" />
            </div>

        </div>
        <div class="tab-pane" id="profileAudioBattles">
            <div class="col-sm-9">
                <RAP:AudioBattlesList ID="AudioBattlesList" runat="server" />
            </div>
        </div>
        <div class="tab-pane" id="profileWrittenBattles">
            <div class="col-sm-9">
                <RAP:WrittenBattlesList ID="WrittenBattlesList" runat="server" />
            </div>
        </div>
        <div class="tab-pane" id="profilePictures">
            <div class="col-sm-9">
                <YAF:AlbumList ID="ProfileAlbumList" runat="server" />
            </div>
        </div>
        <div class="tab-pane" id="profileVerses">
            <div class="col-sm-9">
                <RAP:AddWrittenVerse ID="AddVerseWritten" runat="server" />
                <RAP:WrittenVerses ID="UsersWrittenVerses" runat="server" />
                <RAP:AddAudioVerse ID="AddVerseAudio" runat="server" />
                <RAP:AudioVerses ID="UsersAudioVerses" runat="server" />
            </div>
        </div>
        <div class="tab-pane" id="profileStatistics">
            <div class="col-sm-9">
                <RAP:OverviewStats ID="UserOverviewStats" runat="server" />
                <RAP:WrittenBattleStats ID="UserWrittenStats" runat="server" />
                <RAP:AudioBattleStats ID="UserAudioStats" runat="server" />
            </div>

        </div>
    </div>

</div>
