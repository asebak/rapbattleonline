<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeaturedTracks.ascx.cs" Inherits="FreestyleOnline.Controls.Featured.FeaturedTracks" %>
<%@ Register TagPrefix="RAP" TagName="MusicTrackBox" Src="~/Controls/MusicTrackBox.ascx" %>
<RAP:MusicTrackBox ID="MusicFeatured" runat="server" />
<asp:PlaceHolder ID="NoFeaturedMusic" runat="server" />
