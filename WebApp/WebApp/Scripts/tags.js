$(document).ready(function () {
    $('#tokenfield').tokenfield({
        autocomplete: {
            source: function (request, response) {
                $.ajax({
                    type: 'GET',
                    contentType: "application/json; charset=utf-8",
                    url: "/Song/GetTag",
                    dataType: 'json',
                    data: {
                        term: request.term
                    },
                    success: function (data) {
                        response(data.data);
                    },
                    error: function (result) {
                        alert("Wrong")
                    }
                });
            },
            delay: 100
        },
        showAutocompleteOnFocus: true
    });
    $('#tokenfield').on('tokenfield:createtoken', function (event) {
        var existingTokens = $(this).tokenfield('getTokens');
        $.each(existingTokens, function (index, token) {
            if (token.value === event.attrs.value)
                event.preventDefault();
        });
    });
    //$("#submittoken").click(function () {
    //    var sch = parseInt($('#Suite_hordId').val());
    //    var text = $('#Text').val();
    //    var token = $('#tokenfield').val();
    //    console.log(sch);
    //    console.log(text);
    //    console.log(token);
    //    $.ajax({
    //        type: 'Post',
    //        contentType: "application/json; charset=utf-8",
    //        url: "/Song/UpdateSong",
    //        dataType: 'json',
    //        data: {
    //            id: sch
    //        },
    //        success: function (data) {
    //            response(data.data);
    //        },
    //        error: function (result) {
    //            alert("Wrong")
    //        }
    //    });
    //})
});