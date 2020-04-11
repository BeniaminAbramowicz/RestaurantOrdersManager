function removeMeal(event, mealId) {
    var data = { MealId: mealId }
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        url: "/MealAndTable/RemoveMeal",
        dataType: "json",
        success: function () {
            alert("Pomyślnie usunięto danie z listy");
            $(event.target).parent().parent().remove();
        },
        error: function () {
            alert("Wystąpił błąd");
        }
    });
}

function addMeal(event) {
    event.preventDefault();
    var mealName = $("#meal-name").val();
    var mealUnitPrice = $("#meal-unit-price").val();
    var data = { MealName: mealName, MealUnitPrice: mealUnitPrice };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        url: "/MealAndTable/AddMeal",
        dataType: "json",
        success: function (data) {
            alert("Pomyślnie dodano nowe danie do listy dań");
            $(`<tr>
                    <td style="display: none"><input type="hidden" value="${data.MealId}" /></td>
                    <td>${data.MealName}</td>
                    <td>${data.MealUnitPrice.toFixed(2)} PLN</td>
                    <td><button class="btn btn-success" onclick="editMeal(event)">Edytuj pozycję</button></td>
                    <td><button class="btn btn-danger" onclick="removeMeal(event, ${data.MealId})">Usuń pozycję</button></td>
                    </tr>`)
                .insertAfter($("tbody tr:last-child"));
        },
        error: function () {
            alert("Wystąpił błąd");
        }
    });
}

function editMeal(event) {
    $("#update-meal-form").remove();
    $(`<tr id="update-meal-form">
            <td><label for="edit-meal-name">Nazwa dania</label><input type="text" class="form-control" id="edit-meal-name"/></td>
            <td><label for="editMealUnitPrice">Cena dania</label><input type="text" class="form-control" id="edit-meal-unit-price"/></td>
            <td colspan="2" style="vertical-align: bottom;"><button class="btn btn-primary" onclick="updateMeal(event)">Aktualizuj danie</button></td>
            </tr>`)
        .insertAfter($(event.target).parent().parent());
}

function updateMeal(event) {
    var mealId = $(event.target).parent().parent().prev().children(":first").children(":first").val();
    var mealName = $("#edit-meal-name").val();
    var mealUnitPrice = $("#edit-meal-unit-price").val();
    var data = { MealId: mealId, MealName: mealName, MealUnitPrice: mealUnitPrice };
    $.ajax({
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        url: "/MealAndTable/UpdateMeal",
        dataType: "json",
        success: function (data) {
            alert("Pomyślnie zaktualizowano danie");
            $(event.target).parent().parent().prev().children(":nth-child(2)").html(data.MealName);
            $(event.target).parent().parent().prev().children(":nth-child(3)").html(data.MealUnitPrice.toFixed(2) + " PLN");
            $("#update-meal-form").remove();
        },
        error: function () {
            alert("Wystąpił błąd");
        }
    });
}