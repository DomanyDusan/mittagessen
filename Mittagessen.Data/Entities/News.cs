using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mittagessen.Data.Entities
{
    public class News : EntityBase
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Text { get; set; }
        public bool IsRepeatable { get; set; }
    }
}
