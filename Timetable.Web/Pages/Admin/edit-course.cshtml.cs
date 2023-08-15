using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Domain.Entities;
using Timetable.RazorWeb.ViewModels.InputModels;
using Timetable.RazorWeb.ViewModels.OutputModels;

namespace Timetable.RazorWeb.Pages.Admin
{
    public class edit_courseModel : PageModel
    {
        #region Fields
        private readonly ICourseService _courseService;
        private readonly IValidator<CourseInputModel> _validator;
        #endregion

        #region Input Data
        [BindProperty]
        public CourseInputModel courseInputModel { get; set; } = new CourseInputModel()!;
        #endregion

        #region Output Data
        public CourseOutputModel courseOutputModel { get; set; } = new CourseOutputModel();
        #endregion

        public edit_courseModel(ICourseService courseService, IValidator<CourseInputModel> validator)
        {
            _courseService = courseService;
            _validator = validator;
        }
        public async Task OnGet(string courseId)
        {
            courseOutputModel.Years = _courseService.getAllYears().ToList();
            courseOutputModel.Semesters = _courseService.getAllSemesters().ToList();

            Guid CourseId;
            if (!Guid.TryParse(courseId, out CourseId))
            {
                throw new NotImplementedException(message: courseId + " is not a valid guid");
            }
            var course = _courseService.getCourseById(CourseId);
            courseInputModel.Id = courseId;
            courseInputModel.Name = course.Name;
            courseInputModel.SelectedYear = course.year.YearNo;
            courseInputModel.SelectedSemester = course.semester.SemesterNo;
            courseInputModel.IsElective = course.IsElective;
            courseInputModel.HasPracticalSection = _courseService.HasPracticalSection(course);
            courseInputModel.LuctureNumPerWeek = course.LuctureNumPerWeek;

            if (courseInputModel.HasPracticalSection)
            {
                courseInputModel.LapLuctureNumPerWeek = _courseService.GetCorrespondingLabCourse(course).LuctureNumPerWeek;
            }
        }

        public async Task<IActionResult> OnPost(string courseId)
        {
            Guid CeacherId;
            if (!Guid.TryParse(courseId, out CeacherId))
            {
                throw new NotImplementedException(message: courseId + " is not a valid guid");
            }
            Course course = _courseService.getCourseById(CeacherId);
            course.Name = courseInputModel.Name;
            course.year = _courseService.getYear(courseInputModel.SelectedYear);
            course.semester = _courseService.getSemester(courseInputModel.SelectedSemester);
            course.LuctureNumPerWeek = courseInputModel.LuctureNumPerWeek;
            course.IsElective = courseInputModel.IsElective;
            course.Type = CourseTypeEnum.TheoryCourse;

            _courseService.Update(course);

            if (courseInputModel.HasPracticalSection)
            {
                Course? LabCourse = _courseService.GetCorrespondingLabCourse(course);
                if (LabCourse is not null)
                {
                    LabCourse.LuctureNumPerWeek = courseInputModel.LapLuctureNumPerWeek;
                }
                else
                {
                    Course newLabCourse = new Course {
                        Name = course.Name,
                        year = course.year,
                        semester = course.semester,
                        LuctureNumPerWeek = courseInputModel.LapLuctureNumPerWeek,
                        IsElective = course.IsElective,
                        Type = CourseTypeEnum.LapCourse
                    };
                    await _courseService.createCourseAsync(newLabCourse);
                }
            }

            return RedirectToPage("./courses");
        }
    }
}
