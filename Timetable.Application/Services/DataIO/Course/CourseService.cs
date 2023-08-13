﻿using Timetable.Application.Persistence.UnitOfWork;
using Timetable.Domain.Enums.EntitiesEnums;

namespace Timetable.Application.Services.DataIO.Course
{
    using System.Linq;
    using Timetable.Domain.Entities;
    public class CourseService : ICourseService
    {
        private IUnitOfWork Uow { get; }
        public CourseService(IUnitOfWork uow)
        {
            Uow = uow;
        }

        public async Task createCourseAsync(Course course)
        {
            Uow.CourseRepository.Add(course);
            Uow.SaveChanges();
        }

        public IEnumerable<Course> getAllTheoryCourses()
        {
            return Uow.CourseRepository.getAllTheoryCourses();
        }

        public Course getCourseById(Guid courseId)
        {
            return Uow.CourseRepository.GetById(courseId);
        }

        public IEnumerable<Year> getAllYears()
        {
            return Uow.YearRepository.GetAll().OrderBy(y => y.YearNo);
        }

        public IEnumerable<Semester> getAllSemesters()
        {
            return Uow.SemesterRepository.GetAll().OrderBy(s => s.SemesterNo);
        }

        public bool HasPracticalSection(Course theoryCourse)
        {
            if (theoryCourse.Type != CourseTypeEnum.TheoryCourse)
                throw new NotImplementedException("Course must be of theory type");

            return Uow.CourseRepository.Find(c => c.Name == theoryCourse.Name &&
                c.Type == CourseTypeEnum.LapCourse).Any();
        }

        public Year getYear(int yearNo)
        {
            return Uow.YearRepository.Find(y => y.YearNo == yearNo).First();
        }

        public Semester getSemester(int semesterNo)
        {
            return Uow.SemesterRepository.Find(s => s.SemesterNo == semesterNo).First();
        }

        public Course GetCorrespondingLabCourse(Course theoryCourse)
        {
            return Uow.CourseRepository.Find(c => c.Name.ToUpper() == theoryCourse.Name.ToUpper() && c.Type == CourseTypeEnum.LapCourse).First();
        }

        public IEnumerable<Course> getAllLabCourses()
        {
            return Uow.CourseRepository.Find(c => c.Type == CourseTypeEnum.LapCourse);
        }

        public async Task AssignLabCourseToTeacherAsync(Course LabCourse, User Labteacher, Room room)
        {
            LabCourse.user = Labteacher;
            LabCourse.TeacherpreferredRoom = room;
            Uow.CourseRepository.Update(LabCourse);
            Uow.SaveChanges();
        }

        public async Task AssignTheoryCourseToTeacherAsync(Course TheoryCourse, User TheoryTeacher)
        {
            TheoryCourse.user = TheoryTeacher;
            Uow.CourseRepository.Update(TheoryCourse);
            Uow.SaveChanges();
        }

        public bool CheckCoursesExistWithAssignedUsers()
        {
            return Uow.CourseRepository.CheckCoursesExistWithAssignedUsers();
        }
    }
}