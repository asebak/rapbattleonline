<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsersComboBox.ascx.cs" Inherits="FreestyleOnline.Controls.Generic.UsersComboBox" %>
<ext:ComboBox Cls="form-control" AutoDataBind="true" Visible='<%#this.IsVisible %>' AllowBlank="false" Width="400"
              FieldLabel='<%#this.Text("COMMON", "COMMON_USER") %>' ID="UserComboxBoxList" DisplayField="UserNameAuto" Editable="false" 
              EmptyText='<%#this.Text("COMMON", "COMMON_USERSELECT") %>' ForceSelection="true" QueryMode="Local" runat="server"
              PageSize="10" TriggerAction="All" ValueField="UserNameAuto">
    <Listeners>
        <Select Handler="#{DirectMethods}.HandleUserSelection();"></Select>
    </Listeners>
    <Store>
        <ext:Store ID="UserAutoStore" IsPagingStore="true" PageSize="10" runat="server">
            <Model>
                <ext:Model runat="server">
                    <Fields>
                        <ext:ModelField Name="UserNameAuto" />
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