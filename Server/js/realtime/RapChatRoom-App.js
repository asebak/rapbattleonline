$(function () {
    sap.ui.core.BusyIndicator.show();
    $("#divContainer").hide();
    var chatHub = $.connection.rapChatRoom;
    registerClientMethods(chatHub);
    $.connection.hub.start().done(function () {
        registerEvents(chatHub);
        if (isGuest) {
            setScreen(false);
        } else {
            setScreen(true);
            //maybe needed its weird this program
            //$.connection.hub.stateChanged(function (state) {
                //if (state.oldState === $.signalR.connectionState.connecting && state.newState === $.signalR.connectionState.connected) {
                    chatHub.server.connect(displayName, false, imageString);
                //}
            //});
        }
        $("#divContainer").show();
        sap.ui.core.BusyIndicator.hide();
    });
});

function setScreen(isLogin) {

    if (!isLogin) {

        $("#divChat").hide();
        $("#divLogin").show();
    } else {

        $("#divChat").show();
        $("#divLogin").hide();
    }

}

function registerEvents(chatHub) {
    $("#btnStartChat").click(function () {
        var name = $("#txtNickName").val();
        if (name.length > 0) {
            chatHub.server.connect(name, true, imageString);
        } else {
            alertify.error(noUsername);
        }
    });

    $('#btnSendMsg').click(function () {
        var msg = $("#txtMessage").val();
        if (msg.length > 0) {
            var userName = $('#hdUserName').val();
            chatHub.server.sendMessageToAll(userName, msg);
            $("#txtMessage").val('');
        }
    });

    $("#txtNickName").keypress(function (e) {
        if (e.which == 13) {
            $("#btnStartChat").click();
        }
    });

    $("#txtMessage").keypress(function (e) {
        if (e.which == 13) {
            $('#btnSendMsg').click();
        }
    });
}

function registerClientMethods(chatHub) {

    // Calls when user successfully logged in
    chatHub.client.onConnected = function (id, userName, allUsers, messages) {
        setScreen(true);
        $('#hdId').val(id);
        $('#hdUserName').val(userName);
        $('#spanUser').html(userName);
        // Add All Users
        for (i = 0; i < allUsers.length; i++) {

            AddUser(chatHub, allUsers[i].ConnectionId, allUsers[i].UserName);
        }
        // Add Existing Messages
        for (i = 0; i < messages.length; i++) {

            AddMessage(messages[i].UserName, messages[i].Message);
        }
    };

    chatHub.client.onNewUserConnected = function (id, name) {
        AddUser(chatHub, id, name);
        var connected = $('<div class="connect">' + name + 'Entered</div>');
        $(connected).hide();
        $('#divusers').prepend(connected);
        $(connected).fadeIn(200).delay(2000).fadeOut(200);
    };

    chatHub.client.onUserDisconnected = function (id, userName) {
        $('#' + id).remove();
        var ctrId = 'private_' + id;
        $('#' + ctrId).remove();
        var disc = $('<div class="disconnect">' + userName + 'Left.</div>');
        $(disc).hide();
        $('#divusers').prepend(disc);
        $(disc).fadeIn(200).delay(2000).fadeOut(200);
    };

    chatHub.client.messageReceived = function (userName, message) {
        AddMessage(userName, message);
    };
}

function AddUser(chatHub, id, name) {
    var userconnectionId = $('#hdId').val();
    var code = "";
    if (userconnectionId == id) {
        code = $('<div class="loginUser">' + name + "</div>");
    } else {
        code = $('<a id="' + id + '" class="user" >' + name + '<a>');
        $(code).click(function () {
            var connectionid = $(this).attr('id');
            if (userconnectionId != connectionid) {
                var userIdofPage = GetIDFromName(name.substring(0, name.indexOf('<')));
                if (userIdofPage >= 1) {
                    window.open('/Pages/Profile/' + userIdofPage, 'popupwindow', 'width=1200,height=600,left=150,top=200,scrollbars=1,toolbar=no,status=1');
                } else {
                    alertify.error(guestNoProfile);
                }
            }
        });
    }
    $("#divusers").append(code);
}

function AddMessage(userName, message) {
    $('#divChatWindow').append('<div class="message"><span class="userName">' + userName + '</span>: ' + message + '</div>');
    var height = $('#divChatWindow')[0].scrollHeight;
    $('#divChatWindow').scrollTop(height);
}

function AddDivToContainer($div) {
    $('#divContainer').prepend($div);
    $div.draggable({
        handle: ".header",
        stop: function () {
        }
    });
}