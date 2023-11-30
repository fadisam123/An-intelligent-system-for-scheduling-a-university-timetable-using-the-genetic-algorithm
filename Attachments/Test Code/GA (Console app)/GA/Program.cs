using Model;
using MemoryDB;
using System;
using System.Collections.Generic;
using System.Linq;

using Spectre.Console;

namespace GA
{
    //public sealed class TeacherPreferences
    //{
    //    public class TeacherPreference
    //    {
    //        public Teacher teacher { get; set; }
    //        public int day { get; set; }
    //        public TimeOnly time { get; set; }

    //        public TeacherPreference(Teacher teacher, int day, TimeOnly time)
    //        {
    //            this.teacher = teacher;
    //            this.day = day;
    //            this.time = time;
    //        }

    //        public override string ToString()
    //        {
    //            return ("Day: " + day + "\tTime: " + time);
    //        }
    //    }
    //    private static TeacherPreferences instance;
    //    private static readonly object lockObject = new object();

    //    public List<TeacherPreference> preferences = new List<TeacherPreference>();

    //    private TeacherPreferences()
    //    {
    //        Random random = new Random();
    //        DB db = DB.Instance;
    //        var firstSemesterCourses = db.Courses.Where(c => c.Semester == 1).ToList();


    //        // this will reach 100% satisfaction (no teacher have same preference with other -- all unique)
    //        for (int i = 0; i < firstSemesterCourses.Count; i++)
    //        {
    //        again: TeacherPreference tp = new TeacherPreference
    //            (
    //                firstSemesterCourses[i].Teacher,
    //                random.Next(1, 6),
    //                new TimeOnly(random.Next(9, 17), 0)
    //            );
    //            // no same preference at all
    //            var currentTeacherPreferences = preferences.Where(ctp => ctp.day == tp.day && ctp.time == tp.time).ToList();
    //            if (currentTeacherPreferences.Count != 0)
    //            {
    //                goto again;
    //            }
    //            preferences.Add(tp);
    //            if (firstSemesterCourses[i].Lecture_num_per_week == 2)
    //            {
    //            again2: tp = new TeacherPreference
    //                (
    //                    firstSemesterCourses[i].Teacher,
    //                    random.Next(1, 6),
    //                    new TimeOnly(random.Next(9, 17), 0)
    //                );
    //                currentTeacherPreferences = preferences.Where(ctp => ctp.day == tp.day && ctp.time == tp.time).ToList();
    //                if (currentTeacherPreferences.Count != 0)
    //                {
    //                    goto again2;
    //                }
    //                preferences.Add(tp);
    //            }
    //        }


    //        //// this may or may not reach 100% satisfaction (randomly)
    //        //for (int i = 0; i < (int)firstSemesterCourses.Count; i++)
    //        //{
    //        //again: TeacherPreference tp = new TeacherPreference
    //        //    (
    //        //        firstSemesterCourses[i].Teacher,
    //        //        random.Next(1, 6),
    //        //        new TimeOnly(random.Next(9, 17), 0)
    //        //    );
    //        //    // We don't allow same preference for the same teacher (we want uniqe preferences for each teacher)
    //        //    var currentTeacherPreferences = preferences.Where(p => p.teacher.Id == firstSemesterCourses[i].Teacher.Id).Where(tp2 => (tp2.day == tp.day && tp2.time == tp.time)).ToList();
    //        //    if (currentTeacherPreferences.Count != 0)
    //        //    {
    //        //        goto again;
    //        //    }
    //        //    preferences.Add(tp);
    //        //    if (firstSemesterCourses[i].Lecture_num_per_week == 2)
    //        //    {
    //        //    again2: tp = new TeacherPreference
    //        //        (
    //        //            firstSemesterCourses[i].Teacher,
    //        //            random.Next(1, 6),
    //        //            new TimeOnly(random.Next(9, 17), 0)
    //        //        );
    //        //        currentTeacherPreferences = preferences.Where(p => p.teacher.Id == firstSemesterCourses[i].Teacher.Id).Where(tp2 => (tp2.day == tp.day && tp2.time == tp.time)).ToList();
    //        //        if (currentTeacherPreferences.Count != 0)
    //        //        {
    //        //            goto again2;
    //        //        }
    //        //        preferences.Add(tp);
    //        //    }
    //        //}


