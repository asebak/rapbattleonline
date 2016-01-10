$(document).ready(function () {
    $.fn.editable.defaults.mode = 'inline';
    $('#usersbiography').editable({
        type: 'textarea',
        inputclass: 'fulltextarea',
        url: '/api/profile/createbio/',
        pk: parseInt(pageUserId),
        success: function (response, newValue) {
            if (response.status == 'error')
                return response.msg; //msg will be shown in editable form
            return response.msg;
        },
    });
});