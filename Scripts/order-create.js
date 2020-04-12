$(document).ready(function () {
    $("#addMeal").click(function () {
        $("#table-button").css('visibility', 'visible');
        var mealItemId = $('#selectMeal').val();
        var mealQuantity = $('#quantity').val();
        var mealItem = { MealItemId: mealItemId, MealQuantity: mealQuantity };
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(mealItem),
            url: "/Order/AddItem",
            dataType: "json",
            success: function (data) {
                $("#quantity").val("");
                var items = [];
                $.each(data, function (key, val) {
                    items.push("<td>" + val + "</td>");
                });
                $("<tr/>", {
                    html: items.join("")
                }).appendTo("tbody");
            },
            error: function () {
                alert("Wystąpił błąd");
            }
        });
    });

    $("#addOrder").click(function () {
        var chosenTable = $("#chosenTable").val();
        var tableBodyObjects = $("tbody tr");
        var tableHeadNames = ["MealName", "UnitPrice", "Quantity"];
        var listOfOrderMeals = [];
        var lista = [];
        var temp = {};
        for (var i = 0; i < tableBodyObjects.length; i++) {
            temp = {};
            lista = [];
            lista = tableBodyObjects[i].getElementsByTagName("td");
            for (var j = 0; j < lista.length - 1; j++) {
                if (j == 1) {
                    continue;
                } else {
                    temp[tableHeadNames[j]] = lista[j].innerHTML;
                }

            }
            listOfOrderMeals[i] = temp;
        }

        var finalData = { SentOrderItems: listOfOrderMeals, SentTableNumber: chosenTable };
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

    $("#chosenTable").change(function () {
        $("#meal-quantity-button").css('visibility', 'visible');
    });
});