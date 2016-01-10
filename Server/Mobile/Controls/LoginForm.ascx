<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.ascx.cs" Inherits="FreestyleOnline.Mobile.Controls.LoginForm" %>

<asp:Login ClientIDMode="Static" ID="Login1" runat="server" OnLoginError="Login1_LoginError" OnLoggedIn="Login1_LoggedIn"
    OnAuthenticate="Login1_Authenticate" VisibleWhenLoggedIn="false" Width="100%">
    <LayoutTemplate>
        <div class="row clearfix" style="width: 100%">
            <asp:TextBox ID="UserName" CssClass="form-control" placeholder="Username or Email" runat="server" />
        </div>
        <div class="row clearfix" style="width: 100%">
            <asp:TextBox ID="Password" CssClass="form-control" runat="server" placeholder="Password" TextMode="Password" />
        </div>
        <div class="row clearfix" style="width: 100%">
            <asp:Button ID="Button1" CommandName="Login" runat="server" Text="Login" CssClass="btn btn-lg btn-primary btn-block" ValidationGroup="Login1" />
<%--            <div class="checkbox">
                <label>
                    <input type="checkbox">
                    Keep me signed in
                </label>
            </div>--%>
        </div>
    </LayoutTemplate>
</asp:Login>
