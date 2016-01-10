function createMusicRating(musicId, pageUser, ratingvalue, enabled) {
    ratingvalue = parseFloat(ratingvalue);
    var Rating = new sap.ui.commons.RatingIndicator({
        maxValue: 5,
        visualMode: sap.ui.commons.RatingIndicatorVisualMode.Continuous,
        editable: enabled,
        value: ratingvalue
    });
    Rating.attachChange(function() {
        var enteredvalue = Rating.getValue();
        Rating.setEditable(false);
        voteratingwebservice(musicId, pageUser, enteredvalue);
    });
    Rating.placeAt("RatingDIVGeneric" + musicId);
}