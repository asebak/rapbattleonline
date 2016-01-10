<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChatRoom.ascx.cs" Inherits="FreestyleOnline.Controls.RealTime.ChatRoom" %>
<script type="text/javascript" src="/signalr/hubs"></script>
<script type="text/javascript">
    var isGuest = "<%= this.PageContext.IsGuest.ToString().ToLower() %>" == "true";
    var displayName = "<%= this.PageContext.PageUserName %>";
    var avatar = "<%= this.Get<IAvatars>().GetAvatarUrlForCurrentUser() %>";
    var imageString = "<img class='loginUserImage'  src='<%= this.Get<IAvatars>().GetAvatarUrlForCurrentUser() %>' alt='picture'>";
    var noUsername = '<%= this.Text("CHAT", "NO_NAME") %>';
    var guestNoProfile = '<%= this.Text("CHAT", "NO_PROFILE") %>';
</script>
<div class="row clearfix">
    <div class="col-md-12 column">
        <div id="divLogin" class="form-group">
            <div class="form-group">
                <label for="txtNickName"><%= this.Text("COMMON", "COMMON_USER") %></label><input class="form-control" type="text" id="txtNickName" />
            </div>
            <input type="button" id="btnStartChat" class="btn btn-default" value='<%= this.Text("COMMON", "COMMON_SUBMIT") %>'/>
        </div>
    </div>
</div>
<div id="divChat" class="chatRoom">
    <div class="content">
        <div id="divChatWindow" class="chatWindow">
        </div>
        <div id="divusers" class="users">
        </div>
    </div>
    <div class="messageBar">
        <input class="form-control" type="text" id="txtMessage" />
        <input id="btnSendMsg" type="button" value='<%= this.Text("COMMON", "COMMON_SUBMIT") %>' class="btn btn-primary" />
    </div>
</div>
<input id="hdId" type="hidden" />
<input id="hdUserName" type="hidden" />