/*Script is used to vote on a generic(written/audio) rap battle*/
function battleWebService(battleType, battleId, userId1, userId2, user1Wordplay, user1Metaphores, user1Flow, user1Multis, user1Punchlines,
    user2Wordplay, user2Metaphores, user2Flow, user2Multis, user2Punchlines) {
    $.ajax({
        type: "PUT",
        url: "/api/RapBattleVote/Vote/" + battleId,
        data: JSON.stringify({
            BattleType: battleType,
            BattleId: battleId,
            UserId1: userId1,
            UserId2: userId2,
            User1Wordplay: user1Wordplay,
            User1Metaphores: user1Metaphores,
            User1Flow: user1Flow,
            User1Multis: user1Multis,
            User1Punchlines: user1Punchlines,
            User2Wordplay: user2Wordplay,
            User2Metaphores: user2Metaphores,
            User2Flow: user2Flow,
            User2Multis: user2Multis,
            User2Punchlines: user2Punchlines
        }),
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(retValue) {
        },
        error: function(x, e) {
            if (x.status == 0) {
                alertify.error('No Internet Connection!');
            } else if (x.status == 404) {
                alertify.error('URL Not Found');
            } else if (x.status == 550) {
                alertify.error(x.responseText);
            } else if (x.status == 500) {
                alertify.error('Internel Server Error.');
            } else if (e == 'parsererror') {
                alertify.error('Parsing JSON Request failed!');
            } else if (e == 'timeout') {
                alertify.error('Request Timed out!');
            } else {
                alertify.error('Error Occurred.\n' + x.responseText);
            }
        }
    });
}

function Value(object) {
    return object.getValue();
}
function CreateRatingForVoting(id) {
    if ($('#' + id).length > 0) {
        return new sap.ui.commons.RatingIndicator({ maxValue: 10, visualMode: sap.ui.commons.RatingIndicatorVisualMode.Continuous }).placeAt(id);
    }
    return null;
}
function CreateBattleVotingLayout(displayName1, battleId, displayName2, battleType, userId1, userId2) {
    var user1Wordplay = CreateRatingForVoting("wordplay1");
    var user1Metaphores = CreateRatingForVoting("metaphore1");
    var user1Flow = CreateRatingForVoting("flow1");
    var user1Multis = CreateRatingForVoting("multis1");
    var user1Punchlines = CreateRatingForVoting("punchlines1");
    var user2Wordplay = CreateRatingForVoting("wordplay2");
    var user2Metaphores = CreateRatingForVoting("metaphore2");
    var user2Flow = CreateRatingForVoting("flow2");
    var user2Multis = CreateRatingForVoting("multis2");
    var user2Punchlines = CreateRatingForVoting("punchlines2");
    if ($('#submitratinglayout').length > 0) {
        new sap.ui.layout.VerticalLayout({
            content: [
                new sap.ui.commons.Button({
                    text: submitbtntext,
                    press: function(oEvent) {
                        if (Value(user1Wordplay) == 0 || Value(user1Metaphores) == 0 || Value(user1Flow) == 0 ||
                            Value(user1Multis) == 0 || Value(user1Punchlines) == 0 ||
                            Value(user2Wordplay) == 0 || Value(user2Metaphores) == 0 ||
                            Value(user2Flow) == 0 || Value(user2Multis) == 0 || Value(user2Punchlines) == 0) {
                            alertify.error(votefail);
                        } else {
                            battleWebService(battleType, battleId, userId1, userId2, Value(user1Wordplay), Value(user1Metaphores),
                                Value(user1Flow), Value(user1Multis), Value(user1Punchlines), Value(user2Wordplay),
                                Value(user2Metaphores), Value(user2Flow), Value(user2Multis), Value(user2Punchlines));
                            oEvent.getSource().setEnabled(false);
                            alertify.success(votesuccessfull);
                        }
                    }
                }).addStyleClass("btn").addStyleClass("btn-default")
            ]
        }).placeAt("submitratinglayout");
    }
}