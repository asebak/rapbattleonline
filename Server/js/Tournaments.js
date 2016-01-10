//colors in the table that corresponds to the current user for better readability
function createUserviewTournament(pageUserName) {
    $('#tournamentTable tr').each(function () {
        var html = $(this).find(".team").html();
        var html2 = $(this).find(".winner").html();
        if ($.trim(html) == pageUserName) {
            $(this).find(".team").prepend($('<span></span>')
                .addClass('glyphicon glyphicon-star icon-success'));
        } else if ($.trim(html2) == pageUserName) {
            $(this).find(".winner").prepend($('<span></span>')
                .addClass('glyphicon glyphicon-star icon-success'));
        }
    });
}

//Enhances the visual appearence of the tournament bracket
function initializeTournamentBracketUI() {
    var header = $('#tournamentTable tr').first();
    header.addClass('info');
}