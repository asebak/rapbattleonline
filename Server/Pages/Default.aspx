<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FreestyleOnline.Pages.Default" %>

<%@ Register TagPrefix="RAP" TagName="NewsFeed" Src="~/Controls/NewsFeed.ascx" %>
<%@ Register TagPrefix="RAP" TagName="FeaturedProfiles" Src="~/Controls/Featured/FeaturedProfiles.ascx" %>
<%@ Register TagPrefix="RAP" TagName="FeaturedTracks" Src="~/Controls/Featured/FeaturedTracks.ascx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-md-7 column">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">News</h3>
                </div>
                <div class="panel-body">
                    <RAP:NewsFeed ID="NewsFeed1" runat="server" />
                </div>
            </div>
        </div>
        <div class="col-md-5 column">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Featured Members</h3>
                </div>
                <div id="panel-element-44965" class="panel-collapse in">
                    <div class="panel-body">
                        <RAP:FeaturedProfiles runat="server" />
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Featured Music</h3>
                </div>
                <div id="panel-element-44964" class="panel-collapse in">
                    <div class="panel-body">
                        <RAP:FeaturedTracks ID="FeaturedTracks1" runat="server" />
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Social Feed</h3>
                </div>
                <div id="panel-element-4496999" class="panel-collapse in">
                    <div class="panel-body">
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
