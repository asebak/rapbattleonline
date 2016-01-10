<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AudioBattles.aspx.cs" Inherits="FreestyleOnline.Pages.AudioBattles" %>

<%@ Register TagPrefix="RAP" TagName="CountDown" Src="~/Controls/CountDown.ascx" %>
<%@ Register TagPrefix="RAP" TagName="AudioBattleComments" Src="~/Controls/AudioBattleComments.ascx" %>
<%@ Register TagPrefix="RAP" TagName="AudioBattleVote" Src="~/Controls/BattleVote.ascx" %>
<%@ Register TagPrefix="RAP" TagName="BattleVoteDisplay" Src="~/Controls/BattleVoteDisplay.ascx" %>
<%@ Register TagPrefix="RAP" TagName="AudioBattleChart" Src="~/Controls/RapBattleVotingGraph.ascx" %>
<%@ Register TagPrefix="RAP" TagName="ProfileLink" Src="~/Controls/ProfileLink.ascx" %>
<%@ Register TagPrefix="RAP" TagName="AudioBattleListener" Src="~/Mobile/Controls/RapBattleAudioListener.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            createBattleRatingFull('<%= this.Battle.User1Overall %>', '<%= this.Battle.User2Overall %>');
        });
    </script>
    <div class="tabbable" runat="server">
        <ul class="nav nav-tabs">
            <li class="active">
                <a href="#panel-481676" data-toggle="tab"><%= this.Text("BATTLES", "TITLE") %></a>
            </li>
            <li>
                <a href="#panel-710532" data-toggle="tab"><%= this.Text("COMMENTS", "COMMENTS_TITLE") %></a>
            </li>
            <li id="votetab" runat="server">
                <a href="#panel-710533" data-toggle="tab"><%= this.Text("BATTLES", "TITLE_VOTE") %></a>
            </li>
            <li id="statstab" runat="server">
                <a href="#panel-710534" data-toggle="tab"><%= this.Text("BATTLES", "TITLE_STATS") %></a>
            </li>
        </ul>
        <div class="tab-content">
            <div class="tab-pane active" id="panel-481676">
                <div class="panel-body">
                    <div class="row clearfix">
                        <div class="col-md-6 column">
                            <div id="User1Panel" runat="server" class="panel panel-default">
                                <div class="panel-heading" id="user1OverallRating"></div>
                                <div class="panel-body">
                                    <div class="col-md-3 column">
                                        <RAP:ProfileLink runat="server" ID="BattleUser1" />
                                    </div>
                                    <div class="col-md-9 column">
                                        <RAP:AudioBattleChart ID="AudioBattleChart1" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <ext:Label ClientIDMode="Static" ID="TimerIcon" runat="server"></ext:Label>
                            <RAP:CountDown ID="AudioTimer" runat="server" />
                        </div>
                        <div class="col-md-6 column">
                            <div id="User2Panel" runat="server" class="panel panel-default">
                                <div class="panel-heading" id="user2OverallRating"></div>
                                <div class="panel-body">
                                    <ext:Button ID="JoinButton" Text="Join" AutoPostBack="true" OnCommand="JoinButton_Command" runat="server" />
                                    <div class="col-md-3 column">
                                        <RAP:ProfileLink runat="server" ID="BattleUser2" />
                                    </div>
                                    <div class="col-md-9 column">
                                        <RAP:AudioBattleChart ID="AudioBattleGraph2" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-body" id="silverlightappbody" runat="server">
                        <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="100%" height="100%">
                            <param name="source" value="/ClientBin/RapBattleAudio.xap" />
                            <param name="onError" value="onSilverlightError" />
                            <param name="background" value="white" />
                            <param name="minRuntimeVersion" value="4.0.50826.0" />
                            <param name="initparams" id="initParams" runat="server" value="" />
                            <param name="autoUpgrade" value="true" />
                            <param name="windowless" value="true" />
                            <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=4.0.50826.0" style="text-decoration: none">
                                <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Get Microsoft Silverlight" style="border-style: none" />
                            </a>
                        </object>
                        <iframe id="_sl_historyFrame" style="border: 0px; height: 0px; visibility: hidden; width: 0px;"></iframe>
                    </div>
                   <div class="panel-body" id="mobileaudioapp" runat="server">
                       <RAP:AudioBattleListener ID="ListenerAudio" runat="server"/>
                       <asp:PlaceHolder ID="NoSilverlight" runat="server"/>
                       </div>
                </div>
            </div>
            <div class="tab-pane" id="panel-710532">
                <RAP:AudioBattleComments runat="server" />
            </div>
            <div class="tab-pane" id="panel-710533">
                <RAP:AudioBattleVote ID="AudioVote" runat="server" />
            </div>
            <div class="tab-pane" id="panel-710534">
                <RAP:BattleVoteDisplay ID="AudioVoteDisplay" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
