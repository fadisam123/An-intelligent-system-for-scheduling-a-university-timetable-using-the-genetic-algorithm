namespace Timetable.Domain.Enums.EntitiesEnums
{
    public enum UserTypeEnum
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
