<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeaturedProfiles.ascx.cs" Inherits="FreestyleOnline.Controls.Featured.FeaturedProfiles" %>
<%@ Register TagPrefix="RAP" TagName="ProfileLink" Src="~/Controls/ProfileLink.ascx" %>
<div class="row clearfix">
    <div class="col-md-3 column">
        <RAP:ProfileLink runat="server" ID="FeaturedUsers"/>
    </div>
    <div class="col-md-8 column">
        <ext:Label Cls="formattedLabel" runat="server" ID="UsersBio" />
    </div>
</div>
<asp:PlaceHolder ID="NoFeaturedUsers" runat="server"/>