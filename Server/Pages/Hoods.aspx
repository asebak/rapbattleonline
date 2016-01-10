<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Hoods.aspx.cs" Inherits="FreestyleOnline.Pages.Hoods" %>

<%@ Register TagPrefix="RAP" TagName="HoodBox" Src="~/Controls/HoodBox.ascx" %>
<%@ Register TagPrefix="RAP" TagName="Pager" Src="~/Controls/Generic/Pager.ascx" %>
<%@ Register TagPrefix="RAP" TagName="PostEditor" Src="~/Controls/Generic/PostEditor.ascx" %>
<%@ Register TagPrefix="RAP" TagName="DisplaySitePost" Src="~/Controls/Generic/DisplaySitePost.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row clearfix">
        <div class="col-md-12 column">
            <div class="tabbable" id="tabs-337510">
                <ul class="nav nav-tabs">
                    <li class="active">
                        <a href="#panel-270214" data-toggle="tab"><%=this.Text("PROFILE", "OVERVIEW")%></a>
                    </li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="panel-270214">
                            <RAP:HoodBox ID="CurrentHoodBox" runat="server" />
                            <asp:Repeater ID="HoodCommentsRepeater" runat="server" OnItemDataBound="HoodCommentsRepeater_ItemDataBound">
                                <ItemTemplate>
                                    <RAP:DisplaySitePost ID="PostEditor" UserId='<%#(int)Eval("UserID") %>'
                                        PostInformation='<%#Server.HtmlDecode(Server.HtmlEncode(Eval("Comment").ToString())) %>'
                                        DatePosted='<%#Eval("DatePosted") %>' PostId='<%#Eval("HoodCommentsID") %>' MessageId='<%#++this.PostNumber%>' runat="server" />
                                </ItemTemplate>
                            </asp:Repeater>
                            <RAP:PostEditor runat="server" ID="PostEdit" />
                            <RAP:Pager ID="HoodPager" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
