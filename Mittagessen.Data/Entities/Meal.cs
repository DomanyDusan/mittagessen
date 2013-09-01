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
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.MultilineText)]
        public string Ingredients { get; set; }
        [UIHint("Image")]
        public string ImageName { get; set; }
    }
}
