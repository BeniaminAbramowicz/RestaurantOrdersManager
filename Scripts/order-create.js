$(document).ready(function () {
    $("#chosen-table").change(function () {
        $("#meal-quantity-button").css('visibility', 'visible');
    });

    $("#add-meal").click(function () {
        $("#table-button").css('visibility', 'visible');
        var mealItemId = $('#select-meal').val();
        var mealQuantity = $('#quantity').val();
        var mealNamePrice = $('#select-meal :selected').text().split(" | ");
        var mealName = mealNamePrice[0];
        var mealUnitPrice = mealNamePrice[1];
        $(`<tr>
            <td style="display: none"><input type="hidden" value="${mealItemId}" /></td>
            <td>${mealName}</td>
            <td>${mealUnitPrice}</td>
            <td>${mealQuantity}</td>
            <td>${mealQuantity * parseFloat(mealUnitPrice)}</td>
            </tr>`)
            .appendTo($("tbody"));  
    });

    $("#add-order").click(function () {
        var chosenTable = $("#chosen-table").val();
        var tableBodyObjects = $("tbody tr");
        console.log(tableBodyObjects[0].getElementsByTagName("td"));
        var tableHeadNames = ["", "MealName", "", "Quantity"];
        var OrderItemsList = [];
        var lista = [];
        var temp = {};
        for (var i = 0; i < tableBodyObjects.length; i++) {
            temp = {};
            lista = [];
            lista = tableBodyObjects[i].getElementsByTagName("td");
            for (var j = 0; j < lista.length - 1; j++) {
                if (j == 0 || j == 2 ) {
                    continue;
                } else {
                    temp[tableHeadNames[j]] = lista[j].innerHTML;
                }

            }
            OrderItemsList[i] = temp;
        }

        var finalData = { OrderItems: OrderItemsList, TableNumber: chosenTable };
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(finalData),
            url: "/Order/CreateOrder",
            dataType: "json",
            success: function () {
                alert("Dodano zamówienie");
                location.href = "/Home/Index";
            },
            error: function () {
                alert("Wystąpił błąd");
            }
        });
    });
});