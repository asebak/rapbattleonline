<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationMenu.ascx.cs" Inherits="FreestyleOnline.Controls.NavigationMenu" %>


<nav class="navbar navbar-default" role="navigation">
<div class="navbar-header">
	<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
	 <span class="sr-only">Toggle navigation</span><span class="icon-bar">
	 </span><span class="icon-bar"></span><span class="icon-bar"></span>
	</button>
</div>  				
    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
        <ul class="nav navbar-nav">
            <li>
                <a href='/'><%=this.Text("NAVIGATION", "MENU_HOME")%></a>
            </li>
            <li class="dropdown">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown"><%=this.Text("NAVIGATION", "MENU_COMMUNITY")%></a>
                <ul class="dropdown-menu">
                    <li>
                        <a href="/forum"><%=this.Text("NAVIGATION", "MENU_FORUM")%></a>
                    </li>
                    <li>
                        <a href="/Community/Hoods"><%=this.Text("NAVIGATION", "MENU_HOOD2")%></a>
                    </li>
                    <li class="divider">
                    </li>
                    <li>
                        <a href="/Pages/Chat"><%=this.Text("NAVIGATION", "MENU_CHAT")%></a>
                    </li>
                    <li class="disabled">
                        <a><%=this.Text("NAVIGATION", "MENU_CYPHERS")%></a>
                    </li>
                </ul>
            </li>
            <li class="dropdown">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown"><%=this.Text("NAVIGATION", "MENU_MUSIC")%></a>
                <ul class="dropdown-menu">
                    <li>
                        <a href="/Pages/SearchMusic"><%=this.Text("NAVIGATION", "MENU_SEARCHMUSIC")%></a>
                    </li>
                </ul>
            </li>
            <li class="dropdown">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown"><%=this.Text("NAVIGATION", "MENU_BATTLE")%></a>
                <ul class="dropdown-menu">
                    <li>
                        <a href="/Pages/WrittenBattle"><%=this.Text("NAVIGATION", "MENU_STARTWRITTEN")%></a>
                    </li>
                    <li>
                        <a href="/Pages/MyWrittenBattles"><%=this.Text("NAVIGATION", "MENU_MYWRITTENBATTLES")%></a>
                    </li>
                    <li class="divider">
                    </li>
                    <li>
                        <a href="/Pages/AudioBattle"><%=this.Text("NAVIGATION", "MENU_STARTAUDIO")%></a>
                    </li>
                    <li>
                        <a href="/Pages/MyAudioBattles"><%=this.Text("NAVIGATION", "MENU_MYAUDIOBATTLES")%></a>
                    </li>
                </ul>
            </li>
            <li >
                <a href='/Pages/Tournament'>Tournaments</a>
            </li>
        <li class="dropdown">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown"><%=this.Text("NAVIGATION", "MENU_LEADERBOARDS")%></a>
                <ul class="dropdown-menu">
                    <li>
                        <a href="/Pages/WrittenLeaderboards"><%=this.Text("NAVIGATION", "MENU_MYWRITTENBATTLES2")%></a>
                    </li>
                    <li>
                        <a href="/Pages/AudioLeaderboards"><%=this.Text("NAVIGATION", "MENU_MYAUDIOBATTLES2")%></a>
                    </li>
                    <li class="divider">
                    </li>
                     <li>
                        <a href="/Pages/TopMusicTracks"><%=this.Text("NAVIGATION", "MENU_MUSIC")%></a>
                    </li>
                </ul>
            </li>
        </ul>
        <ul class="nav navbar-nav navbar-right">
                        <li>
                <a href='/Pages/Profile/<%=this.PageContext.PageUserID%>'><%=this.Text("NAVIGATION", "MENU_PROFILE")%></a>
            </li>
        </ul>
    </div><!--/.nav-collapse -->
</nav>