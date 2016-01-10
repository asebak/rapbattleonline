<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AudioBattlesList.ascx.cs" Inherits="FreestyleOnline.Controls.AudioBattlesList" %>
<%@ Import Namespace="Common.Types.Enums" %>
<%@ Import Namespace="FreestyleOnline.classes.Providers" %>
<%@ Register TagPrefix="RAP" TagName="Pager" Src="~/Controls/Generic/Pager.ascx" %>
<%@ Register TagPrefix="RAP" TagName="RapBattleBox" Src="~/Controls/RapBattleBox.ascx" %>

<asp:GridView GridLines="None" runat="server" ID="MyAudioBattles" AutoGenerateColumns="False" Width="100%" ShowHeader="false">
    <Columns>
       <asp:TemplateField>
<%--        <asp:TemplateField ItemStyle-CssClass="sapUiTf sapUiTfBack sapUiTfBrd sapUiTfStd sapUiTxtA">--%>
            <ItemTemplate>
                <RAP:RapBattleBox
                    UserId1='<%# DataBinder.Eval(Container.DataItem, "UserId1") %>' 
                    UserId2='<%# DataBinder.Eval(Container.DataItem, "UserId2") %>'  
                    EndDate='<%# DataBinder.Eval(Container.DataItem, "EndDate") %>'
                    WinnerId='<%# DataBinder.Eval(Container.DataItem, "WinnerId") %>'
                    User1Overall='<%# DataBinder.Eval(Container.DataItem, "User1Overall") %>'
                    User2Overall='<%# DataBinder.Eval(Container.DataItem, "User2Overall") %>'
                    BattleUrl='<%#this.GetService<UrlProvider>().GetUrl("~/Pages/AudioBattles", DataBinder.Eval(Container.DataItem, "BattleId")) %>' 
                    BattleId='<%#DataBinder.Eval(Container.DataItem, "BattleId") %>'
                    Type='<%#RapBattleType.Audio%>' runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<RAP:Pager ID="AudioBattlesPager" runat="server" />
<asp:PlaceHolder ID="NoAudioBattles" runat="server" />