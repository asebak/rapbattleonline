<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="FreestyleOnline.Pages.Admin" %>
<%@ Register TagPrefix="RAP" TagName="NewsManager" Src="~/Controls/Admin/NewsManager.ascx" %>
<%@ Register TagPrefix="RAP" TagName="ReportManager" Src="~/Controls/Admin/ReportManager.ascx" %>
<%@ Register TagPrefix="RAP" TagName="ContactManager" Src="~/Controls/Admin/ContactManager.ascx" %>
<%@ Register TagPrefix="RAP" TagName="FeaturedManager" Src="~/Controls/Admin/FeaturedManager.ascx" %>
<%@ Register TagPrefix="RAP" TagName="GlobalMessagesManager" Src="~/Controls/Admin/GlobalMessagesManager.ascx" %>
<%@ Register TagPrefix="RAP" TagName="TestDataGenerator" Src="~/Controls/Admin/TestDataGenerator.ascx" %>
<%@ Register TagPrefix="RAP" TagName="ExceptionsList" Src="~/Controls/Admin/ExceptionList.ascx" %>
<%@ Register TagPrefix="RAP" TagName="TournamentManager" Src="~/Controls/Admin/TournamentManager.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row clearfix">
        <div class="col-md-12 column">
            <div class="col-md-2">
                <div class="sidebar-nav">
                    <div class="navbar navbar-default" role="navigation">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".sidebar-navbar-collapse">
                                <span class="sr-only">Toggle navigation</span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                            </button>
                            <span class="visible-xs navbar-brand"><%=this.Text("ADMIN", "HEADER")%></span>
                        </div>
                        <div class="navbar-collapse collapse sidebar-navbar-collapse">
                            <ul class="nav nav-tabs">
                                <li class="active">
                                    <a href="#panel-28010" data-toggle="tab"><%= this.Text("ADMIN", "NEWSMANAGER") %></a>
                                </li>
                                <li >
                                    <a href="#panel-840055" data-toggle="tab"><%= this.Text("ADMIN", "REPORTMANAGER") %></a>
                                </li>
                                <li>
                                    <a href="#panel-280101" data-toggle="tab"><%= this.Text("ADMIN", "CONTACTMANAGER") %></a>
                                </li>
                                <li >
                                    <a href="#panel-8400552" data-toggle="tab"><%= this.Text("ADMIN", "FEATUREDMANAGER") %></a>
                                </li>
                                <li>
                                    <a href="#panel-280103" data-toggle="tab"><%= this.Text("ADMIN", "RTMESSAGE") %></a>
                                </li>
                                <li >
                                    <a href="#panel-8400554" data-toggle="tab"><%= this.Text("ADMIN", "DATAGENERATOR") %></a>
                                </li>
                                <li>
                                    <a href="#panel-280105" data-toggle="tab"><%= this.Text("ADMIN", "EXCEPTIONS") %></a>
                                </li>
                                <li >
                                    <a href="#panel-8400556" data-toggle="tab"><%= this.Text("ADMIN", "TOURNAMENTS") %></a>
                                </li>
                                <li >
                                    <a href="#panel-12345" data-toggle="tab"><%= this.Text("ADMIN", "ADMIN_WRITTEN") %></a>
                                </li>
                                <li >
                                    <a href="#panel-12346" data-toggle="tab"><%= this.Text("ADMIN", "ADMIN_AUDIO") %></a>
                                </li>
                               <li >
                                    <a href="#panel-12346" data-toggle="tab"><%= this.Text("ADMIN", "HOODS") %></a>
                                </li>
                            </ul>
                        </div><!--/.nav-collapse -->
                    </div>
                </div>
            </div>
            <div class="col-md-6">              
                    <div class="tab-content">
                        <div class="tab-pane active" id="panel-28010">
                            <RAP:NewsManager runat="server" />
                        </div>
                        <div class="tab-pane" id="panel-840055">
                            <RAP:ReportManager runat="server" />
                        </div>
                        <div class="tab-pane" id="panel-280101">
                            <RAP:ContactManager runat="server" />
                        </div>
                        <div class="tab-pane" id="panel-8400552">
                            <RAP:FeaturedManager runat="server" />
                        </div>
                        <div class="tab-pane" id="panel-280103">
                            <RAP:GlobalMessagesManager runat="server" />
                        </div>
                        <div class="tab-pane" id="panel-8400554">
                            <RAP:TestDataGenerator runat="server" />
                        </div>
                        <div class="tab-pane" id="panel-280105">
                            <RAP:ExceptionsList runat="server" />
                        </div>
                        <div class="tab-pane" id="panel-8400556">
                            <RAP:TournamentManager runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>