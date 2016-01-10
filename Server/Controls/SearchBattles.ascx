<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchBattles.ascx.cs" Inherits="FreestyleOnline.Controls.SearchBattles" %>
<%@ Import Namespace="Common.Types.Enums" %>
<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<%@ Register TagPrefix="RAP" TagName="RapBattleBox" Src="~/Controls/RapBattleBox.ascx" %>
<%@ Register TagPrefix="RAP" TagName="UsersComboBox" Src="~/Controls/Generic/UsersComboBox.ascx" %>
<ext:FormPanel runat="server" Title="Search Battles">
    <Content>
        <RAP:UsersComboBox ID="Users" runat="server" />
        <ext:DropDownField Editable="false" ID="BattleTypeField" FieldLabel="Battle Type" MatchFieldWidth="false" PickerAlign="tl-tr?" runat="server" TriggerIcon="SimpleArrowDown">
            <Component>
                <ext:Panel Height="90" Layout="HBoxLayout" runat="server" Width="400">
                    <LayoutConfig>
                        <ext:HBoxLayoutConfig Align="Stretch" />
                    </LayoutConfig>
                    <Items>
                        <ext:MenuPanel Border="false" Flex="1" runat="server" SaveSelection="false">
                            <Menu runat="server" ShowSeparator="false">
                                <Items>
                                    <ext:MenuItem runat="server" Text="Audio" Icon="MusicNote" />
                                    <ext:MenuItem runat="server" Text="Written" Icon="Note" />
                                    <ext:MenuItem runat="server" Text="Video" Icon="Television" Selectable="false"/>
                                </Items>
                                <Listeners>
                                    <Click Handler="#{BattleTypeField}.setValue(menuItem.text);" />
                                </Listeners>
                            </Menu>
                        </ext:MenuPanel>
                    </Items>
                </ext:Panel>
            </Component>
        </ext:DropDownField>
    </Content>
</ext:FormPanel>
<ext:Button AutoDataBind="true" Text='<%#this.Text("COMMON", "COMMON_SUBMIT") %>' Icon="Find" ID="OnSearchButton" runat="server" AutoPostBack="true" OnClick="OnSearchButton_Click" />
<asp:GridView ID="SearchBattlesGrid" runat="server" AutoGenerateColumns="False" Width="100%"
              DataKeyNames="BattleId" BackColor="WhiteSmoke" ShowHeader="false">
    <Columns>
        <asp:TemplateField ItemStyle-CssClass="sapUiTf sapUiTfBack sapUiTfBrd sapUiTfStd sapUiTxtA">
            <ItemTemplate>
                <RAP:RapBattleBox
                    UserId1='<%# DataBinder.Eval(Container.DataItem, "UserId1") %>' 
                    UserId2='<%# DataBinder.Eval(Container.DataItem, "UserId2") %>'  
                    EndDate='<%# DataBinder.Eval(Container.DataItem, "EndDate") %>'
                    WinnerId='<%# DataBinder.Eval(Container.DataItem, "WinnerId") %>'
                    User1Overall='<%# DataBinder.Eval(Container.DataItem, "User1Overall") %>'
                    User2Overall='<%# DataBinder.Eval(Container.DataItem, "User2Overall") %>'
                    BattleUrl='<%# (this.Type == RapBattleType.Written) ? FriendlyUrl.Href("~/Pages/WrittenBattles", DataBinder.Eval(Container.DataItem, "BattleId")) : FriendlyUrl.Href("~/Pages/AudioBattles", DataBinder.Eval(Container.DataItem, "BattleId")) %>' 
                    BattleId='<%#DataBinder.Eval(Container.DataItem, "BattleId") %>'
                    runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<ext:Label ID="SearchResults" Visible="false" runat="server">
</ext:Label>