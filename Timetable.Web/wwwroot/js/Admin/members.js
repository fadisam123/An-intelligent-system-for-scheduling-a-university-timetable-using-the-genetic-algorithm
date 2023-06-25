function addteacher() {
    var teacherName = document.getElementById("teacher-name").value;
    var teacherType = document.getElementById("teacher-type").value;

    var table = document.getElementById("teachers-table");
    var row = table.insertRow();
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var cell3 = row.insertCell(2);

    cell1.innerHTML = '<button style="background-color: #1F6E8C; margin-top: 5px; border: none; color: #fff; border-radius: 5px;" onclick="editName(this)">تعديل</button> <button style="background-color: #1F6E8C; border: none; color: #fff; border-radius: 5px;" onclick="deleteName(this)">حذف</button>';
    cell2.innerHTML = teacherName;
    cell3.innerHTML = teacherType;
}

function editteacher(button) {
    var row = button.parentNode.parentNode;
    var rowCells = row.getElementsByTagName("td");

    var teacherType = rowCells[2].innerHTML;
    var teacherName = rowCells[1].innerHTML;

    var newteacherType = prompt("يرجى إدخال الصفة الجديدة:", teacherType);
    var newteacherName = prompt("يرجى إدخال اسم القاعة الجديدة:", teacherName);

    rowCells[2].innerHTML = newteacherType;
    rowCells[1].innerHTML = newteacherName;
}

function deleteteacher(button) {
    var row = button.parentNode.parentNode;
    row.parentNode.removeChild(row);
}