    //        //// this will not reach 100% satisfaction (more than one teacher has the same preference)
    //        //for (int i = 0; i < (int)firstSemesterCourses.Count; i++)
    //        //{
    //        //again: TeacherPreference tp = new TeacherPreference
    //        //    (
    //        //        firstSemesterCourses[i].Teacher,
    //        //        random.Next(1, 6),
    //        //        new TimeOnly(random.Next(9, 17), 0)
    //        //    );
    //        //    // We don't allow same preference for the same teacher (we want uniqe preferences for each teacher)
    //        //    var currentTeacherPreferences = preferences.Where(p => p.teacher.Id == firstSemesterCourses[i].Teacher.Id).Where(tp2 => (tp2.day == tp.day && tp2.time == tp.time)).ToList();
    //        //    if (currentTeacherPreferences.Count != 0)
    //        //    {
    //        //        goto again;
    //        //    }
    //        //    // make this preference same as previous teacher preference
    //        //    if (i%2 == 0 && i != 0)
    //        //    {
    //        //        tp.day = preferences[i - 1].day;
    //        //        tp.time = preferences[i - 1].time;
    //        //    }
    //        //    preferences.Add(tp);
    //        //    if (firstSemesterCourses[i].Lecture_num_per_week == 2)
    //        //    {
    //        //    again2: tp = new TeacherPreference
    //        //        (
    //        //            firstSemesterCourses[i].Teacher,
    //        //            random.Next(1, 6),
    //        //            new TimeOnly(random.Next(9, 17), 0)
    //        //        );
    //        //        currentTeacherPreferences = preferences.Where(p => p.teacher.Id == firstSemesterCourses[i].Teacher.Id).Where(tp2 => (tp2.day == tp.day && tp2.time == tp.time)).ToList();
    //        //        if (currentTeacherPreferences.Count != 0)
    //        //        {
    //        //            goto again2;
    //        //        }
    //        //        preferences.Add(tp);
    //        //    }
    //        //}
    //    }

    //    public static TeacherPreferences Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //            {
    //                lock (lockObject)
    //                {
    //                    if (instance == null)
    //                    {
    //                        instance = new TeacherPreferences();
    //                    }
    //                }
    //            }
    //            return instance;
    //        }
    //    }
    //}


    public class TimetableIndividual
    {
        public List<Lecture> Lectures { get; set; }
        public double Fitness { get; set; }
    }

    public class GeneticAlgorithm
    {
        private List<TimetableIndividual> population;
        private Random random;

        private int maxGenerations;
        private int populationSize;
        private int tournamentSize;
        private double crossoverRate;
        private double mutationRate;

        public GeneticAlgorithm(int maxGenerations, int populationSize, int tournamentSize, double crossoverRate, double mutationRate)
        {
            this.population = new List<TimetableIndividual>();
            this.random = new Random();
            this.maxGenerations = maxGenerations;
            this.populationSize = populationSize;
            this.tournamentSize = tournamentSize;
            this.crossoverRate = crossoverRate;
            this.mutationRate = mutationRate;
        }

        public TimetableIndividual Run()
        {
            InitializePopulation();
            CalculateFitness();
            Evolve();
            return population.OrderByDescending(individual => individual.Fitness).First();
        }

        private void InitializePopulation()
        {
            for (int i = 0; i < populationSize; i++)
            {
                TimetableIndividual individual = new TimetableIndividual();
                individual.Lectures = GenerateRandomLectures();
                population.Add(individual);
            }
        }

        // internal
        private List<Lecture> GenerateRandomLectures()
        {
            // Generate random Lectures (solution or individual)
            List<Lecture> lectures = new List<Lecture>();

            DB db = DB.Instance;
            var firsSemesterCourses = db.Courses.Where(course => course.Semester == 1).ToList();
            for (int i = 0; i < firsSemesterCourses.Count; i++)
            {
                Lecture l = new Lecture
                    (
                        (byte)random.Next(1, 6),
                        new TimeOnly(random.Next(9, 17), 0),
                        db.classRooms[random.Next(0, db.classRooms.Count)],
                        firsSemesterCourses[i]
                    );
                lectures.Add(l);
                if (firsSemesterCourses[i].Lecture_num_per_week == 2)
                {
                    Lecture l2 = new Lecture
                        (
                            (byte)random.Next(1, 6),
                            new TimeOnly(random.Next(9, 17), 0),
                            db.classRooms[random.Next(0, db.classRooms.Count)],
                            firsSemesterCourses[i]
                        );
                    lectures.Add(l2);
                }
            }
            return lectures;
        }

