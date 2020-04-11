function removeTable(event, tableId) {
    var data = { TableId: tableId }
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        url: "/MealAndTable/RemoveTable",
        dataType: "json",
        success: function () {
            alert("Pomyślnie usunięto stolik z listy");
            $(event.target).parent().parent().remove();
        },
        error: function () {
            alert("Wystąpił błąd");
        }
    });
}

function addTable(event) {
    event.preventDefault();
    var tableName = $("#table-name").val();
    var data = { TableName: tableName };
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        url: "/MealAndTable/AddTable",
        dataType: "json",
        success: function (data) {
            alert("Pomyślnie dodano stolik do listy");
            $(`<tr>
                <td style="display: none"><input type="hidden" value="${data.TableId}" /></td>
                <td>${data.TableName}</td>
                <td><button class="btn btn-success" onclick="editTable(event)">Edytuj pozycję</button></td>
                <td><button class="btn btn-danger" onclick="removeTable(event, ${data.TableId})">Usuń pozycję</button></td>
                </tr>`)
                .insertAfter($("tbody tr:last-child"));
        },
        error: function () {
            alert("Wystąpił błąd");
        }
    });
}

function editTable(event) {
    $("#update-meal-form").remove();
    $(`<tr id="update-meal-form">
        <td><label for="edit-table-name">Nazwa stolika</label><input type="text" class="form-control" id="edit-table-name"/></td>
        <td colspan="2" style="vertical-align: bottom;"><button class="btn btn-primary" onclick="updateTable(event)">Aktualizuj nazwę stolika</button></td>
        </tr>`)
        .insertAfter($(event.target).parent().parent());
}

function updateTable(event) {
    var tableId = $(event.target).parent().parent().prev().children(":first").children(":first").val();
    var tableName = $("#edit-table-name").val();
    var data = { TableId: tableId, TableName: tableName};
    $.ajax({
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        url: "/MealAndTable/UpdateTable",
        dataType: "json",
        success: function (data) {
            alert("Pomyślnie zaktualizowano nazwę stolika");
            $(event.target).parent().parent().prev().children(":nth-child(2)").html(data.TableName);
            $("#update-meal-form").remove();
        },
        error: function () {
            alert("Wystąpił błąd");
        }
    });
}