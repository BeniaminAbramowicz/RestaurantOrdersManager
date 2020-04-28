let storeData = "";
let editState = 0;

function removeMeal(event, mealId) {
    let data = { MealId: mealId }
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
    let mealName = $("#meal-name").val();
    let mealUnitPrice = $("#meal-unit-price").val();
    let data = { MealName: mealName, MealUnitPrice: mealUnitPrice };
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
                    <td><button class="btn btn-danger" onclick="removeMeal(event, ${data.MealId})">Usuń pozycję</button></td>
                    </tr>`)
                .insertAfter($("tbody tr:last-child"));
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
        $(event.target).html(`<input type="text" value="${$(event.target).attr("class") === "meal-name" ? event.target.innerText : $(event.target).text().split(" ")[0] }" /><button onclick="updateMeal(event)">Edit</button><button onclick="revertValue(event)">X</button>`);
    }
}

function revertValue(event) {
    $(event.target).parent().html(storeData);
    editState = 0;
    event.stopPropagation();
}

function updateMeal(event) {
    let mealId = $(event.target).parent().parent().children("td:hidden").children().val();
    let mealData = $(event.target).parent().children(":first").val();
    let data = { MealId: mealId, MealData: mealData };
    $.ajax({
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        url: "/MealAndTable/UpdateMeal",
        dataType: "json",
        success: function (data) {
            alert("Pomyślnie zaktualizowano danie");
            if ($(event.target).parent().attr("class") === "meal-name") {
                $(event.target).parent().html(data.MealName);
            } else {
                $(event.target).parent().html(data.MealUnitPrice.toFixed(2).replace(".", ",") + " PLN");
            }
            editState = 0;
        },
        error: function () {
            alert("Wystąpił błąd");
        }
    });
    event.stopPropagation();
}