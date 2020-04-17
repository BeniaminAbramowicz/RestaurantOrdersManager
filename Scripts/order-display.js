function addInDisplay(idOfOrder, passedValue) {
    var mealItemId = $("#select-meal_" + passedValue).val();
    var mealQuantity = $("#quantity_" + passedValue).val();
    var sendData = { MealItemId: mealItemId, MealQuantity: mealQuantity, IdOfOrder: idOfOrder };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(sendData),
        url: "/Order/AddToDisplay",
        dataType: "json",
        success: function () {
            $("#quantity").val("");
            alert("Dodano pozycję do zamówienia");
            location.href = "/Home/Index";
        },
        error: function () {
            alert("Wystąpił błąd");
        }
    });
}

function removingPosition(idOfOrder, itemName, complexPositionNumber, positionNumber) {
    var items = { IdOfOrder: idOfOrder, ItemName: itemName };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(items),
        url: "/Order/RemovePosition",
        dataType: "json",
        success: function (data) {
            $("#" + complexPositionNumber).remove();
            $.each(data, function (key, val) {
                $("#order-total-price_" + positionNumber).text("Cena zamówienia: " + val + " PLN")
            });
        },
        error: function () {
            alert("Wystąpił błąd");
        }
    });
}
function removeOrder(idOfOrder, positionNumber) {
    var items = { IdOfOrder: idOfOrder };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(items),
        url: "/Order/RemoveOrder",
        dataType: "json",
        success: function () {
            alert("Usunięto zamówienie");
            $("#third_" + positionNumber).nextUntil("#fifth_" + positionNumber).remove();
            $("#headTr").remove();
            $("#first_" + positionNumber).remove();
            $("#second_" + positionNumber).remove();
            $("#third_" + positionNumber).remove();
            $("#fourth_" + positionNumber).remove();
            $("#fifth_" + positionNumber).remove();
        },
        error: function () {
            alert("Wystąpił błąd");
        }
    });
}