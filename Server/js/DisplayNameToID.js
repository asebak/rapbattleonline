/*Client script to get user ID from their name*/
function GetIDFromName(name) {
    var userId;
    userId = $.ajax({
        type: "GET",
        url: "/api/Profile/GetUserId/" + name,
        async: false,
        global: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).responseText;
    userId = JSON.parse(userId);
    return userId;
}