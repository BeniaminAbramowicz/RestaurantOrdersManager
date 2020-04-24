let storeData = "";
let editState = 0;

function addPosition(orderId, passedValue) {
    var mealId = $("#select-meal_" + passedValue).val();
    var quantity = $("#quantity_" + passedValue).val();
    var data = { MealId: mealId, Quantity: quantity, OrderId: orderId };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        url: "/Order/AddPosition",
        dataType: "json",
        success: function (data) {
            $("#select-meal_" + passedValue).val("");
            $("#quantity_" + passedValue).val("");
            alert("Dodano pozycję do zamówienia");
            var newMeal = `<tr><td>${data.Data.Meal.MealName}</td>
                <td>${data.Data.Meal.MealUnitPrice} PLN</td>
                <td>${data.Data.Quantity}</td>
                <td>${data.Data.Price} PLN</td>
                <td>
                <input type="button" style="visibility:visible" class="btn btn-warning" value="Usuń pozycję" onclick="removingPosition(${data.Data.OrderId}, ${data.Data.OrderItemId}, ${passedValue}, event)"
                </td></tr>`
            $(newMeal).insertBefore("#fourth_" + passedValue);
            $("#order-total-price_" + passedValue).html("Cena zamówienia: " + data.TotalPrice + " PLN");
        },
        error: function () {
            alert("Wystąpił błąd");
        }
    });
}

function editFieldMode(event) {
    if (editState !== 1) {
        editState = 1;
        storeData = $(event.target).html();
        $(event.target).html(`<input type="${$(event.target).attr("class") === "order-meal-name" ? "text" : "number"}" style="${$(event.target).attr("class") === "order-meal-name" ? "width:30rem" : "width:5rem"}" value="${event.target.innerText}" /><button onclick="updateOrderItem(event)">Edit</button><button onclick="revertValue(event)">X</button>`);
    }
}

function revertValue(event) {
    $(event.target).parent().html(storeData);
    editState = 0;
    event.stopPropagation();
}

function updateOrderItem(event) {
    var orderItemId = $(event.target).parent().parent().children("td:hidden").children().val();
    var orderData = $(event.target).parent().children(":first").val();
    var data = { OrderItemId: orderItemId, OrderData: orderData };
    $.ajax({
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        url: "/MealAndTable/UpdateOrderItem",
        dataType: "json",
        success: function (data) {
            alert("Pomyślnie zaktualizowano zamówienie");
            if ($(event.target).parent().attr("class") === "order-meal-name") {
                $(event.target).parent().html(data.Meal.MealName);
                $("#order-total-price_" + positionNumber).text("Cena zamówienia: " + val + " PLN")
            } else {
                $(event.target).parent().html(data.Quantity);
                $("#order-total-price_" + positionNumber).text("Cena zamówienia: " + val + " PLN")
            }
            editState = 0;
        },
        error: function () {
            alert("Wystąpił błąd");
        }
    });
    event.stopPropagation();
}

function removingPosition(orderId, orderItemId, positionNumber, event) {
    var ids = { OrderId: orderId, OrderItemId: orderItemId };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(ids),
        url: "/Order/RemovePosition",
        dataType: "json",
        success: function (data) {
            $(event.target).parent().parent().remove();
            $.each(data, function (key, val) {
                alert("Removed chosen position");
                $("#order-total-price_" + positionNumber).text("Cena zamówienia: " + val + " PLN")
            });
        },
        error: function () {
            alert("Wystąpił błąd");
        }
    });
}
function removeOrder(orderId, positionNumber) {
    var items = { OrderId: orderId };
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