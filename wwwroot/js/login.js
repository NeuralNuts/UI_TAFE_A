$("#login-button-id").click(function (event) {

    var email_input = $("#email-input").val()
    var password_input = $("#password-input").val()
    var response_good = "Authorized"
    var response_bad = '"Unauthorized"'
    var url = "https://localhost:7034/Home/HomePage"
    var error_message_incorect_details = $("#error-message-incorect-details")
    sessionStorage.setItem("email_input", email_input)
    $.ajax({
        type: "GET",
        url: "https://localhost:7034/api/User/AuthenticateUserLogin" + "?" +
            "email=" +
            email_input +
            "&password=" +
            password_input,
        dataType: "JSON",
        data: Request,
        success: function (data) {
            if (data == response_good) {
                error_message_incorect_details.hide()
                window.location.href = url
            }
        },
        error: function (data) {
            error_message_incorect_details.show()
        }
    })
});