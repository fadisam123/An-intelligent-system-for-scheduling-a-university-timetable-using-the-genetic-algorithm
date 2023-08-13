using Spectre.Console;
using System.Data;
using Timetable.Application.GA;
using Timetable.Application.Persistence.UnitOfWork;
using Timetable.Domain.Enums.EntitiesEnums;

namespace Timetable.Application.Services.DataIO.DayTime
{
    public class LectureService : ILectureService
    {
        private IUnitOfWork Uow { get; }
        public LectureService(IUnitOfWork uow)
        {
            Uow = uow;
        }
        public IEnumerable<Lecture> GenerateLabSchedule()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lecture> GenerateTheorySchedule(Semester semester)
        {
            GeneticAlgorithm ga = new GeneticAlgorithm(
                        maxGenerations: 1000,
                        populationSize: 90,
                        tournamentSize: 5,
                        crossoverRate: 0.7,
                        mutationRate: 0.01,
                        semester,
                        Uow
                        );
            var result = ga.Run();

            ProcessTimetable(result, Uow);

            Uow.LectureRepository.RemoveRange(Uow.LectureRepository.Find(l => l.Type == LectureTypeEnum.TheoryLecture && l.course.semester.SemesterNo == semester.SemesterNo));
            Uow.LectureRepository.AddRange(result.Lectures);
            Uow.SaveChanges();

            return result.Lectures;
        }

        public IEnumerable<Lecture> GetLabSchedule()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lecture> GetTheorySchedule(Semester semester, Year year)
        {
            return Uow.LectureRepository.Find(l => l.Type == LectureTypeEnum.TheoryLecture && l.course.semester.SemesterNo == semester.SemesterNo && l.course.year.YearNo == year.YearNo);
        }



