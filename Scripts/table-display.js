let storeData = "";
let editState = 0;

function removeTable(event, tableId) {
    let data = { TableId: tableId }
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
    let tableName = $("#table-name").val();
    let data = { TableName: tableName };
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

function editFieldMode(event) {
    if (editState !== 1) {
        editState = 1;
        storeData = $(event.target).html();
        $(event.target).html(`<input type="text" value="${event.target.innerText}" /><button class="btn btn-success update-item-button" onclick="updateTable(event)">Edit</button><button class="btn btn-primary update-item-button" onclick="revertValue(event)">X</button>`);
    }
}

function revertValue(event) {
    $(event.target).parent().html(storeData);
    editState = 0;
    event.stopPropagation();
}

function updateTable(event) {
    let tableId = $(event.target).parent().parent().children("td:hidden").children().val();
    let tableName = $(event.target).parent().children(":first").val();
    let data = { TableId: tableId, TableName: tableName};
    $.ajax({
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        url: "/MealAndTable/UpdateTable",
        dataType: "json",
        success: function (data) {
            alert("Pomyślnie zaktualizowano nazwę stolika");
            $(event.target).parent().html(data.TableName);
            editState = 0;
        },
        error: function () {
            alert("Wystąpił błąd");
        }
    });
    event.stopPropagation();
}