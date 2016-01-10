<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BattleVoteDisplay.ascx.cs" Inherits="FreestyleOnline.Controls.BattleVoteDisplay" %>
<%@ Register TagPrefix="RAP" TagName="ProfileLink" Src="~/Controls/ProfileLink.ascx" %>
<table class="table table-hover table-bordered">
    <thead>
        <tr>
            <th>
             Voter  
            </th>
            <th>
                <%=this.DisplayName1 %>
            </th>
            <th>
                <%=this.DisplayName2 %>
            </th>
        </tr>
    </thead>
    <tbody>
        <asp:Repeater runat="server" ID="VoteDisplay" OnItemDataBound="VoteDisplay_ItemDataBound">
            <ItemTemplate>
             <script type="text/javascript">
                 CreateDisplayOfBattleVotes("<%#Eval("User1Wordplay") %>", "<%#Eval("User1Metaphores") %>", "<%#Eval("User1Flow") %>",
                     "<%#Eval("User1Multis") %>", "<%#Eval("User1Punchlines") %>", "<%#Eval("User2Wordplay") %>", "<%#Eval("User2Metaphores") %>",
                "<%#Eval("User2Flow") %>", "<%#Eval("User2Multis") %>", "<%#Eval("User2Punchlines") %>", "<%#Eval(this.SqlColumnIdTag) %>");
             </script>   
                   <tr id="votingcell" runat="server">
                    <td>
                    <RAP:ProfileLink ID="ProfileLink1" runat="server" UserId='<%#Eval("UserID") %>' />
                    </td>
                    <td>
                     <div id='user1rating<%#Eval(this.SqlColumnIdTag) %>'></div>
                    </td>
                    <td>
                        <div id='user2rating<%#Eval(this.SqlColumnIdTag) %>'></div>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
