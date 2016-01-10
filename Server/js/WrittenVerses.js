var aIds = [];
function createEditableVerse(id) {
    aIds.push(id);
}

$(document).ready(function () {
    $.fn.editable.defaults.mode = 'inline';
    for (var i = 0; i < aIds.length; i++) {
        $('#writtenverse' + aIds[i]).editable({
            type: 'textarea',
            inputclass: 'fulltextarea',
            url: '/api/verses/updatewrittenverse/',
            pk: parseInt(aIds[i]),
            success: function (response) {
                if (response.status == 'error')
                    return response.msg; //msg will be shown in editable form
                return response.msg;
            },
        });
    }
});