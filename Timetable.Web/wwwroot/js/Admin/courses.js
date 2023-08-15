
const courseForm = document.getElementById('courseForm');
const courseTableBody = document.getElementById('courseTableBody');
const practicalCheckbox = document.getElementById('practical');
const practicalCountGroup = document.getElementById('practicalCountGroup');
let editRowIndex = -1;

courseForm.addEventListener('submit', function (e) {
    e.preventDefault();
    const courseName = document.getElementById('courseName').value;
    const year = document.getElementById('year').value;
    const semester = document.getElementById('semester').value;
    const lectureCount = document.getElementById('lectureCount').value;
    const hasPractical = practicalCheckbox.checked;
    const practicalCount = document.getElementById('practicalCount').value || '';

    if (editRowIndex === -1) {

        const newRow = document.createElement('tr');
        newRow.innerHTML = `
      <td>
        <button style="background-color: #1F6E8C; border: none; margin-top: 5px; color: #fff; border-radius: 5px;" class="editBtn">تعديل</button>
        <button style="background-color: #1F6E8C; border: none; color: #fff; border-radius: 5px;" class="deleteBtn">حذف</button>
      </td>
      <td>${hasPractical ? 'نعم' : 'لا'}</td>
      <td>${semester}</td>
      <td>${year}</td>
      <td>${courseName}</td>
    `;
        courseTableBody.appendChild(newRow);
    } else {

        const row = courseTableBody.rows[editRowIndex];
        row.cells[1].textContent = hasPractical ? 'نعم' : 'لا';
        row.cells[2].textContent = semester;
        row.cells[3].textContent = year;
        row.cells[4].textContent = courseName;
    }


    courseForm.reset();
    practicalCheckbox.checked = false;
    //practicalCountGroup.style.display = 'none';
    editRowIndex = -1;
});

practicalCheckbox.addEventListener('change', function () {
    //practicalCountGroup.style.display = this.checked ? 'block' : 'none';
});

courseTableBody.addEventListener('click', function (e) {
    const target = e.target;
    if (target.classList.contains('editBtn')) {

        const row = target.parentNode.parentNode;
        const cells = row.cells;
        const hasPractical = cells[1].textContent === 'نعم';
        const semester = cells[2].textContent;
        const year = cells[3].textContent;
        const courseName = cells[4].textContent;

        const updatedCourseName = prompt('يرجى إدخال اسم المقرر', courseName);
        if (updatedCourseName !== null) {
            const updatedYear = prompt('يرجى إدخال السنة', year);
            if (updatedYear !== null) {
                const updatedSemester = prompt('يرجى إدخال الفصل', semester);
                if (updatedSemester !== null) {
                    const updatedHasPractical = confirm('هل لها قسم عملي؟');


                    cells[1].textContent = updatedHasPractical ? 'نعم' : 'لا';
                    cells[2].textContent = updatedSemester;
                    cells[3].textContent = updatedYear;
                    cells[4].textContent = updatedCourseName;
                }
            }
        }

        editRowIndex = -1;
    } else if (target.classList.contains('deleteBtn')) {

        const row = target.parentNode.parentNode;
        courseTableBody.removeChild(row);
        editRowIndex = -1;
    }
});
