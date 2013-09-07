﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mittagessen.Data.Entities;

namespace Mittagessen.Data.Interfaces
{
    public interface IEnrollmentRepository : ISimpleRepository<Enrollment>
    {
        Enrollment Get(Guid userId, Guid lunchId);
        bool TryInsert(Enrollment enrollment);
    }
}