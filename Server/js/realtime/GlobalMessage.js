$(function () {
    var globalNotify = $.connection.globalNotifications;

    $.connection.hub.start().done(function () {
        $('#notifyAll').click(function () {
            globalNotify.server.notify($('#contentTitle').val(), $('#contentMessage').val());
        });
        globalNotify.server.connect(pageId);
        globalNotify.client.notifyGlobal = function (title, msg) {
            var dialogmsg = new sap.ui.commons.Dialog();
            dialogmsg.setTitle(title);
            dialogmsg.addContent(new sap.ui.commons.TextView({ text: msg }));
            dialogmsg.addButton(new sap.ui.commons.Button({ text: "OK", press: function () { dialogmsg.close(); } }));
            dialogmsg.open();
        };
    });
});