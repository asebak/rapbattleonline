<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserAudioBattleStats.ascx.cs" Inherits="FreestyleOnline.Controls.Stats.UserAudioBattleStats" %>
<div class="panel panel-default">
    <div class="panel-heading">
        <h3 class="panel-title"><%=this.Text("PROFILE","AUDIO_STATS")%></h3>
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
