$(document).ready(function () {
    $("#Previous").click(function () {
        if (Calculate("Previous"))
            $('form').submit();
    })
});
$(document).ready(function () {
    $("#Next").click(function () {
        if (Calculate("Next")) {
            $('form').submit();
        }
    })
});
$(document).ready(function () {
    var currentPage = parseInt($("#CurrentPage").val());
    var lastPage = parseInt($("#LastPage").val());
    if (currentPage + 1 === lastPage) {
        var div = document.getElementById("Next");
        div.style.display = "none";
    }
    else {
        var div = document.getElementById("Next");
        div.style.display = "inline";
    }
    if (currentPage === 0) {
        var div = document.getElementById("Previous");
        div.style.display = "none";
    } else {
        var div = document.getElementById("Previous");
        div.style.display = "inline";
    }
});
function Calculate(moving) {
    var currentPage = parseInt($("#CurrentPage").val());
    var lastPage = parseInt($("#LastPage").val());
    if (moving === "Next") {
        currentPage++;
    }
    else if (moving === "Previous") {
        currentPage--
    }
    else
        alert("Wrong");
    $("#CurrentPage").val(currentPage);
    if (currentPage + 1 === lastPage) {
        var div = document.getElementById("Next");
        div.style.display = "none";
    }
    else {
        var div = document.getElementById("Next");
        div.style.display = "inline";
    }
    if (currentPage === 0) {
        var div = document.getElementById("Previous");
        div.style.display = "none";
    } else {
        var div = document.getElementById("Previous");
        div.style.display = "inline";
    }
    return true;
}
function first() {
    var currentState = location.href.split("=")[0];
    var urlPath = currentState + "=" + parseInt($("#NextId").val());
    window.history.pushState("", "", urlPath);
    return true;
    //history.replaceState({ "html": response.html, "pageTitle": response.pageTitle }, "", "bar2.html");
};

show.visible = '1';
show.hidden = '2';

function show() {
    show.hidden = show.visible;
    show.visible = (show.visible === '1') ? '2' : '1';
    document.getElementById(show.visible).style.display = 'block';
    document.getElementById(show.hidden).style.display = 'none';
}
var engine = new Bloodhound({
    local: [{ value: 'red' }, { value: 'blue' }, { value: 'green' }, { value: 'yellow' }, { value: 'violet' }, { value: 'brown' }, { value: 'purple' }, { value: 'black' }, { value: 'white' }],
    datumTokenizer: function (d) {
        return Bloodhound.tokenizers.whitespace(d.value);
    },
    queryTokenizer: Bloodhound.tokenizers.whitespace
});

engine.initialize();

$('#tokenfield-typeahead').tokenfield({
    typeahead: [null, { source: engine.ttAdapter() }]
});
//$(document).ready(function () {
//        var token = [];
//        var engine;
//        $.get("Song/GetTag", function (response) {
//            $.each(response.data, function (i, v) {
//                token.push({ value: v });
//                console.log(v);
//            });
//            engine = new Bloodhound({
//                local: token,
//                datumTokenizer: function (d) {
//                    return Bloodhound.tokenizers.whitespace(d.value);
//                },
//                queryTokenizer: Bloodhound.tokenizers.whitespace

//            });
//            engine.initialize();
//            console.log(token);
//            $('#tokenfield').on('tokenfield:createtoken', function (e) {
//                var data = e.attrs.value.split('|')
//                e.attrs.value = data[1] || data[0]
//                e.attrs.label = data[1] ? data[0] + ' (' + data[1] + ')' : data[0]
//            }).on('tokenfield:createdtoken', function (e) {
//                var re = /\S+\S+\.\S+/
//                var valid = re.test(e.attrs.value)
//                if (!valid) {
//                    $(e.relatedTarget).addClass('invalid')
//                }
//            }).on('tokenfield:edittoken', function (e) {
//                if (e.attrs.label !== e.attrs.value) {
//                    var label = e.attrs.label.split(' (')
//                    e.attrs.value = label[0] + '|' + e.attrs.value
//                }
//            }).on('tokenfield:removedtoken', function (e) {
//            }).tokenfield({
//                typeahead: [null, { source: engine.ttAdapter() }]
//            });
//        });
//    });