        private void CalculateFitness()
        {
            foreach (var individual in population)
            {
                individual.Fitness = CalculateIndividualFitness(individual);
            }
        }

        // internal
        private double CalculateIndividualFitness(TimetableIndividual individual)
        {
            double preferredDayTime = 0.0;
            double preferredRoom = 0.0;
            int conflict = 1;
            //TeacherPreferences tp = TeacherPreferences.Instance;
            for (int i = 0; i < individual.Lectures.Count(); i++)
            {
                for (int j = i + 1; j < individual.Lectures.Count(); j++)
                {
                    var i1 = individual.Lectures[i];
                    var i2 = individual.Lectures[j];

                    // conflict with lectures times and rooms
                    if (
                        i1.Day == i2.Day &&
                        i1.Time == i2.Time &&
                        i1.ClassRoom == i2.ClassRoom
                       )
                    {
                        conflict++;
                    }
                    // conflict within same year lectures times
                    if (
                        i1.Course.Year == i2.Course.Year &&
                        i1.Day == i2.Day &&
                        i1.Time == i2.Time
                       )
                    {
                        conflict++;
                    }
                    // conflict same teacher have two lectures at the same day and time
                    if (
                        i1.Course.Teacher.Id == i2.Course.Teacher.Id &&
                        i1.Day == i2.Day &&
                        i1.Time == i2.Time
                       )
                    {
                        conflict++;
                    }
                }

                // Courses with two lectures per week must satisfy this constraint
                if (
                        individual.Lectures[i].Course.Lecture_num_per_week == 2 &&
                        individual.Lectures.Where
                        (
                            lec => lec.Course.Id == individual.Lectures[i].Course.Id
                        ).Count() != 2
                    )
                {
                    conflict++;
                }

                //double hitsRate = 0.0; ;
                //var allTeachers = tp.preferences.GroupBy(tp => tp.teacher.Id).ToList();
                //for (int z = 0; z < allTeachers.Count; z++)
                //{
                //    int h = 0;
                //    var currentTeacherPreferences = allTeachers[z].ToList();
                //    for (int c = 0; c < currentTeacherPreferences.Count; c++)
                //    {
                //        var lecuterHitTeacherPreferencs = individual.Lectures.Where(l => l.Course.Teacher.Id == currentTeacherPreferences[0].teacher.Id).Where(l => l.Day == currentTeacherPreferences[c].day && l.Time == currentTeacherPreferences[c].time).ToList();

                //        if (lecuterHitTeacherPreferencs.Count != 0)
                //        {
                //            h++;
                //        }
                //    }
                //    hitsRate += h / (double)currentTeacherPreferences.Count;
                //}
                //hitsRate /= allTeachers.Count;
                //preferredDayTime = hitsRate * 500;
                
                if (individual.Lectures[i].Course.Teacher.ListOfPreferredDayTime.Where(pdt => pdt.preferredDay == individual.Lectures[i].Day && pdt.preferredTime == individual.Lectures[i].Time).Count() != 0)
                {
                    preferredDayTime++;
                }
                if (individual.Lectures[i].ClassRoom.Id == individual.Lectures[i].Course.preferredRoom.Id)
                {
                    preferredRoom++;
                }
            }
            //Console.WriteLine(preferredDayTime + "/" + individual.Lectures.Count);
            //Console.WriteLine();
            preferredDayTime = preferredDayTime / individual.Lectures.Count;
            preferredRoom = preferredRoom / individual.Lectures.Count;
            // fitness = ( Log( 1/conflict ) + 0.2 ) * preferredDayTime
            double fitness = ((Math.Log(Math.Pow(conflict, -1)) + 0.1) * 1000) * Math.Exp((preferredRoom + preferredDayTime) * 10);
            Console.WriteLine(fitness);
            return fitness;
        }


