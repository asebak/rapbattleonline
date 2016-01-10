<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WrittenBattlesList.ascx.cs" Inherits="FreestyleOnline.Controls.WrittenBattlesList" %>
<%@ Import Namespace="Common.Types.Enums" %>
<%@ Import Namespace="FreestyleOnline.classes.Providers" %>
<%@ Register TagPrefix="RAP" TagName="Pager" Src="~/Controls/Generic/Pager.ascx" %>
<%@ Register TagPrefix="RAP" TagName="RapBattleBox" Src="~/Controls/RapBattleBox.ascx" %>
<asp:GridView GridLines="None" runat="server" ID="MyWrittenBattles" AutoGenerateColumns="False" Width="100%" ShowHeader="false">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <RAP:RapBattleBox
                    UserId1='<%# DataBinder.Eval(Container.DataItem, "UserId1") %>' 
                    UserId2='<%# DataBinder.Eval(Container.DataItem, "UserId2") %>'  
                    EndDate='<%# DataBinder.Eval(Container.DataItem, "EndDate") %>'
                    WinnerId='<%# DataBinder.Eval(Container.DataItem, "WinnerId") %>'
                    User1Overall='<%# DataBinder.Eval(Container.DataItem, "User1Overall") %>'
                    User2Overall='<%# DataBinder.Eval(Container.DataItem, "User2Overall") %>'
                    BattleUrl='<%#this.GetService<UrlProvider>().GetUrl("~/Pages/WrittenBattles", DataBinder.Eval(Container.DataItem, "BattleId")) %>' 
                    BattleId='<%#DataBinder.Eval(Container.DataItem, "BattleId") %>'
                    User1Verse='<%#DataBinder.Eval(Container.DataItem, "User1Verse") %>' 
                    User2Verse='<%#DataBinder.Eval(Container.DataItem, "User2Verse") %>'
                    Type='<%#RapBattleType.Written%>' runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<RAP:Pager ID="WrittenBattlesPager" runat="server" />
<asp:PlaceHolder ID="NoWritten" runat="server"/>