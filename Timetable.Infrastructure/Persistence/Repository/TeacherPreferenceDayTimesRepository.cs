using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable.Application.Persistence.Repository;

namespace Timetable.Infrastructure.Persistence.Repository
{
    public class TeacherPreferenceDayTimesRepository : GenericRepository<TeacherPreferenceDayTime>, ITeacherPreferenceDayTimesRepository
    {
        public TeacherPreferenceDayTimesRepository(AppDbContext context) : base(context)
        {
        }
    }
}
