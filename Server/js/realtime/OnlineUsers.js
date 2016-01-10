$(function () {
    var totalUsers = $.connection.onlineUsers;
    $.connection.hub.start().done(function () {
        $('#HeadLoginStatus').click(function (event) {
            totalUsers.connection.stop();
            $.ajax({
                url: "/api/Logout/",
                type: "POST"
            }).done(function() {
                totalUsers.connection.start();
            });
        });
    });
    totalUsers.client.totalOnlineUsers = function (usercount) {
        $('#TotalOnlineUsers').text('Real Time Online Users: ' + usercount);
    };
});