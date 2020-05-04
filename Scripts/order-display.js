let storeData = "";
let editState = 0;

function addPosition(orderId, passedValue) {
    let mealId = $("#select-meal_" + passedValue).val();
    let quantity = $("#quantity_" + passedValue).val();
    let data = { MealId: mealId, Quantity: quantity, OrderId: orderId };
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
            let newMeal = `<tr><td style="display:none"><input type="hidden" value="${data.Data.OrderItemId}"/></td>
                <td colspan="2" class="order-meal-name" onclick="editFieldMode(event)">${data.Data.Meal.MealName} | ${data.Data.Meal.MealUnitPrice} PLN</td>
                <td onclick="editFieldMode(event)">${data.Data.Quantity}</td>
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

function editFieldMode(event, positionNumber, orderStatus) {
    if (orderStatus === "PendingPayment") {
        if (editState !== 1) {
            editState = 1;
            storeData = $(event.target).html();
            if ($(event.target).attr("class") === "order-meal-name") {
                let selectMeal = $(".add-in-display:first").html();
                $(event.target).html(`<select id="change-meal-select">${selectMeal}</select><button class="btn btn-success update-item-button" onclick="updateOrderItem(event,${positionNumber})">Edit</button><button class="btn btn-primary update-item-button" onclick="revertValue(event)">X</button>`);
            } else {
                $(event.target).html(`<input type="text" style="${$(event.target).attr("class") === "order-meal-name" ? "width:30rem" : "width:5rem"}" value="${event.target.innerText}" /><button class="btn btn-success update-item-button" onclick="updateOrderItem(event,${positionNumber})">Edit</button><button class="btn btn-primary update-item-button" onclick="revertValue(event)">X</button>`);
            }
        }
    }  
}

function revertValue(event) {
    $(event.target).parent().html(storeData);
    editState = 0;
    event.stopPropagation();
}

function updateOrderItem(event, positionNumber) {
    let orderItemId = $(event.target).parent().parent().children("td:hidden").children().val();
    let orderData = "";
    if ($(event.target).parent().attr("class") === "order-meal-name") {
        orderData = $(event.target).parent().children(":first").children("option:selected").text().split(" | ")[0];
    } else {
        orderData = $(event.target).parent().children(":first").val();
    }
    let itemData = { OrderItemId: orderItemId, OrderData: orderData };
    $.ajax({
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(itemData),
        url: "/Order/UpdateOrderItem",
        dataType: "json",
        success: function (data) {
            alert("Pomyślnie zaktualizowano zamówienie");
            if ($(event.target).parent().attr("class") === "order-meal-name") {
                $("#fourth_" + positionNumber).children(":first").children(":first").text("Cena zamówienia: " + data.TotalPrice + " PLN");
                $(event.target).parent().siblings(".position-price").text(data.Data.Price + " PLN");
                $(event.target).parent().text(data.Data.Meal.MealName + " | " + data.Data.Meal.MealUnitPrice + " PLN");
            } else {
                $("#fourth_" + positionNumber).children(":first").children(":first").text("Cena zamówienia: " + data.TotalPrice + " PLN");
                $(event.target).parent().next().text(data.Data.Price + " PLN");
                $(event.target).parent().text(data.Data.Quantity);
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
    let ids = { OrderId: orderId, OrderItemId: orderItemId };
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
    let items = { OrderId: orderId };
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