namespace Timetable.Domain.Enums
{
    public enum RoleEnum
    {
        Admin,
        [Display(Name = "رئيس قسم")]
        DepartmentHead,
        [Display(Name = "هيئة تدريسية")]
        Professor,
        [Display(Name = "مدرس عملي")]
        LapTeacher
    }
}
