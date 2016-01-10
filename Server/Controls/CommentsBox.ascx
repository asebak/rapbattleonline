<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommentsBox.ascx.cs" Inherits="FreestyleOnline.Controls.CommentsBox" %>
<%@ Import Namespace="YAF.Types.Extensions" %>
<%@ Register TagPrefix="RAP" TagName="ProfileLink" Src="~/Controls/ProfileLink.ascx" %>
<div class="container-fluid">
    <div class="row-fluid ">
        <div class="col-md-2 column">
            <RAP:ProfileLink UserID='<%#this.UserId %>' runat="server" />
        </div>
        <div class="col-md-10 column">
            <div class="row-fluid ">
                <ext:Label Cls="commentsBoxTitle" ID="TitleToDisplay" runat="server" Text='<%#this.Title %>'/>
            </div>
            <div class="row-fluid ">
                <ext:Label Cls="commentsBoxUser" ID="UserLabel" runat="server" Text='<%#this.Text("COMMON", "COMMON_POSTEDBY").FormatWith(UserMembershipHelper.GetDisplayNameFromID(this.UserId)) %>'/>            
            </div>
            <div class="row-fluid">
                <YAF:DisplayDateTime runat="server" DateTime='<%#this.Date%>'/>
            </div> 
            <div class="row-fluid">
                <ext:Label Cls="commentsBoxComment" ID="PostedComment" runat="server" Html='<%#this.Message %>'/>
            </div>
             <div class="row-fluid">
                 <ext:Hyperlink Cls="commentsBoxComment" ID="Label1" runat="server" Text='<%#this.Text("NEWS", "VIEW_COMMENTS").FormatWith(this.CommentsCount)%>' NavigateUrl='<%#this.TitleHyperLink%>'/>
            </div>
        </div>
    </div>
</div>
<br/>
<br/>