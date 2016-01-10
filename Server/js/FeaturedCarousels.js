/*These functions get called only when loading the default page*/
$(function() {
    if ($('#featprofile').children('li').length <= 0) {
        $('#featprofile').hide();
    }

    $('#featprofile').jcarousel({
        //scroll: 1,
        //auto: 5,
        //wrap: 'circular'
    });
});

$(function() {
    if ($('#feattracks').children('li').length <= 0) {
        $('#feattracks').hide();
    }
    $('#feattracks').jcarousel({
        scroll: 1,
        wrap: 'circular'
    });
    $("#feattracks img").each(function() {
        $(this).css("height", "50px");
        $(this).css("width", "50px");
    });
    $("#feattracks span:last-child").each(function() {
        $(this).css("float", "right");
        $(this).css("margin-top", "-20px");

    });
    //$('div').filter(function () {
    //    return $(this).css('whatever') == 'the_value';
    //});
    //$("#feattracks div").each(function () {
    //    $(this).css("margin-right", "100px");
    //});
});

$(function() {
    if ($('#newWrittenBattles').children('li').length <= 0) {
        $('#newWrittenBattles').hide();
    }
    $('#newWrittenBattles').jcarousel({
        scroll: 1,
        auto: 5,
        wrap: 'circular'
    });
});

$(function() {
    if ($('#activeTournaments').children('li').length <= 0) {
        $('#activeTournaments').hide();
    }

    $('#activeTournaments').jcarousel({
        scroll: 1,
        auto: 5,
        wrap: 'circular'
    });
});

$(function() {
    if ($('#rndhoods').children('li').length <= 0) {
        $('#rndhoods').hide();
    }

    $('#rndhoods').jcarousel({
        scroll: 1,
        auto: 5,
        wrap: 'circular'
    });
});

$(function() {
    if ($('#rndprofiles').children('li').length <= 0) {
        $('#rndprofiles').hide();
    }
    $('#rndprofiles').jcarousel({
        scroll: 1,
        auto: 5,
        wrap: 'circular'
    });
});

$(function() {
    if ($('#voteprogressWrittenBattles').children('li').length <= 0) {
        $('#voteprogressWrittenBattles').hide();
    }
    $('#voteprogressWrittenBattles').jcarousel({
        scroll: 1,
        auto: 5,
        wrap: 'circular'
    });
});

$(function() {
    if ($('#voteProgressAudioBattles').children('li').length <= 0) {
        $('#voteProgressAudioBattles').hide();
    }
    $('#voteProgressAudioBattles').jcarousel({
        scroll: 1,
        auto: 5,
        wrap: 'circular'
    });
});

$(function() {
    if ($('#newAudioBattles').children('li').length <= 0) {
        $('#newAudioBattles').hide();
    }
    $('#newAudioBattles').jcarousel({
        scroll: 1,
        auto: 5,
        wrap: 'circular'
    });
});