function addRoom() {
    var teacherName = document.getElementById("teacher-name").value;
    var lecture = document.getElementById("lecture").value;
    var lab = document.getElementById("lab").value;
    var table = document.getElementById("rooms-table");
    var row = table.insertRow();

    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var cell3 = row.insertCell(2);
    var cell4 = row.insertCell(3);

    cell1.innerHTML = '<button style="background-color: #1F6E8C; margin-top: 5px; border: none; color: #fff; border-radius: 5px;" onclick="editName(this)">تعديل</button> <button style="background-color: #1F6E8C; border: none; color: #fff; border-radius: 5px;" onclick="deleteName(this)">حذف</button>';
    cell2.innerHTML = lab;
    cell3.innerHTML = lecture;
    cell4.innerHTML = teacherName;
}

function editName(button) {
    var row = button.parentNode.parentNode;
    var rowCells = row.getElementsByTagName("td");

    var lecture = rowCells[2].innerHTML;
    var teacherName = rowCells[3].innerHTML;
    var lab = rowCells[1].innerHTML;
    var newteacherName = prompt("يرجى إدخال اسم المدرس :", teacherName);
    var newlecture = prompt("يرجى إدخال اسم المقرر :", lecture);
    var newlab = prompt("يرجى إدخال اسم المخبر :", lab);

    rowCells[3].innerHTML = newlecture;
    rowCells[2].innerHTML = newteacherName;
    rowCells[1].innerHTML = newlab;
}

function deleteName(button) {
    var row = button.parentNode.parentNode;
    row.parentNode.removeChild(row);
}