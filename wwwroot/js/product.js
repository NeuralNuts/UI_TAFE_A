var session_id = sessionStorage.getItem("data");

//if (session_id == null) {
//    sessionStorage.removeItem("data");
//    window.location.href = '@Url.Action("LoginPage", "Home")';
//}

const searchInput = document.getElementById("search-input")
var product_array = []
var list_array = []
var productSelect = document.getElementById("lists-select")
var nameInput = document.getElementById("name-input")

$.ajax({
    type: "GET",
    url: "https://localhost:7034/api/Item/GetAllItems",
    dataType: "JSON",
    beforeSend: function () {

    },
    success: function (response) {
        product_array = response
        buildProductDivs(product_array)
        console.log(product_array)
    },
    complete: function () {

    }
})

$.ajax({
    type: "GET",
    url: "https://localhost:7034/api/Lists/GetUserLists?" + "email=" + sessionStorage.getItem("email_input"),
    dataType: "JSON",
    beforeSend: function () {

    },
    success: function (response) {
        list_array = response
        buildUserSelect(list_array)
        console.log(product_array)
    },
    complete: function () {

    }
})

$("#save-list-btn").click(function (event) {
    $.ajax({
        type: "POST",
        url: "https://localhost:7034/api/Lists/PostList?" + "UserEmail=" + sessionStorage.getItem("email_input") + "&ListName=" + nameInput.value,
        dataType: "JSON",
        beforeSend: function () {

        },
        success: function (response) {
            list_array = response
            buildUserSelect(list_array)
            console.log(product_array)
        },
        complete: function () {

        }
    })
    location.reload()
})

function buildUserSelect(data) {
    var table = document.getElementById('lists-select')
    for (var i = 0; i < data.length; i++) {
        var row = `<option>${data[i].listName}</option>`
        table.innerHTML += row
    }
}

function showCorrectToast() {
    var x = document.getElementById("snackbar");
    x.className = "show";

    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
}

function searchTable() {
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("search-input");
    filter = input.value.toUpperCase();
    table = document.getElementById("my-table");
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        td_2 = tr[i].getElementsByTagName("td")[2];

        if (td) {
            txtValue = td.textContent || td.innerText || td.innerHTML || td_2.textContent || td_2.innerText || td_2.innerHTML
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            }
            else if (td_2) {
                txtValue_2 = td_2.textContent || td_2.innerText || td_2.innerHTML
                if (txtValue_2.toUpperCase().indexOf(filter) > -1) {
                    tr[i].style.display = "";
                } else {

                    var a = tr[i].style.display = "none"
                }
            }
            else {
                tr[i].style.display = "none";
            }
        }
    }
}

function buildProductDivs(data) {
    let body = document.getElementById('my-table')
    $('#loader').removeClass('hidden')

    for (var i = 0; i < data.length; i++) {
        var div = `<tr class="tr-data">
                                   <td>${data[i].itemName}</td>
                                   <td>${data[i].itemDescription}</td>
                                   <td>${data[i].itemUnitSize}</td>
                                   <td class="price-td">$${data[i].itemPrice}</td>
                                   <td><button class="add-button" id="add-to-list">${data[i].id}</button></td>
                               </tr>`
        body.innerHTML += div
        $('#loader').addClass('hidden')

        var id = body.getElementsByClassName('add-button')

        for (var i = 0, len = id.length; i < len; i++) {
            for (var j = 0, len = id.length; j < len; j++) {

                id[j].onclick = function () {

                    $.ajax({
                        type: "GET",
                        url: "https://localhost:7034/api/Item/GetItemById?" +
                            "id=" +
                            this.innerHTML,
                        dataType: "JSON",
                        success: function (data) {

                            console.log(data)

                            for (var i = 0; i < data.length; i++) {

                                $.ajax({
                                    type: "POST",
                                    url: "https://localhost:7034/api/Cart/PostSingleCartItem?" +
                                        "UserEmail=" +
                                        sessionStorage.getItem("email_input") +
                                        "&ListName=" +
                                        productSelect.options[productSelect.selectedIndex].text +
                                        "&ItemName=" +
                                        data[i].itemName +
                                        "&ItemUnitSize=" +
                                        data[i].itemUnitSize +
                                        "&ItemPrice=" +
                                        data[i].itemPrice +
                                        "&ItemQty=" +
                                        1,
                                    dataType: "JSON",
                                    success: function (response) {
                                        console.log(response);
                                    },
                                    complete: function () {
                                        showCorrectToast()
                                    }
                                })
                            }
                        }
                    })
                }
            }
        };
    }
}