var isEditing = false;

function toggleEditSave() {
    var button = document.getElementById("editSaveButton");
    var table = document.getElementById("myTable");
    var rows = table.getElementsByTagName("tr");

    if (isEditing) {

        for (var i = 0; i < rows.length; i++) {
            var cells = rows[i].getElementsByTagName("td");

            for (var j = 0; j < cells.length; j++) {
                cells[j].setAttribute("contenteditable", "false");
            }
        }

        button.innerHTML = "تعديل";
        isEditing = false;
    } else {

        for (var i = 0; i < rows.length; i++) {
            var cells = rows[i].getElementsByTagName("td");

            for (var j = 0; j < cells.length; j++) {
                cells[j].setAttribute("contenteditable", "true");
            }
        }

        button.innerHTML = "حفظ";
        isEditing = true;
    }
}