<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PMWindow.ascx.cs" Inherits="FreestyleOnline.Controls.Generic.PMWindow" %>
<ext:Window AutoRender="false" BodyPadding="10" Height="300" Hidden="true" ID="PrivateMsgWindow" runat="server"  Width="500">
    <Items>
        <ext:TextField AutoDataBind="true" ID="ToMsg" AllowBlank="false" FieldLabel="To" Text='<%#this.To%>' runat="server" Enabled="false" />
        <ext:TextField ID="Title" runat="server" FieldLabel="Subject" AllowBlank="false" />
        <ext:TextArea ID="MessageContent" AllowBlank="false" Width="400" FieldLabel="Message" Flex="1" Margins="0" runat="server" />
    </Items>
    <Buttons>
        <ext:Button runat="server" Text="Send">
            <Listeners>
                <Click Handler="#{DirectMethods}.SubmitPrivateMessage();"></Click>          
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>