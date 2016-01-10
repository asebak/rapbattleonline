<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WrittenBattles.aspx.cs" Inherits="FreestyleOnline.Pages.WrittenBattles" %>

<%@ Register TagPrefix="RAP" TagName="CountDown" Src="~/Controls/CountDown.ascx" %>
<%@ Register TagPrefix="RAP" TagName="WrittenBattleComments" Src="~/Controls/WrittenBattleComments.ascx" %>
<%@ Register TagPrefix="RAP" TagName="WrittenBattleVote" Src="~/Controls/BattleVote.ascx" %>
<%@ Register TagPrefix="RAP" TagName="BattleVoteDisplay" Src="~/Controls/BattleVoteDisplay.ascx" %>
<%@ Register TagPrefix="RAP" TagName="WrittenBattleChart" Src="~/Controls/RapBattleVotingGraph.ascx" %>
<%@ Register TagPrefix="RAP" TagName="ProfileLink" Src="~/Controls/ProfileLink.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            createBattleRatingFull('<%=this.Battle.User1Overall %>', '<%=this.Battle.User2Overall %>');
        });
        var barLength = parseInt('<%=this.Battle.Length%>');
        var length = parseInt('<%=this.BarLength%>');
    </script>
    <div class="tabbable" runat="server">
        <ul class="nav nav-tabs">
            <li class="active">
                <a href="#panel-481676" data-toggle="tab"><%=this.Text("BATTLES", "TITLE")%></a>
            </li>
            <li>
                <a href="#panel-710532" data-toggle="tab"><%=this.Text("COMMENTS", "COMMENTS_TITLE")%></a>
            </li>
            <li id="votetab"  runat="server">
                <a href="#panel-710533" data-toggle="tab"><%=this.Text("BATTLES", "TITLE_VOTE") %></a>
            </li>
            <li id="statstab" runat="server">
                <a href="#panel-710534" data-toggle="tab"><%=this.Text("BATTLES", "TITLE_STATS") %></a>
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
                                        <RAP:ProfileLink ID="BattleUser1" runat="server" />
                                    </div>
                                    <div class="col-md-9 column">
                                        <RAP:WrittenBattleChart ID="WrittenVoteGraph1" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <textarea onkeydown="return limitwritten(this);" style="resize: none;" id="BattleTextArea1" runat="server" class="form-control"></textarea>
                            <ext:Button AutoPostBack="true" AutoDataBind="true" runat="server" ID="BattleSubmit1" Text='<%#this.Text("COMMON", "COMMON_SUBMIT")%>'
                                OnCommand="BattleSubmit1_Command" />
                            <ext:Label ClientIDMode="Static" ID="TimerIcon" runat="server" />
                            <RAP:CountDown ID="WrittenTimer" runat="server" />
                        </div>
                        <div class="col-md-6 column">
                            <div id="User2Panel" runat="server" class="panel panel-default">
                                <div class="panel-heading" id="user2OverallRating"></div>
                                <div class="panel-body">
                                    <ext:Button AutoPostBack="true" AutoDataBind="true" runat="server" ID="BattleJoin2" Text='<%#this.Text("COMMON", "COMMON_JOIN")%>' Icon="ArrowJoin" OnCommand="BattleJoin2_Command" />
                                    <div class="col-md-3 column">
                                        <RAP:ProfileLink ID="BattleUser2" runat="server" />
                                    </div>
                                    <div class="col-md-9 column">
                                        <RAP:WrittenBattleChart ID="WrittenVoteGraph2" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <textarea onkeydown="return limitwritten(this);" style="resize: none;" id="BattleTextArea2" runat="server" class="form-control"></textarea>
                            <ext:Button AutoPostBack="true" runat="server" ID="BattleSubmit2" Text='<%#this.Text("COMMON", "COMMON_SUBMIT")%>'
                                AutoDataBind="true" OnCommand="BattleSubmit2_Command" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="tab-pane" id="panel-710532">
                <RAP:WrittenBattleComments ID="WrittenBattleComments1" runat="server" />
            </div>
            <div class="tab-pane" id="panel-710533">
                <RAP:WrittenBattleVote ID="WrittenVote" runat="server" />
            </div>
            <div class="tab-pane" id="panel-710534">
                <RAP:BattleVoteDisplay ID="WrittenVoteDisplay" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
