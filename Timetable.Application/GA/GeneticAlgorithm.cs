using Timetable.Application.Persistence.UnitOfWork;
using Timetable.Domain.Entities;
using Timetable.Domain.Enums.EntitiesEnums;

namespace Timetable.Application.GA
{
    /*-- How Would Be Called --*/
    //GeneticAlgorithm ga = new GeneticAlgorithm(
    //            maxGenerations: 1000,
    //            populationSize: 100,
    //            tournamentSize: 5,
    //            crossoverRate: 0.7,
    //            mutationRate: 0.01
    //            );


    public class TimetableIndividual
    {
        public List<Lecture> Lectures { get; set; } = null!;
        public double Fitness { get; set; }
    }

    public class GeneticAlgorithm
    {
        private readonly IUnitOfWork Uow;

        private List<Day> Days;
        private List<Time> Times;
        private List<Room> Rooms;
        private Semester semester;
        private List<TimetableIndividual> population;
        private Random random;

        private int maxGenerations;
        private int populationSize;
        private int tournamentSize;
        private double crossoverRate;
        private double mutationRate;

        public GeneticAlgorithm(int maxGenerations, int populationSize, int tournamentSize, double crossoverRate, double mutationRate, Semester semester, IUnitOfWork uow)
        {
            this.Uow = uow;

            this.population = new List<TimetableIndividual>();
            this.random = new Random();
            this.maxGenerations = maxGenerations;
            this.populationSize = populationSize;
            this.tournamentSize = tournamentSize;
            this.crossoverRate = crossoverRate;
            this.mutationRate = mutationRate;


            this.semester = semester;

            Days = uow.DayRepository.GetAll().ToList();
            Times = uow.TimeRepository.GetAll().ToList();
            Rooms = uow.RoomRepository.GetAll().ToList();

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
            // ----------
            var SemesterTheoryCourses = Uow.CourseRepository.Find(course => course.semester.SemesterNo == this.semester.SemesterNo && course.Type == CourseTypeEnum.TheoryCourse && course.user != null).ToList();
            // ----------
            for (int i = 0; i < SemesterTheoryCourses.Count; i++)
            {
                int randomDayIndex = random.Next(0, Days.Count());
                Day randomDay = Days[randomDayIndex];

                int randomTimeIndex = random.Next(0, Times.Count());
                Time randomTime = Times[randomTimeIndex];

                int randomRoomIndex = random.Next(0, Rooms.Count());
                Room randomRoom = Rooms[randomRoomIndex];

                Lecture randomLecture = new Lecture
                {
                    day = randomDay,
                    Time = randomTime,
                    Room = randomRoom,
                    course = SemesterTheoryCourses[i],
                    Type = LectureTypeEnum.TheoryLecture
                };

                lectures.Add(randomLecture);

                if (SemesterTheoryCourses[i].LuctureNumPerWeek == 2)
                {
                    Lecture randomLecture2 = new Lecture
                    {
                        day = randomDay,
                        Time = randomTime,
                        Room = randomRoom,
                        course = SemesterTheoryCourses[i],
                        Type = LectureTypeEnum.TheoryLecture
                    };
                    lectures.Add(randomLecture2);
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

            for (int i = 0; i < individual.Lectures.Count(); i++)
            {
                for (int j = i + 1; j < individual.Lectures.Count(); j++)
                {
                    var i1 = individual.Lectures[i];
                    var i2 = individual.Lectures[j];

                    // conflict with lectures times and rooms
                    if (
                        i1.day.DayNo == i2.day.DayNo &&
                        i1.Time.Id == i2.Time.Id &&
                        i1.Room.Id == i2.Room.Id
                       )
                    {
                        conflict++;
                    }
                    // conflict within same year lectures times
                    if (
                        i1.course.year.YearNo == i2.course.year.YearNo &&
                        i1.day.DayNo == i2.day.DayNo &&
                        i1.Time.Id == i2.Time.Id
                       )
                    {
                        conflict++;
                    }
                    // conflict same teacher have two lectures at the same day and time
                    if (
                        i1.course.user.Id == i2.course.user.Id &&
                        i1.day.DayNo == i2.day.DayNo &&
                        i1.Time.Id == i2.Room.Id
                       )
                    {
                        conflict++;
                    }
                }

                // Courses with two lectures per week must satisfy this constraint
                if (
                        individual.Lectures[i].course.LuctureNumPerWeek == 2 &&
                        individual.Lectures.Where
                        (
                            lec => lec.course.Id == individual.Lectures[i].course.Id
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

                if (individual.Lectures[i].course.user.Preferences.Where(pdt => pdt.day.DayNo == individual.Lectures[i].day.DayNo && pdt.time.Id == individual.Lectures[i].Time.Id).Count() != 0)
                {
                    preferredDayTime++;
                }
                if (individual.Lectures[i].Room.Id == individual.Lectures[i].course?.TeacherpreferredRoom?.Id)
                {
                    preferredRoom++;
                }
            }
            //Console.WriteLine(preferredDayTime + "/" + individual.Lectures.Count);
            //Console.WriteLine();
            preferredDayTime = preferredDayTime / individual.Lectures.Count;
            preferredRoom = preferredRoom / individual.Lectures.Count;

            // from 300 (Total = 300)
            //double fitness = ((Math.Log(Math.Pow(conflict, -1)) + 0.1) * 1000) + (preferredDayTime * 100) + (preferredRoom * 100);

            double fitness = ((Math.Log(Math.Pow(conflict, -1)) + 0.1) * 1000) * Math.Exp((preferredRoom + preferredDayTime) * 10);
            Console.WriteLine(fitness + "\t" + conflict + "\t" + preferredDayTime + "\t" + preferredRoom);
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

                    Lecture offspringLecture = new Lecture
                    {
                        day = firstParentLecture.day,
                        Time = firstParentLecture.Time,
                        Room = firstParentLecture.Room,
                        course = firstParentLecture.course,
                    };

                    offspring.Lectures.Add(offspringLecture);
                }
                for (int i = randomIndex; i < parentLecturesCount; i++)
                {
                    Lecture firstParentLecture = firstParent.Lectures[i];
                    Lecture secondParentLecture = secondParent.Lectures[i];

                    Lecture offspringLecture = new Lecture
                    {
                        day = secondParentLecture.day,
                        Time = secondParentLecture.Time,
                        Room = secondParentLecture.Room,
                        course = firstParentLecture.course,
                    };

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
                                int randomDayIndex = random.Next(0, Days.Count());
                                Day randomDay = Days[randomDayIndex];
                                offspring.Lectures[i].day = randomDay;
                                break;
                            case 2:
                                int randomTimeIndex = random.Next(0, Times.Count());
                                Time randomTime = Times[randomTimeIndex];
                                offspring.Lectures[i].Time = randomTime;
                                break;
                            case 3:
                                int randomRoomIndex = random.Next(0, Rooms.Count());
                                Room randomRoom = Rooms[randomRoomIndex];
                                offspring.Lectures[i].Room = randomRoom;
                                break;
                        }
                    }
                }
            }
        }


    }
}
