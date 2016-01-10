/*Called when creating the display of votes repeater for rap battles*/
function CreateDisplayOfBattleVotes(user1wordplay, user1meta, user1flow, user1multis, user1punch, user2wordplay,
    user2meta, user2flow, user2multis, user2punch, divId) {
    var user1average = (parseInt(user1wordplay) + parseInt(user1meta) + parseInt(user1flow) + parseInt(user1multis) + parseInt(user1punch)) / 5;
    var user2average = (parseInt(user2wordplay) + parseInt(user2meta) + parseInt(user2flow) + parseInt(user2multis) + parseInt(user2punch)) / 5;
    //if ($('#user1rating' + divId).length > 0) {
        new sap.ui.commons.RatingIndicator({
            maxValue: 10,
            editable: false,
            value: user1average
        }).placeAt("user1rating" + divId);
    //}
    //if ($('#user2rating' + divId).length > 0) {
        new sap.ui.commons.RatingIndicator({
            maxValue: 10,
            editable: false,
            value: user2average
        }).placeAt("user2rating" + divId);
    //}
}