<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserWrittenBattleStats.ascx.cs" Inherits="FreestyleOnline.Controls.Stats.UserWrittenBattleStats" %>
<div class="panel panel-default">
    <div class="panel-heading">
        <h3 class="panel-title"><%=this.Text("PROFILE", "WRITTEN_STATS") %></h3>
    </div>
    <div class="panel-body">
        <div class="progress">
            <div id="wordplay" runat="server" role="progressbar" aria-valuemin="0" aria-valuemax="100">
            </div>
        </div>
        <div class="progress">
            <div id="flow" runat="server" role="progressbar" aria-valuemin="0" aria-valuemax="100">
            </div>
        </div>
        <div class="progress">
            <div id="multis" runat="server" role="progressbar" aria-valuemin="0" aria-valuemax="100">
            </div>
        </div>
        <div class="progress">
            <div id="punchlines" runat="server" role="progressbar" aria-valuemin="0" aria-valuemax="100">
            </div>
        </div>
        <div class="progress">
            <div id="metaphores" runat="server" role="progressbar" aria-valuemin="0" aria-valuemax="100">
            </div>
        </div>
    </div>
</div>
