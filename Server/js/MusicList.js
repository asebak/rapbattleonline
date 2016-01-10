function CreateRating(musicId, userIDofSong, userIDonPage) {
    var enabledwebservice;
    var ratingValuewebservice;
    var totalvoteswebservice;
    var trackRating = new sap.ui.commons.RatingIndicator("musicRating" + musicId,
    {
        maxValue: 5,
        visualMode: sap.ui.commons.RatingIndicatorVisualMode.Continuous
    });
    var queryString = "MusicId=" + musicId + "&UserId=" + userIDofSong + "&PageUserId=" + userIDonPage;
    enabledwebservice = $.ajax({
        type: "GET",
        url: "/api/MusicVote/IsRatingEnabled?" + queryString,
        data: JSON.stringify({ MusicId: musicId, UserId: userIDofSong, PageUserId: userIDonPage }),
        async: false,
        global: false,
        traditional: true,
    }).responseText;

    ratingValuewebservice = $.ajax({
        type: "GET",
        url: "/api/MusicVote/GetRatingValue/" + musicId,
        async: false,
        global: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).responseText;

    totalvoteswebservice = $.ajax({
        type: "GET",
        url: "/api/MusicVote/GetTotalVotes/" + musicId,
        async: false,
        global: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).responseText;

    enabledwebservice = JSON.parse(enabledwebservice);
    ratingValuewebservice = JSON.parse(ratingValuewebservice);
    totalvoteswebservice = JSON.parse(totalvoteswebservice);
    trackRating.setEditable(enabledwebservice);
    trackRating.setValue(ratingValuewebservice);
    trackRating.attachChange(function() {
        var enteredvalue = trackRating.getValue();
        trackRating.setEditable(false);
        voteratingwebservice(musicId, userIDonPage, enteredvalue);
        sap.ui.commons.MessageBox.alert("Voted!");
        window.location.reload();
    });
    trackRating.placeAt("RatingDIV" + musicId);
    var totalRatings = new sap.ui.commons.TextView("TotalRatingsDIV" + musicId);
    totalRatings.setText("(" + totalvoteswebservice + " Votes)");
    totalRatings.placeAt("RatingDIV" + musicId);
}

function voteratingwebservice(musicId, userId, value) {
    $.ajax({
        type: "PUT",
        url: "/api/MusicVote/SetRatingValue/" + musicId,
        data: JSON.stringify({ MusicId: musicId, PageUserId: userId, RatingValue: value }),
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(retValue) {
            alertify.success("Thanks for voting.");
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