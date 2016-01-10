<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BattleVote.ascx.cs" Inherits="FreestyleOnline.Controls.BattleVote" %>
<script type="text/javascript">
    var votesuccessfull = '<%=this.Text("BATTLES", "VOTE_SUCCESS")%>';
    var submitbtntext = '<%=this.Text("COMMON", "COMMON_SUBMIT")%>';
    var votefail = '<%=this.Text("BATTLES", "BATTLE_NULLRATING")%>';
</script>
<div class="row clearfix">
    <div class="col-md-12 column">
        <table class="table table-bordered table-hover">
            <thead>
                <tr>
                    <th>
                        <%= this.Text("TITLE_STATS", "TITLE_STATS") %>
                    </th>
                    <th>
                        <%= UserMembershipHelper.GetDisplayNameFromID(this.UserId1) %>
                    </th>
                    <th>
                        <%= UserMembershipHelper.GetDisplayNameFromID(this.UserId2) %>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <%= this.Text("BATTLES", "BATTLE_WORDPLAY") %>
                    </td>
                    <td>
                        <div id="wordplay1"></div>
                    </td>
                    <td>
                       <div id="wordplay2"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%= this.Text("BATTLES", "BATTLE_METAPHORES") %>
                    </td>
                    <td>
                        <div id="metaphore1"></div>
                    </td>
                    <td>
                        <div id="metaphore2"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%= this.Text("BATTLES", "BATTLE_FLOW") %>
                    </td>
                    <td>
                       <div id="flow1"></div>
                    </td>
                    <td>
                        <div id="flow2"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%= this.Text("BATTLES", "BATTLE_MULTIS") %>
                    </td>
                    <td>
                        <div id="multis1"></div>
                    </td>
                    <td>
                       <div id="multis2"></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%= this.Text("BATTLES", "BATTLE_PUNCHLINES") %>
                    </td>
                    <td>
                        <div id="punchlines1"></div>
                    </td>
                    <td>
                        <div id="punchlines2"></div>
                    </td>
                </tr>
            </tbody>
        </table>
          <div id="submitratinglayout"></div>
    </div>
</div>