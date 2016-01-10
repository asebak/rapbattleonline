<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateAudioBattle.ascx.cs" Inherits="FreestyleOnline.Controls.CreateAudioBattle" %>
<%@ Register TagPrefix="RAP" TagName="UsersComboBox" Src="~/Controls/Generic/UsersComboBox.ascx" %>
<div class="row clearfix">
    <div class="col-md-12 column">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <%=this.Text("BATTLES", "AUDIO_CREATE") %>
                </h3>
            </div>
            <div class="panel-body">
        <ext:RadioGroup Cls="form-control" AutoDataBind="true" Padding="2" Layout="ColumnLayout" ColumnsWidths="1" FieldLabel='<%#this.Text("COMMON", "COMMON_PRIVACY") %>' ID="PrivacyGroup"
             runat="server" Vertical="false">
            <Items>
                <ext:Radio ID="Radio1" AutoDataBind="true" Checked="true" BoxLabel='<%#this.Text("COMMON", "COMMON_PUBLIC") %>' runat="server" InputValue="1"/>
                <ext:Radio ID="Radio2" AutoDataBind="true" runat="server" BoxLabel='<%#this.Text("COMMON", "COMMON_PRIVATE") %>' InputValue="0"/>
            </Items>
            <Listeners>
                <Change Handler="#{DirectMethods}.HideComboBox();"></Change>
            </Listeners>
        </ext:RadioGroup>
        <ext:Container ID="Container1" runat="server">
            <Content>
                <RAP:UsersComboBox runat="server" ID="AudioBattleUser" />
            </Content>
        </ext:Container>
        <ext:RadioGroup Cls="form-control" AutoDataBind="true" Layout="ColumnLayout" ColumnsWidths="1" FieldLabel='<%#this.Text("BATTLES", "AUDIO_LENGTH") %>' ID="AudioLengthGroup" runat="server"
             Vertical="false">
            <Items>
                <ext:Radio AutoDataBind="true" ID="Radio3" Checked="true" BoxLabel="30 seconds" InputValue="30" runat="server"/>
                <ext:Radio AutoDataBind="true" ID="Radio4" runat="server" BoxLabel="1 minute" InputValue="60"/>
                <ext:Radio AutoDataBind="true" ID="Radio5" runat="server" BoxLabel="2 minutes" InputValue="120"/>
            </Items>
        </ext:RadioGroup>
        <ext:DateField Cls="form-control" AutoDataBind="true" Editable="false" ID="DateBox" FieldLabel='<%#this.Text("COMMON", "COMMON_ENDDATE") %>' runat="server">
            <Listeners>
                <Select Handler="#{DirectMethods}.HandleDate();"></Select>
            </Listeners>
        </ext:DateField>
            </div>
            <div class="panel-footer">
                <ext:Button Cls="btn btn-default" AutoDataBind="true" runat="server" Text='<%#this.Text("COMMON", "COMMON_CREATE") %>' ID="CreateBattleAudio" AutoPostBack="true"
                      OnClick="CreateBattleAudio_Click"/>
            </div>
        </div>
    </div>
</div>