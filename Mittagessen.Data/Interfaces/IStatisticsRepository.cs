using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Models;

namespace Mittagessen.Data.Interfaces
{
    public interface IStatisticsRepository
    {
        IEnumerable<Attendance> GetUserAttendance(DateTime fromDate, DateTime toDate);
    }
}
