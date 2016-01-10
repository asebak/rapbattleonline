<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewWrittenBattles.ascx.cs" Inherits="FreestyleOnline.Controls.Featured.NewWrittenBattles" %>
<%@ Import Namespace="FreestyleOnline.classes.Providers" %>
<%@ Register TagPrefix="RAP" TagName="RapBattleBox" Src="~/Controls/RapBattleBox.ascx" %>
<div id="newWrittenBattles" class="jcarousel-skin-tango">
    <ul>
        <asp:Repeater ID="NewWrittenBattlesRepeater" runat="server">
            <ItemTemplate>
                <li class="sapUiTf sapUiTfBack sapUiTfBrd sapUiTfStd sapUiTxtA">
                    <RAP:RapBattleBox
                        UserId1='<%# DataBinder.Eval(Container.DataItem, "UserId1") %>' 
                        UserId2='<%# DataBinder.Eval(Container.DataItem, "UserId2") %>'  
                        EndDate='<%# DataBinder.Eval(Container.DataItem, "EndDate") %>'
                        WinnerId='<%# DataBinder.Eval(Container.DataItem, "WinnerId") %>'
                        User1Overall='<%# DataBinder.Eval(Container.DataItem, "User1Overall") %>'
                        User2Overall='<%# DataBinder.Eval(Container.DataItem, "User2Overall") %>'
                        BattleUrl='<%#this.GetService<UrlProvider>().GetUrl("~/Pages/WrittenBattles", DataBinder.Eval(Container.DataItem, "BattleId")) %>' 
                        BattleId='<%#DataBinder.Eval(Container.DataItem, "BattleId") %>'
                        runat="server" />
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>