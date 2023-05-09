var session_id = sessionStorage.getItem("data");

if (session_id == null) {
    sessionStorage.removeItem("data");
    window.location.href = "https://templeos.org/";
}

var email_input = $("#email-input")
var theme_input = $("#theme-input")
var session_email = sessionStorage.getItem("email_input")
var session_theme = sessionStorage.getItem("theme_input")
var themeSelect = document.getElementById("theme-select")
var reader = new FileReader()

theme_input.val(session_theme)
email_input.val(session_email)

$("#logout-btn").click(function (event) {
    window.location = "https://localhost:7034/"
    localStorage.clear();
    sessionStorage.clear();
})

$("#save-button").click(function (event) {

    $.ajax({
        type: "PUT",
        url: "https://localhost:7034/api/User/PutUserTheme?" + "email=" +
            email_input.val() +
            "&theme=" +
            themeSelect.options[themeSelect.selectedIndex].text,

        dataType: "JSON",
        success: function (data) {

            console.log(data)
        },
        error: function (data) {

            console.log(data)
        }
    })
    sessionStorage.setItem("theme_input", themeSelect.options[themeSelect.selectedIndex].text)
    window.location.reload()
})

$.ajax({
    type: "GET",
    url: "https://localhost:7034/api/User/GetSingleUser?" + "email=" +
        session_email,

    dataType: "JSON",
    success: function (data) {

        for (var i = 0; i < data.length; i++) {

            theme_input.val(data[i].userTheme)
        }

        console.log(data)
    }
})

$("#save-image-button").click(function (event) {

    $.ajax({
        type: "PUT",
        url: "https://localhost:7034/api/User/PutUserImage?" + "email=" +
            email_input.val() +
            "&image=" +
            reader.result,

        dataType: "JSON",
        success: function (data) {

            console.log(data)
        },
        error: function (data) {

            console.log(data)
        }
    })

    //window.location.reload()
    sessionStorage.setItem("theme_input", theme_input.val())
})