        private void Evolve()
        {
            for (int i = 0; i < maxGenerations; i++)
            {
                //// if we reach 100% satisfaction stop evolving
                //if (population.Where(i => i.Fitness == 100).Count() != 0)
                //{
                //    return;
                //}
                population = GenerateNextPopulation();
                CalculateFitness();
            }
        }

        // internal
        private List<TimetableIndividual> GenerateNextPopulation()
        {
            var newPopulation = new List<TimetableIndividual>();
            for (int i = 0; i < population.Count; i++)
            {
                // Selection
                List<TimetableIndividual> parents = SelectParents();

                // Crossover
                TimetableIndividual offspring = PerformCrossover(parents);

                // Mutation
                PerformMutation(offspring);

                newPopulation.Add(offspring);
            }
            return newPopulation;
        }

        // double internal
        private List<TimetableIndividual> SelectParents()
        {
            // tournament selection of parents from the current population
            List<TimetableIndividual> parents = new List<TimetableIndividual>();
            int parentSelected = 0;
            while (parentSelected < 2)
            {
                List<TimetableIndividual> tournamentPopulation = new List<TimetableIndividual>();
                for (int i = 0; i < tournamentSize; i++)
                {
                    int randomIndex = random.Next(population.Count);
                    tournamentPopulation.Add(population[randomIndex]);
                }

                tournamentPopulation = tournamentPopulation.OrderByDescending(t => t.Fitness).ToList();
                parents.Add(tournamentPopulation[0]);
                parentSelected++;
            }
            return parents;
        }

        // double internal
        private TimetableIndividual PerformCrossover(List<TimetableIndividual> parents)
        {
            // single-point crossover to generate offspring individual from parents

            if (random.NextDouble() < crossoverRate)
            {
                // perform crossover and return the child (offspring)
                TimetableIndividual offspring = new TimetableIndividual();
                offspring.Lectures = new List<Lecture>();
                int parentLecturesCount = parents[0].Lectures.Count;
                TimetableIndividual firstParent = parents[0];
                TimetableIndividual secondParent = parents[1];
                int randomIndex = random.Next(1, parentLecturesCount);

                for (int i = 0; i < randomIndex; i++)
                {
                    Lecture firstParentLecture = firstParent.Lectures[i];
                    Lecture secondParentLecture = secondParent.Lectures[i];

                    Lecture offspringLecture = new Lecture
                        (
                            firstParentLecture.Day,
                            firstParentLecture.Time,
                            firstParentLecture.ClassRoom,
                            firstParentLecture.Course
                        );
                    offspring.Lectures.Add(offspringLecture);
                }
                for (int i = randomIndex; i < parentLecturesCount; i++)
                {
                    Lecture firstParentLecture = firstParent.Lectures[i];
                    Lecture secondParentLecture = secondParent.Lectures[i];

                    Lecture offspringLecture = new Lecture
                        (
                            secondParentLecture.Day,
                            secondParentLecture.Time,
                            secondParentLecture.ClassRoom,
                            firstParentLecture.Course
                        );
                    offspring.Lectures.Add(offspringLecture);
                }
                return offspring;
            }

            // no crossover, return one of the parent randomly
            return random.NextDouble() < 0.5 ? parents[0] : parents[1];
        }

        // double internal
        private void PerformMutation(TimetableIndividual offspring)
        {
            // random gene mutation on offspring individuals
            for (int i = 0; i < offspring.Lectures.Count; i++)
            {
                // mutate three Gens (day, time, room)
                for (int j = 1; j <= 3; j++)
                {
                    if (random.NextDouble() < mutationRate)
                    {
                        switch (j)
                        {
                            case 1:
                                offspring.Lectures[i].Day = (byte)random.Next(1, 6);
                                break;
                            case 2:
                                offspring.Lectures[i].Time = new TimeOnly(random.Next(9, 17), 0);
                                break;
                            case 3:
                                offspring.Lectures[i].ClassRoom =
                                    DB.Instance.classRooms
                                    [
                                        random.Next(0, DB.Instance.classRooms.Count)
                                    ];
                                break;
                        }
                    }
                }

                //if (random.NextDouble() < mutationRate)
                //{
                //    offspring.Lectures[i].Day = (byte)random.Next(1, 6);
                //    offspring.Lectures[i].Time = new TimeOnly(random.Next(9, 17), 0);
                //    offspring.Lectures[i].ClassRoom =
                //        DB.Instance.classRooms
                //        [
                //            random.Next(0, DB.Instance.classRooms.Count)
                //        ];
                //}

            }
        }

    }
    public class Program
    {
        public static void Main()
        {
            GeneticAlgorithm ga = new GeneticAlgorithm(
                maxGenerations: 1000,
                populationSize: 100,
                tournamentSize: 5,
                crossoverRate: 0.7,
                mutationRate: 0.01
                );

            // Retrieve the best individual (timetables for all years)
            TimetableIndividual? bestIndividual = ga.Run();
            if (bestIndividual != null)
            {
                // Process and display the best timetable
                ProcessTimetable(bestIndividual);
            }
        }

