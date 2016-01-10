<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TournamentManager.ascx.cs" Inherits="FreestyleOnline.Controls.Admin.TournamentManager" %>
<div class="row clearfix">
    <div class="col-md-12 column">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <%=this.Text("ADMIN", "TOURNAMENT_CREATE") %>
                </h3>
            </div>
            <div class="panel-body">
                <ext:DropDownField Cls="form-control" AutoDataBind="true" Editable="false" ID="BattleTypeField" FieldLabel='<%#this.Text("COMMON", "BATTLETYPE")%>' 
                    MatchFieldWidth="false" PickerAlign="tl-tr?" runat="server" TriggerIcon="SimpleArrowDown">
                    <Component>
                        <ext:Panel Layout="HBoxLayout" runat="server">
                            <LayoutConfig>
                                <ext:HBoxLayoutConfig Align="Stretch" />
                            </LayoutConfig>
                            <Items>
                                <ext:MenuPanel Border="false" Flex="1" runat="server" SaveSelection="false">
                                    <Menu runat="server" ShowSeparator="false">
                                        <Items>
                                            <ext:MenuItem runat="server" Text='<%#this.Text("BATTLES", "AUDIO")%>' Icon="MusicNote" />
                                            <ext:MenuItem runat="server" Text='<%#this.Text("BATTLES", "WRITTEN")%>' Icon="Note" />
                                            <ext:MenuItem runat="server" Text='<%#this.Text("BATTLES", "VIDEO")%>' Icon="Television" Selectable="false" />
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
                <ext:DropDownField Cls="form-control" AutoDataBind="true" Editable="false" ID="ContestantsField" FieldLabel='<%#this.Text("TOURNAMENTS", "CONTESTANTS")%>' 
                    MatchFieldWidth="false" PickerAlign="tl-tr?" runat="server" TriggerIcon="SimpleArrowDown">
                    <Component>
                        <ext:Panel Layout="HBoxLayout" runat="server">
                            <LayoutConfig>
                                <ext:HBoxLayoutConfig Align="Stretch" />
                            </LayoutConfig>
                            <Items>
                                <ext:MenuPanel Border="false" Flex="1" runat="server" SaveSelection="false">
                                    <Menu runat="server" ShowSeparator="false">
                                        <Items>
                                            <ext:MenuItem runat="server" Text="4" />
                                            <ext:MenuItem runat="server" Text="8" />
                                            <ext:MenuItem runat="server" Text="16" />
                                            <ext:MenuItem runat="server" Text="32" />
                                            <ext:MenuItem runat="server" Text="64" />
                                            <ext:MenuItem runat="server" Text="128" />
                                            <ext:MenuItem runat="server" Text="256" />
                                        </Items>
                                        <Listeners>
                                            <Click Handler="#{ContestantsField}.setValue(menuItem.text);" />
                                        </Listeners>
                                    </Menu>
                                </ext:MenuPanel>
                            </Items>
                        </ext:Panel>
                    </Component>
                </ext:DropDownField>
            </div>
            <div class="panel-footer">
                <ext:Button Cls="btn btn-default" AutoDataBind="true" Text='<%#this.Text("COMMON", "COMMON_SUBMIT")%>'
                    ID="CreateTournament" OnClick="CreateTournament_Click" AutoPostBack="true" runat="server" />
            </div>
        </div>
    </div>
</div>