        private static void ProcessTimetable(TimetableIndividual timetable, IUnitOfWork uow)
        {
            var lectures = timetable.Lectures;

            // print all years timetable
            for (int currentYear = 1; currentYear <= 5; currentYear++)
            {
                // print horezintal line
                Console.WriteLine();
                Console.WriteLine();
                var rule = new Spectre.Console.Rule("Year " + currentYear + " Timetable");
                rule.Style = Style.Parse("yellow bold");
                AnsiConsole.Write(rule);
                Console.WriteLine();
                Console.WriteLine();

                // Create a table to show the data within it
                var table = new Table();
                table.Border = TableBorder.Rounded;
                table.Centered();
                table.Expand();
                table.Title("[yellow bold]Year " + currentYear + " Timetable - Semester 1[/]");

                // Add some columns
                table.AddColumn(new TableColumn("").Centered());
                table.AddColumn(new TableColumn("[navy bold]1[/]").Centered());
                table.AddColumn(new TableColumn("[navy bold]2[/]").Centered());
                table.AddColumn(new TableColumn("[navy bold]3[/]").Centered());
                table.AddColumn(new TableColumn("[navy bold]4[/]").Centered());
                table.AddColumn(new TableColumn("[navy bold]5[/]").Centered());
                table.AddColumn(new TableColumn("[navy bold]7[/]").Centered());

                // Add some rows
                table.AddRow("[navy bold]9-10[/]");
                table.AddEmptyRow();
                table.AddRow("[navy bold]10-11[/]");
                table.AddEmptyRow();
                table.AddRow("[navy bold]11-12[/]");
                table.AddEmptyRow();
                table.AddRow("[navy bold]12-1[/]");
                table.AddEmptyRow();
                table.AddRow("[navy bold]1-2[/]");
                table.AddEmptyRow();
                table.AddRow("[navy bold]2-3[/]");
                table.AddEmptyRow();
                table.AddRow("[navy bold]3-4[/]");
                table.AddEmptyRow();
                table.AddRow("[navy bold]4-5[/]");

                // fill the table with data
                AnsiConsole.MarkupLine("[grey bold]Year " + currentYear + " Timetable - Semester 1[/]");
                var currentYearLectures = lectures.Where(l => l.course.year.YearNo == currentYear).OrderBy(l => l.day.DayNo).ThenBy(l => l.Time.Start);
                foreach (var item in currentYearLectures)
                {
                    AnsiConsole.MarkupLineInterpolated($"[grey]{item.ToString()}[/]");
                    if (item.Room.Id == item.course?.TeacherpreferredRoom?.Id)
                    {
                        AnsiConsole.MarkupLineInterpolated($"[rgb(0,100,0)]Teacher Preferred to study this course in: {item.course?.TeacherpreferredRoom?.Name}[/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLineInterpolated($"[rgb(255,0,0)]Teacher Preferred to study this course in: {item.course?.TeacherpreferredRoom?.Name}[/]");
                    }
                    //var temp = item.Course.Teacher.ListOfPreferredDayTime.Where(pdt => pdt.preferredDay == item.Day && pdt.preferredTime == item.Time).ToList();
                    //if (temp.Count != 0)
                    //{
                    //    AnsiConsole.MarkupLineInterpolated($"[rgb(0,100,0)]Preferred Day and Time :{temp[0].preferredDay + ", " + temp[0].preferredTime}[/]");
                    //}
                    //temp = item.Course.Teacher.ListOfPreferredDayTime.Where(pdt => pdt.preferredDay != item.Day && pdt.preferredTime != item.Time).ToList();
                    //if(temp.Count != 0)
                    //{
                    //    for (int i = 0; i < temp.Count; i++)
                    //    {
                    //        AnsiConsole.MarkupLineInterpolated($"[rgb(255,0,0)]Un satisfaied Preferred Day and Time:[/]");
                    //        AnsiConsole.MarkupLineInterpolated($"[rgb(255,0,0)]Preferred Day and Time :{temp[i].preferredDay + ", " + temp[i].preferredTime}[/]");
                    //    }
                    //}
                    int rowCellNumber;
                    if (int.Parse(item.Time.Start.ToString().Split(":")[0]) >= 9)
                    {
                        rowCellNumber = Math.Abs(int.Parse(item.Time.Start.ToString().Split(":")[0]) - 9) * 2;
                    }
                    else
                    {
                        rowCellNumber = (Math.Abs(int.Parse(item.Time.Start.ToString().Split(":")[0]) - 1) * 2) + 8;
                    }
                    table.UpdateCell(rowCellNumber, item.day.DayNo, item.course.Name + "\n" + item.course.user.Name + "\n" + item.Room.Name);
                }
                AnsiConsole.MarkupLineInterpolated($"[grey bold]Count: {currentYearLectures.Count()}[/]");
                Console.WriteLine();


                // find all conflicts
                AnsiConsole.MarkupLine("[red bold]Violations (Conflicts):[/]");
                int violation = 0;
                // find conflicts within same year
                foreach (var item in currentYearLectures)
                {
                    var x = currentYearLectures.Except(new[] { item });
                    foreach (var item2 in x)
                    {
                        if (item.day.DayNo == item2.day.DayNo && item.Time.Id == item2.Time.Id)
                        {
                            violation++;
                            AnsiConsole.MarkupLineInterpolated($"[red bold]{item.ToString() + " (Conf within same y)"}[/]");
                        }
                    }
                }
                // conflict with lectures from other yeares
                foreach (var item in currentYearLectures)
                {
                    var x = lectures.Except(currentYearLectures);
                    foreach (var item2 in x)
                    {
                        if (item.day.DayNo == item2.day.DayNo && item.Time.Id == item2.Time.Id && item.Room == item2.Room)
                        {
                            violation++;
                            AnsiConsole.MarkupLineInterpolated($"[red bold]{item.ToString() + " (Conf with other y 'y" + item2.course.year.YearNo + "'), "}[/]");
                        }
                    }
                }

                AnsiConsole.MarkupLineInterpolated($"[red bold]{"Count: " + violation}[/]");
                Console.WriteLine();
                // Render the table to the console
                AnsiConsole.Write(table);
            }


            // print horezintal line
            Console.WriteLine();
            Console.WriteLine();
            var rule2 = new Spectre.Console.Rule("Teacher Preferences");
            rule2.Style = Style.Parse("yellow bold");
            AnsiConsole.Write(rule2);



            // print all teachers preferences
            double totalHits = 0.0;
            double localHits = 0.0;
            double localNoHits = 0.0;
            double totalNoHits = 0.0;
            
            var teachers = uow.TeacherRepository.GetAll().ToList();
            foreach (var teacher in teachers)
            {
                localHits = 0.0;
                localNoHits = 0.0;
                AnsiConsole.MarkupLineInterpolated($"[deeppink4_1 bold]{teacher.Name} preferences:[/]");

                if (teacher.Preferences != null)
                {
                    var teacherPreferences = teacher.Preferences.OrderBy(pdt => pdt.day.DayNo).ThenBy(pdt => pdt.time.Start).ToList();
                    foreach (var preference in teacherPreferences)
                    {
                        var currentTeacherLectures = lectures.Where(l => l.course.user.Id == teacher.Id).ToList();
                        var matchedLecture = currentTeacherLectures.Where(ctl => ctl.day.DayNo == preference.day.DayNo && ctl.Time.Id == preference.time.Id).ToList();
                        if (matchedLecture.Count() != 0)
                        {
                            // MC: means Match Count
                            AnsiConsole.MarkupLineInterpolated($"[green]{preference.ToString() + " (MC:" + matchedLecture.Count + ", Y" + matchedLecture.First().course.year.YearNo + ", " + matchedLecture.First().course.Name})[/]");
                            localHits++;
                        }
                        else
                        {
                            AnsiConsole.MarkupLineInterpolated($"[deeppink4_1]{preference.ToString()}[/]");
                            localNoHits++;
                        }
                    }
                    AnsiConsole.MarkupLineInterpolated($"[deeppink4_1 bold]Count: {localHits + localNoHits} [/][deeppink4_1 bold]| [/][green bold]Hits: {localHits} [/][deeppink4_1 bold]| [/][maroon bold]No Hits: {localNoHits}[/] [deeppink4_1 bold]| [/][deeppink4_1 bold]({Math.Round(localHits / (localHits + localNoHits) * 100, 2)}%)[/]");
                    Console.WriteLine();
                    totalHits += localHits;
                    totalNoHits += localNoHits;
                }
            }
            AnsiConsole.MarkupLineInterpolated($"[blue bold]Total Satisfaction: {Math.Round(totalHits / (totalHits + totalNoHits) * 100, 2)}%[/]");

            // print fitness value
            Console.WriteLine();
            AnsiConsole.MarkupInterpolated($"[blue bold]Solution's Fitness = {timetable.Fitness}[/]");
            Console.WriteLine();

        }


    }
}