        private static void ProcessTimetable(TimetableIndividual timetable)
        {
            var lectures = timetable.Lectures;

            // print all years timetable
            for (int currentYear = 1; currentYear <= 5; currentYear++)
            {
                // print horezintal line
                Console.WriteLine();
                Console.WriteLine();
                var rule = new Rule("Year " + currentYear + " Timetable");
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
                var currentYearLectures = lectures.Where(l => l.Course.Year == currentYear).OrderBy(l => l.Day).ThenBy(l => l.Time);
                foreach (var item in currentYearLectures)
                {
                    AnsiConsole.MarkupLineInterpolated($"[grey]{item.ToString()}[/]");
                    if (item.ClassRoom.Id == item.Course.preferredRoom.Id)
                    {
                        AnsiConsole.MarkupLineInterpolated($"[rgb(0,100,0)]Teacher Preferred to study this course in: {item.Course.preferredRoom.Name}[/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLineInterpolated($"[rgb(255,0,0)]Teacher Preferred to study this course in: {item.Course.preferredRoom.Name}[/]");
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
                    if (int.Parse(item.Time.ToString().Split(":")[0]) >= 9)
                    {
                        rowCellNumber = Math.Abs(int.Parse(item.Time.ToString().Split(":")[0]) - 9) * 2;
                    }
                    else
                    {
                        rowCellNumber = (Math.Abs(int.Parse(item.Time.ToString().Split(":")[0]) - 1) * 2) + 8;
                    }
                    table.UpdateCell(rowCellNumber, item.Day, item.Course.Name + "\n" + item.Course.Teacher.Name + "\n" + item.ClassRoom.Name);
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
                        if (item.Day == item2.Day && item.Time == item2.Time)
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
                        if (item.Day == item2.Day && item.Time == item2.Time && item.ClassRoom == item2.ClassRoom)
                        {
                            violation++;
                            AnsiConsole.MarkupLineInterpolated($"[red bold]{item.ToString() + " (Conf with other y 'y" + item2.Course.Year + "'), "}[/]");
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
            var rule2 = new Rule("Teacher Preferences");
            rule2.Style = Style.Parse("yellow bold");
            AnsiConsole.Write(rule2);



            // print all teachers preferences
            double totalHits = 0.0;
            double localHits = 0.0;
            double localNoHits = 0.0;
            double totalNoHits = 0.0;
            var teachers = DB.Instance.teachers;
            foreach (var teacher in teachers)
            {
                localHits = 0.0;
                localNoHits = 0.0;
                AnsiConsole.MarkupLineInterpolated($"[deeppink4_1 bold]{teacher.Name} preferences:[/]");

                if (teacher.ListOfPreferredDayTime != null)
                {
                    var teacherPreferences = teacher.ListOfPreferredDayTime.OrderBy(pdt => pdt.preferredDay).ThenBy(pdt => pdt.preferredTime);
                    foreach (var preference in teacherPreferences)
                    {
                        var currentTeacherLectures = lectures.Where(l => l.Course.Teacher.Id == teacher.Id).ToList();
                        var matchedLecture = currentTeacherLectures.Where(ctl => ctl.Day == preference.preferredDay && ctl.Time == preference.preferredTime).ToList();
                        if (matchedLecture.Count() != 0)
                        {
                            // MC: means Match Count
                            AnsiConsole.MarkupLineInterpolated($"[green]{preference.ToString() + " (MC:" + matchedLecture.Count + ", Y" + matchedLecture.First().Course.Year + ", " + matchedLecture.First().Course.Name})[/]");
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