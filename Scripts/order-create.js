$(document).ready(function () {
    $("#chosen-table").change(function () {
        $("#meal-quantity-button").css('visibility', 'visible');
    });

    $("#add-meal").click(function () {
        $("#table-button").css('visibility', 'visible');
        let mealItemId = $('#select-meal').val();
        let mealQuantity = $('#quantity').val();
        let mealNamePrice = $('#select-meal :selected').text().split(" | ");
        let mealName = mealNamePrice[0];
        let mealUnitPrice = mealNamePrice[1];
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
        let chosenTable = $("#chosen-table").val();
        let tableBodyObjects = $("tbody tr");
        let tableHeadNames = ["", "MealName", "", "Quantity"];
        let OrderItemsList = [];
        let lista = [];
        let temp = {};
        for (let i = 0; i < tableBodyObjects.length; i++) {
            temp = {};
            lista = [];
            lista = tableBodyObjects[i].getElementsByTagName("td");
            for (let j = 0; j < lista.length - 1; j++) {
                if (j == 0 || j == 2 ) {
                    continue;
                } else {
                    temp[tableHeadNames[j]] = lista[j].innerHTML;
                }

            }
            OrderItemsList[i] = temp;
        }

        let finalData = { OrderItems: OrderItemsList, TableNumber: chosenTable };
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