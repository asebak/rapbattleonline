<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cypher.ascx.cs" Inherits="FreestyleOnline.Controls.RealTime.Cypher" %>
<link href="/Resources/icomoon/icomoon.css" rel="stylesheet" />
<link href="/Resources/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
<link href="/Styles/rapcypher.min.css" rel="stylesheet" />
<link href="/Styles/alertify.core.css" rel="stylesheet" />
<link href="/Styles/alertify.default.css" rel="stylesheet" />

<body data-bind="attr: { 'data-mode': Mode }">
    <div class="container-fluid">
        <div class="row-fluid instructions">
            <div class="span12">
                <div class="alert alert-error">Look up at the top of the address bar to allow access to your microphone.</div>
            </div>
        </div>
        <div class="row-fluid browser-warning">
            <div class="span12">
                <div class="alert alert-error">Your browser does not appear to support WebRTC. Try using FireFox or Chrome</div>
            </div>
        </div>
    </div>
    <div class="navbar-fixed-top">
        <div class="navbar-inner">
            <div class="container-fluid">
                <span class="loading-indicator icon-spinner-3" data-bind="css: { on: Loading }"></span>
                <div class="nav-collapse collapse">
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div class="row-fluid">
            <!-- Side Bar -->
            <div class="span3">
                <!-- In Call Actions -->
                <div class="well actions">
                    <div class="status" data-bind="text: CallStatus"></div>
                    <button class="btn btn-danger hangup">Hang Up</button>
                </div>
                <!--Online User List -->
                <div class="well user-list">
                    <ul class="nav nav-list">
                        <li class="nav-header">Online Users <small data-bind="text: Users().length"></small></li>
                        <!-- ko foreach: Users -->
                        <li class="user" data-bind="attr: { 'data-cid': ConnectionId, 'title': UserName }">
                            <a href="#">
                                <div class="username" data-bind="text: UserName"></div>
                                <div class="helper" data-bind="css: $parent.getUserStatus($data)"></div>
                            </a>
                        </li>
                        <!-- /ko -->
                    </ul>
                </div>
                <!--Cypher 1 -->
                <div class="well user-list">
                    <ul class="nav nav-list">
                        <li class="nav-header">Cypher 1<button style="float: right;" class="cypher1" data-bind="text: Cypher1Text, css: Cypher1Css"></button></li>
                        <!-- ko foreach: Cypher1Users -->
                        <li class="user" data-bind="attr: { 'data-cid': ConnectionId, 'title': UserName }">
                            <a href="#">
                                <div class="username" data-bind="text: UserName"></div>
                                <div class="helper" data-bind="css: $parent.getUserStatus($data)"></div>
                            </a>
                        </li>
                        <!-- /ko -->
                    </ul>
                </div>
                <!--Cypher 2 -->
                <div class="well user-list">
                    <ul class="nav nav-list">
                        <li class="nav-header">Cypher 2<button style="float: right;" class="btn btn-success cypher2" data-bind="text: Cypher2Text, css: Cypher2Css"></button></li>
                        <!-- ko foreach: Cypher2Users -->
                        <li class="user" data-bind="attr: { 'data-cid': ConnectionId, 'title': UserName }">
                            <a href="#">
                                <div class="username" data-bind="text: UserName"></div>
                                <div class="helper" data-bind="css: $parent.getUserStatus($data)"></div>
                            </a>
                        </li>
                        <!-- /ko -->
                    </ul>
                </div>
                <!--Cypher 3 -->
                <div class="well user-list">
                    <ul class="nav nav-list">
                        <li class="nav-header">Cypher 3<button style="float: right;" class="btn btn-success cypher3" data-bind="text: Cypher3Text, css: Cypher3Css"></button></li>
                        <!-- ko foreach: Cypher3Users -->
                        <li class="user" data-bind="attr: { 'data-cid': ConnectionId, 'title': UserName }">
                            <a href="#">
                                <div class="username" data-bind="text: UserName"></div>
                                <div class="helper" data-bind="css: $parent.getUserStatus($data)"></div>
                            </a>
                        </li>
                        <!-- /ko -->
                    </ul>
                </div>
                <!--Cypher 4 -->
                <div class="well user-list">
                    <ul class="nav nav-list">
                        <li class="nav-header">Cypher 4<button style="float: right;" class="btn btn-success cypher4" data-bind="text: Cypher4Text, css: Cypher4Css"></button></li>
                        <!-- ko foreach: Cypher4Users -->
                        <li class="user" data-bind="attr: { 'data-cid': ConnectionId, 'title': UserName }">
                            <a href="#">
                                <div class="username" data-bind="text: UserName"></div>
                                <div class="helper" data-bind="css: $parent.getUserStatus($data)"></div>
                            </a>
                        </li>
                        <!-- /ko -->
                    </ul>
                </div>
                <!--Cypher 5 -->
                <div class="well user-list">
                    <ul class="nav nav-list">
                        <li class="nav-header">Cypher 5<button style="float: right;" class="btn btn-success cypher5" data-bind="text: Cypher5Text, css: Cypher5Css"></button></li>
                        <!-- ko foreach: Cypher5Users -->
                        <li class="user" data-bind="attr: { 'data-cid': ConnectionId, 'title': UserName }">
                            <a href="#">
                                <div class="username" data-bind="text: UserName"></div>
                                <div class="helper" data-bind="css: $parent.getUserStatus($data)"></div>
                            </a>
                        </li>
                        <!-- /ko -->
                    </ul>
                </div>
                <!-- Videos -->
                <div class="span9">
                    <div class="row-fluid">
                        <div class="span6">
                            <h4>You</h4>
                            <video class="video mine cool-background" autoplay="autoplay"></video>
                        </div>
                        <div class="span6">
                            <h4>Partner</h4>
                        
                            <video class="video partner cool-background" autoplay="autoplay"></video>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript" src="/js/Scripts/jquery-1.8.2.min.js"></script>
        <script type="text/javascript" src="/js/Scripts/knockout-3.0.0.js"></script>
        <script type="text/javascript" src="/Resources/bootstrap/js/bootstrap.min.js"></script>
        <script type="text/javascript" src="/js/Scripts/knockout.mapping-latest.js"></script>
        <script type="text/javascript" src="/js/Scripts/alertify.min.js"></script>
        <script type="text/javascript" src="/js/Scripts/jquery.signalR-1.0.0.js"></script>
        <script type="text/javascript" src="/js/Scripts/adapter.js"></script>
        <script type="text/javascript" src="/signalr/hubs"></script>
        <script type="text/javascript" src="/js/realtime/RapCypher-KnockOut.js"></script>
        <script type="text/javascript" src="/js/realtime/RapCypher-ConnectionManager.js"></script>
        <script type="text/javascript" src="/js/realtime/RapCypher-App.js"></script>
        <script type="text/javascript">
            RapCypher.App.start("<%= this.PageContext.PageUserName %>");
        </script></div>
</body>