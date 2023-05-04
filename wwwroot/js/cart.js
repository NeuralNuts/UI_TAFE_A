var cart_array = []
var item_array = []
var list_array = []

var listSelectCart = document.getElementById("lists-2-select")

async function UpdateCartTable() {
    var list_array_2 = []
    var result = await fetch("https://localhost:7034/CartTable")
    var htmlResult = await result.text()
    var listSelectCart = document.getElementById("lists-2-select")

    document.getElementById("render-cart").innerHTML = htmlResult;

    //var div = document.getElementById("parent-cart-id");

    //div.addEventListener('load', (e) => {
    //    e.preventDefault();
    //    console.log(e)
    //})

    $.ajax({
        type: "GET",
        url: "https://localhost:7034/api/Lists/GetUserLists?" + "email=" + sessionStorage.getItem("email_input"),
        dataType: "JSON",
        beforeSend: function () {

        },
        success: function (response) {
            list_array_2 = response
            buildUserSelect2(list_array)
            console.log()

            $.ajax({
                type: "GET",
                url: "https://localhost:7034/api/Cart/GetUserCartItems?" +
                    "email=" +
                    sessionStorage.getItem("email_input") +
                    "&list_name=" +
                    listSelectCart.options[listSelectCart.selectedIndex].text,

                dataType: "JSON",
                beforeSend: function () {
                    $('#spinner-loader-id').removeClass('hidden')
                },
                success: function (data) {

                    cart_array = data
                    buildCartDivs(cart_array)
                    console.log(data)

                    var cart_array = data;
                    var total = 0;
                    for (var i = 0; i < cart_array.length; i++) {
                        total += cart_array[i].itemPrice;
                    }
                    console.log('Cart Total:', total);
                    $("#total-price-id").val(`$${total}`)
                },
                complete: function () {
                    $('#spinner-loader-id').addClass('hidden')
                },
            })
        },
        complete: function () {

        }
    })
}

$.ajax({
    type: "GET",
    url: "https://localhost:7034/api/Lists/GetUserLists?" + "email=" + sessionStorage.getItem("email_input"),
    dataType: "JSON",
    beforeSend: function () {

    },
    success: function (response) {
        list_array = response
        buildUserSelect2(list_array)
        console.log(product_array)

        $.ajax({
            type: "GET",
            url: "https://localhost:7034/api/Cart/GetUserCartItems?" +
                "email=" +
                sessionStorage.getItem("email_input") +
                "&list_name=" +
                listSelectCart.options[listSelectCart.selectedIndex].text,

            dataType: "JSON",
            beforeSend: function () {
                $('#spinner-loader-id').removeClass('hidden')
            },
            success: function (data) {

                cart_array = data
                buildCartDivs(cart_array)
                console.log(data)

                var cart_array = data;
                var total = 0;
                for (var i = 0; i < cart_array.length; i++) {
                    total += cart_array[i].itemPrice;
                }
                console.log('Cart Total:', total);
                $("#total-price-id").val(`$${total}`)
            },
            complete: function () {
                $('#spinner-loader-id').addClass('hidden')
            },
        })
    },
    complete: function () {

    }
})

function buildUserSelect2(data) {
    var table = document.getElementById('lists-2-select')
    for (var i = 0; i < data.length; i++) {
        var row = `<option>${data[i].listName}</option>`
        table.innerHTML += row
    }
}

function buildCartDivs(data) {

    let body = document.getElementById('cart-id')
    for (var i = 0; i < data.length; i++) {

        var div = `<tr class="tr-data">
                                   <td id="item-id">${data[i].itemName}</td>
                                   <td id="qty-input">${data[i].itemQty}</td>
                                   <td class="price-td" id="price-id">${data[i].itemPrice}$</td>
                                   <td><button class="edit-button">${data[i].id}</button></td>
                                   <td><button class="delete-button">${data[i].id}</button></td>
                               </tr>`

        body.innerHTML += div

        var id = body.getElementsByClassName('delete-button')

        for (var i = 0, len = id.length; i < len; i++) {
            for (var j = 0, len = id.length; j < len; j++) {

                id[j].onclick = function () {

                    $.ajax({
                        type: "DELETE",
                        url: "https://localhost:7034/api/Cart/DeleteItem?" +
                            "id=" +
                            this.innerHTML,

                        dataType: "JSON",
                        success: function (data) {


                            console.log(data)
                            UpdateCartTable();
                        }
                    })
                    UpdateCartTable();
                }
            }
        };

        var edit_button = body.getElementsByClassName('edit-button')
        var input = document.getElementById('edit-id')

        for (var g = 0, len = edit_button.length; g < len; g++) {

            edit_button[g].onclick = function () {

                input.value = this.innerHTML
                $("#update-button").click(function (event) {

                    var input = document.getElementById('edit-id')

                    $.ajax({
                        type: "PUT",
                        url: "https://localhost:7034/api/Cart/PutItemQty?" + "id=" +
                            input.value +
                            "&qty=" +
                            $("#qty-input-id").val(),

                        dataType: "JSON",
                        beforeSend: function () {
                            $('#spinner-loader-id').removeClass('hidden')
                        },
                        success: function (data) {

                            console.log(data)
                        },
                        complete: function () {
                            $('#spinner-loader-id').addClass('hidden')
                        },
                    })
                    UpdateCartTable();
                })
            }
        }
    }
}