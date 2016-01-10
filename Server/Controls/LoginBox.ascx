<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginBox.ascx.cs" Inherits="FreestyleOnline.Controls.LoginBox" %>
<div id="LoginBox" style="display: inline-block;">
    <asp:Login ID="Login1" runat="server" RememberMeSet="True" OnLoginError="Login1_LoginError" OnLoggedIn="Login1_LoggedIn"
               OnAuthenticate="Login1_Authenticate" VisibleWhenLoggedIn="false">
        <LayoutTemplate>
            <ext:FormPanel AutoDataBind="true" StyleSpec="float:right;" Collapsible="true" Collapsed="true" runat="server" Title='<%#this.Text("COMMON", "COMMON_LOGIN") %>'
                           Width="280" Layout="Form">
                <Content>
                    <ext:TextField Width="275" AllowBlank="false" EmptyText='<%#this.Text("COMMON", "COMMON_USER") %>' ID="UserName" runat="server" IndicatorText="*" Cls="requiredField" />
                    <ext:TextField Width="275" AllowBlank="false" EmptyText='<%#this.Text("COMMON", "COMMON_PASSWORD") %>' ID="Password" InputType="Password" runat="server" Cls="requiredField" IndicatorText="*" />
                    <ext:HyperLink Text='<%#this.Text("COMMON", "COMMON_FORGOTPASSWORD") %>' NavigateUrl="~/forum/rap_recoverpassword" runat="server" />
                </Content>
                <Buttons>
                    <ext:Button AutoPostBack="true" ID="LoginButton" Text='<%#this.Text("COMMON", "COMMON_LOGIN") %>' 
                                OnClientClick="return handlepostback(#{LoginButton});" runat="server" CommandName="Login" ValidationGroup="Login1" />
                    <ext:Button AutoPostBack="true" ID="RegisterButton" OnClick="RegisterButton_Click" Text='<%#this.Text("COMMON", "COMMON_REGISTER") %>' 
                                runat="server" CommandName="Login" ValidationGroup="Login1" />
                </Buttons>
            </ext:FormPanel>
        </LayoutTemplate>
    </asp:Login>
</div>