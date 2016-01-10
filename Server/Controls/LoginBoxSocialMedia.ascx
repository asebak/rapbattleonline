<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginBoxSocialMedia.ascx.cs" Inherits="FreestyleOnline.Controls.LoginBoxSocialMedia" %>
<asp:UpdatePanel ID="UpdateLoginPanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div align="center">
            <asp:Login ID="Login1" runat="server" RememberMeSet="True" OnLoginError="Login1_LoginError" OnLoggedIn="Login1_LoggedIn"
                OnAuthenticate="Login1_Authenticate" VisibleWhenLoggedIn="True">
                <LayoutTemplate>
                    <table align="center" width="100%" border="1" style="border: 1px solid  #428bca" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" class="content" width="100%">
                                    <tr runat="server" style="background-color: #428bca; color: white; font-weight: bold; font-size: 10.5pt;">
                                        <td colspan="2" class="post">
                                            <label>Social Media Login</label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="SingleSignOnOptionsRow" visible="False">
                                        <td colspan="2" class="post">
                                            <asp:RadioButtonList runat="server" ID="SingleSignOnOptions" AutoPostBack="true"
                                                OnSelectedIndexChanged="SingleSignOnOptionsChanged">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="UserNameRow">
                                        <td align="right" class="postheader">
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">
                                                <YAF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="username" />
                                            </asp:Label>
                                        </td>
                                        <td class="post">
                                            <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="PasswordRow">
                                        <td align="right" class="postheader" style="height: 24px">
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">
                                                <YAF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="PASSWORD" />
                                            </asp:Label>
                                        </td>
                                        <td class="post" style="height: 24px">
                                            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="post">
                                            <asp:CheckBox ID="RememberMe" runat="server"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="postfooter" style="text-align: center; height: 24px">
                                            <asp:Button ID="LoginButton" runat="server" CssClass="btn btn-default" CommandName="Login" ValidationGroup="Login1" />
                                            <asp:Button ID="PasswordRecovery" runat="server" CausesValidation="false" class="btn btn-default"
                                                OnClick="PasswordRecovery_Click" />
                                            <asp:PlaceHolder ID="RegisterLinkPlaceHolder" runat="server" Visible="false">
                                                <div>
                                                    <u>
                                                        <asp:LinkButton ID="RegisterLink" runat="server" OnClick="RegisterLinkClick"></asp:LinkButton></u>
                                                </div>
                                            </asp:PlaceHolder>
                                        </td>
                                    </tr>
                                    <tr id="SingleSignOnRow" runat="server" visible="false">
                                        <td style="text-align: center" colspan="2" class="postfooter">
                                            <asp:LinkButton runat="server" ID="FacebookRegister" CssClass="authLogin facebookLogin btn btn-default" Visible="False" OnClick="FacebookFormClick"></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="TwitterRegister" CssClass="authLogin twitterLogin btn btn-default" Visible="False" OnClick="TwitterFormClick"></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="GoogleRegister" CssClass="authLogin googleLogin btn btn-default" Visible="False" OnClick="GoogleFormClick"></asp:LinkButton>

                                            <asp:PlaceHolder ID="FacebookHolder" runat="server" Visible="false">
                                                <a id="FacebookLogin" runat="server" class="authLogin facebookLogin">
                                                    <YAF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="FACEBOOK_LOGIN" />
                                                </a>
                                            </asp:PlaceHolder>
                                            <asp:PlaceHolder ID="TwitterHolder" runat="server" Visible="false">
                                                <a id="TwitterLogin" runat="server" class="authLogin twitterLogin">
                                                    <YAF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="TWITTER_LOGIN" />
                                                </a>
                                            </asp:PlaceHolder>
                                            <asp:PlaceHolder ID="GoogleHolder" runat="server" Visible="false">
                                                <a id="GoogleLogin" runat="server" class="authLogin googleLogin">
                                                    <YAF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedTag="GOOGLE_LOGIN" />
                                                </a>
                                            </asp:PlaceHolder>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:Login>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
