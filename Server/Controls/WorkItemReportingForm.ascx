<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkItemReportingForm.ascx.cs" Inherits="FreestyleOnline.Controls.WorkItemReportingForm" %>
<ext:Window AutoDataBind="true" AutoRender="false" BodyPadding="10" Height="300" Hidden="true" ID="WorkReporterWindow" runat="server"  Width="500">
    <Items>
        <ext:Label runat="server" />
        <ext:TextField AutoDataBind="true" ID="TitleMsg" AllowBlank="false" FieldLabel='<%#this.Text("BUG", "TYPE") %>' runat="server" />
        <ext:TextArea ID="MessageContent" AllowBlank="false" Width="400" FieldLabel='<%#this.Text("BUG", "STEPS") %>' Flex="1" Margins="0" runat="server" />
    </Items>
    <Buttons>
        <ext:Button ID="SubmitBtn" runat="server" Text='<%#this.Text("COMMON", "COMMON_SUBMIT") %>'>
            <Listeners>
                <Click Handler="#{DirectMethods}.SubmitBugReport();"></Click>          
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>