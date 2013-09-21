using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Mittagessen.Data.Entities
{
    public class Meal : EntityBase
    {
        [DisplayName("Nazov")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Popis")]
        public string Description { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Suroviny")]
        public string Ingredients { get; set; }
        [UIHint("Image")]
        [DisplayName("Obrazok")]
        public string ImageName { get; set; }
        [HiddenInput(DisplayValue=false)]
        public double AverageRating { get; set; }
        [ScaffoldColumn(false)]
        public double AverageRatingRounded
        {
            get { return Math.Round(AverageRating,1); }
        }
    }
}
