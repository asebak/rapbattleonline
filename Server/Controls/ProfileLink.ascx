<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileLink.ascx.cs" Inherits="FreestyleOnline.Controls.ProfileLink" %>
<%@ Import Namespace="YAF.Types.Extensions" %>

<ext:ImageButton AutoDataBind="true" Width="60px" Height="60px" runat="server" ID="ProfileImageButton"
                 ToolTip='<%#this.Text("PROFILE", "PROFILE_VIEW").FormatWith(UserMembershipHelper.GetDisplayNameFromID(this.UserId)) %>'
                 ImageUrl='<%# this.Get<IAvatars>().GetAvatarUrlForUser(this.UserId) %>' OnDirectClick="ProfileImageButton_DirectClick"/>