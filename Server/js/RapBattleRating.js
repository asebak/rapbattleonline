/*This function is called in creating a rapbattlebox user control*/

function createBattleRating(id, rating1Visible, rating2Visible, user1Overall, user2Overall) {
    if ($('#user1rating' + id).length > 0) {
        new sap.ui.commons.RatingIndicator({
            maxValue: 5,
            visualMode: sap.ui.commons.RatingIndicatorVisualMode.Continuous,
            value: parseFloat(user1Overall / 2),
            visible: rating1Visible == 'true',
            editable: false
        }).placeAt("user1rating" + id);
    }
    if ($('#user2rating' + id).length > 0) {
        new sap.ui.commons.RatingIndicator({
            maxValue: 5,
            visualMode: sap.ui.commons.RatingIndicatorVisualMode.Continuous,
            value: parseFloat(user2Overall / 2),
            visible: rating2Visible == 'true',
            editable: false
        }).placeAt("user2rating" + id);
    }
}

function createBattleRatingFull(user1Overall, user2Overall) {
    new sap.ui.commons.RatingIndicator({
        maxValue: 10,
        visualMode: sap.ui.commons.RatingIndicatorVisualMode.Continuous,
        value: parseFloat(user1Overall),
        editable: false,
        visible: user1Overall != ''
    }).placeAt("user1OverallRating");
    new sap.ui.commons.RatingIndicator({
        maxValue: 10,
        visualMode: sap.ui.commons.RatingIndicatorVisualMode.Continuous,
        value: parseFloat(user2Overall),
        editable: false,
        visible: user2Overall != ''
    }).placeAt("user2OverallRating");
}