<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyHood.ascx.cs" Inherits="FreestyleOnline.Controls.MyHood" %>
<%@ Register TagPrefix="RAP" TagName="HoodBox" Src="~/Controls/HoodBox.ascx" %>
<div class="panel panel-default" id="MyHoods" runat="server">
    <div class="panel-heading">
        <h3 class="panel-title">
            <%= this.Text("HOOD", "MY") %>
        </h3>
    </div>
    <asp:GridView Width="100%" Height="100%" GridLines="None" ID="MyHoodGridView" AutoGenerateColumns="false" ShowHeader="false" runat="server" OnRowDataBound="MyHoodGridView_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <RAP:HoodBox ID="MyHoodBox" runat="server" />
                            <div id="MyHoodAdmin" runat="server">
                                <div class="panel-footer panel-faq" >
                                    <h3 class="panel-title">
                                        <%=this.Text("HOOD", "HOOD_EDIT") %>
                                    </h3>
                                    <div class="row clearfix">
                                        <div class="col-md-6 column">
                                            <ext:RadioGroup Layout="ColumnLayout" ColumnsWidths="1" FieldLabel='<%#this.Text("COMMON", "COMMON_PRIVACY") %>' ID="Privacy" runat="server" Vertical="false">
                                                <Items>
                                                    <ext:Radio ID="PublicityHood1" runat="server" Checked='<%#Convert.ToBoolean(Eval("IsPublic")) %>' BoxLabel='<%#this.Text("COMMON", "COMMON_PUBLIC") %>' InputValue="1" />
                                                    <ext:Radio ID="PublicityHood2" runat="server" Checked='<%#!Convert.ToBoolean(Eval("IsPublic")) %>' BoxLabel='<%#this.Text("COMMON", "COMMON_PRIVATE") %>' InputValue="0" />
                                                </Items>
                                            </ext:RadioGroup>
                                            <br />
                                            <ext:TextArea EnforceMaxLength="true" MaxLength="200" FieldLabel='<%#this.Text("COMMON", "COMMON_DESCRIPTION") %>' Text='<%#Eval("Details") %>' ID="DescriptionMyHood" runat="server" />
                                            <ext:Button AutoPostBack="true" ID="SubmitButton" Text='<%#this.Text("COMMON", "COMMON_SUBMIT") %>' Cls="btn btn-default" 
                                                OnCommand="SubmitButton_Command" runat="server"/>
                                        </div>
                                        <div class="col-md-6 column">
                                            <ext:ComboBox PageSize="5" FieldLabel='<%#this.Text("HOOD", "HOOD_EDITMEMBERS") %>' ID="HoodMembers"
                                                DisplayField="UserNameAuto" Editable="false" EmptyText='<%#this.Text("COMMON", "COMMON_USERSELECT") %>'
                                                ForceSelection="true" QueryMode="Local" runat="server" TriggerAction="All" ValueField="HoodUserId">
                                                <Triggers>
                                                    <ext:FieldTrigger AutoDataBind="true" Icon="SimplePlus" Qtip='<%#this.Text("HOOD", "HOOD_PROMOTE") %>' />
                                                    <ext:FieldTrigger AutoDataBind="true" Icon="Clear" Qtip='<%#this.Text("HOOD", "HOOD_REMOVEMEMBER") %>' />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick AutoPostBack="false" Handler="#{DirectMethods}.HoodMemberTrigger(index, Ext.encode(#{HoodMembers}.getValue()))" />
                                                </Listeners>
                                                <Store>
                                                    <ext:Store ID="HoodMembersStore" PageSize="5" IsPagingStore="true" runat="server">
                                                        <Model>
                                                            <ext:Model runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="UserNameAuto" />
                                                                    <ext:ModelField Name="HoodUserId" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <ListConfig>
                                                    <ItemTpl runat="server">
                                                        <Html>
                                                            <div class="list-item">
                                                        <h3>{UserNameAuto}</h3>
                                                    </div>
                                                        </Html>
                                                    </ItemTpl>
                                                </ListConfig>
                                            </ext:ComboBox>
                                            <ext:ComboBox FieldLabel='<%#this.Text("HOOD", "HOOD_INVITEMEMBER") %>' ID="InviteBox" DisplayField="UserNameAuto" Editable="false"
                                                EmptyText='<%#this.Text("COMMON", "COMMON_USERSELECT") %>' ForceSelection="true" QueryMode="Local" runat="server"
                                                PageSize="10" TriggerAction="All" ValueField="InviteHoodUser">
                                                <Triggers>
                                                    <ext:FieldTrigger AutoDataBind="true" Icon="SimplePlus" Qtip='<%#this.Text("HOOD", "HOOD_INVITEMEMBER") %>' />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick  AutoPostBack="false" Handler="#{DirectMethods}.InviteMemberTrigger(index, Ext.encode(#{InviteBox}.getValue()))" />
                                                </Listeners>
                                                <Store>
                                                    <ext:Store IsPagingStore="true" PageSize="10" ID="InviteMembersStore" runat="server">
                                                        <Model>
                                                            <ext:Model runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="UserNameAuto" />
                                                                    <ext:ModelField Name="InviteHoodUser" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <ListConfig>
                                                    <ItemTpl runat="server">
                                                        <Html>
                                                            <div class="list-item">
                                                        <h3>{UserNameAuto}</h3>
                                                    </div>
                                                        </Html>
                                                    </ItemTpl>
                                                </ListConfig>
                                            </ext:ComboBox>
                                        </div>
                                    </div>
                                </div>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
<asp:PlaceHolder ID="NoHoods" runat="server"></asp:PlaceHolder>
