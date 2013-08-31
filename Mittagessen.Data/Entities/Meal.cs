using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Mittagessen.Data.Entities
{
    public class Meal : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        [UIHint("FileUpload")]
        public string ImageName { get; set; }
    }
}
