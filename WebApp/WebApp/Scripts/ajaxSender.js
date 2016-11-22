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
    var div;
    if (document.getElementById("Next") !== null) {
        div = document.getElementById("Next");
        if (currentPage + 1 === lastPage) {
            div.style.display = "none";
        }
        else {
            div.style.display = "inline";
        }
    }
    if (document.getElementById("Previous")!== null){
         div = document.getElementById("Previous");
        if (currentPage === 0) {
            div.style.display = "none";
        } else {
            div.style.display = "inline";
        }
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
};

//$('#tokenfield').tokenfield({
//    autocomplete: {
//        source: ['A', 'B'],
//        delay: 100
//    },
//    showAutocompleteOnFocus: true
//})
