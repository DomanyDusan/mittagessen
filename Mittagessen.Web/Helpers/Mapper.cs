using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mittagessen.Data.Entities;
using Mittagessen.Data.Interfaces;
using Mittagessen.Web.Areas.Sprava.Models;
using StructureMap;

namespace Mittagessen.Web.Helpers
{
    public static class Mapper
    {
        private static IEnrollmentRepository _enrollmentRepository;
        private static IEnrollmentRepository EnrollmentRepository
        {
            get { return _enrollmentRepository = _enrollmentRepository ?? ObjectFactory.GetInstance<IEnrollmentRepository>(); }
        }
        
        public static Lunch Map(LunchModel model, Lunch item)
        {
            item.LunchDate = model.LunchDate.Date + model.LunchTime;
            item.CookedMealId = model.CookedMealId;
            item.NumberOfPortions = model.NumberOfPortions;
            return item;
        }

        public static LunchModel Map(Lunch item, LunchModel model)
        {
            model.LunchId = item.Id;
            model.LunchDate = item.LunchDate.Date;
            model.LunchTime = item.LunchDate.TimeOfDay;
            model.CookedMealId = item.CookedMealId;
            model.CookedMealName = item.CookedMeal.Name;
            model.NumberOfPortions = item.NumberOfPortions;
            model.Enrollments = item.Enrollments.Select(e => new EnrollmentModel()
                {
                    UserName = e.EnrolledBy.Name,
                    VariationName = e.MealVariation != null ? e.MealVariation.Name : ""
                }).ToList();
            return model;
        }
    }
}