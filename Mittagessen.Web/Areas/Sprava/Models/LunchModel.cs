using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mittagessen.Data.Entities;

namespace Mittagessen.Web.Areas.Sprava.Models
{
    public class LunchModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid LunchId { get; set; }
        [DisplayName("Datum obeda")]
        public DateTime LunchDate { get; set; }
        [DisplayName("Cas obeda")]
        public TimeSpan LunchTime { get; set; }
        [DisplayName("Pocet porcii")]
        public int NumberOfPortions { get; set; }
        public Guid CookedMealId { get; set; }
        public string CookedMealName { get; set; }
        public IList<EnrollmentModel> Enrollments { get; set; }
    }
}