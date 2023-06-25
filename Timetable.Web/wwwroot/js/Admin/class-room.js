function addRoom() {
    var roomName = document.getElementById("room-name").value;
    var roomType = document.getElementById("room-type").value;

    var table = document.getElementById("rooms-table");
    var row = table.insertRow();

    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var cell3 = row.insertCell(2);

    cell1.innerHTML = '<button style="background-color: #1F6E8C; border: none; color: #fff; border-radius: 5px;" onclick="editName(this)">تعديل</button> <button style="background-color: #1F6E8C; border: none; color: #fff; border-radius: 5px; margin-top: 5px;" onclick="deleteName(this)">حذف</button>';
    cell2.innerHTML = roomName;
    cell3.innerHTML = roomType;
}

function editRoom(button) {
    var row = button.parentNode.parentNode;
    var rowCells = row.getElementsByTagName("td");

    var roomType = rowCells[2].innerHTML;
    var roomName = rowCells[1].innerHTML;

    var newRoomType = prompt("يرجى إدخال الصفة الجديدة:", roomType);
    var newRoomName = prompt("يرجى إدخال اسم القاعة الجديدة:", roomName);

    rowCells[2].innerHTML = newRoomType;
    rowCells[1].innerHTML = newRoomName;
}

function deleteRoom(button) {
    var row = button.parentNode.parentNode;
    row.parentNode.removeChild(row);
}