﻿namespace Timetable.Application.Persistence.Repository
{
    public interface IYearRepository : IGenericRepository<Year>
    {
        public void Clear();
    }
}
