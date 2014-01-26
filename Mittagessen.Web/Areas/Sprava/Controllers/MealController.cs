using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap.Attributes;
using Mittagessen.Data.Interfaces;
using Mittagessen.Data.Entities;
using System.IO;
using System.Configuration;
using Mittagessen.Web.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Mittagessen.Web.Areas.Sprava.Controllers
{
    [Authorize(Roles="admin")]
    public class MealController : Controller
    {
        [SetterProperty]
        public IMealRepository MealRepository { get; set; }

        public ActionResult Index()
        {
            var meals = MealRepository.GetAll();
            return View(meals);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Meal());
        }

        [HttpPost]
        public ActionResult Create(Meal meal, [Required] HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                SaveMealImage(meal, file);

                MealRepository.Insert(meal);
                return RedirectToAction("Index");
            }
            else
            {
                return View(meal);
            }
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var meal = MealRepository.Get(id);
            return View(meal);
        }

        [HttpPost]
        public ActionResult Edit(Meal meal, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                SaveMealImage(meal, file);
                MealRepository.Update(meal);
                return RedirectToAction("Index");
            }
            else
            {
                return View(meal);
            }
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var meal = MealRepository.Get(id);
            DeleteExistingImage(meal);
            MealRepository.Delete(meal);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddVariation(Guid mealId)
        {
            var variation = new MealVariation()
            {
                MealId = mealId
            };
            return View(variation);
        }

        [HttpPost]
        public ActionResult AddVariation(MealVariation variation)
        {
            MealRepository.AddVariation(variation.MealId, variation.Name, variation.RequiresDeadLine);

            return RedirectToAction("Edit", new { id = variation.MealId });
        }

        [HttpGet]
        public ActionResult RemoveVariation(Guid mealId, Guid variationId)
        {
            MealRepository.RemoveVariation(variationId);
            return RedirectToAction("Edit", new { id = mealId });
        }

        [HttpPost]
        public void UpdateVariation(Guid variationId, bool requiresDeadline)
        {
            MealRepository.UpdateVariation(variationId, requiresDeadline);
        }

        private void SaveMealImage(Meal meal, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                DeleteExistingImage(meal);

                var fileName = Guid.NewGuid().ToString().Replace('-', '_') + ".jpg";

                meal.ImageName = this.Url.Content("~" + ConfigurationManager.AppSettings["UploadsPath"] + "/" + fileName);

                var path = Path.Combine(Server.MapPath("~" + meal.ImageName));
                file.SaveAs(path);                
            }
        }

        private void DeleteExistingImage(Meal meal)
        {
            if (!string.IsNullOrEmpty(meal.ImageName))
            {
                var oldImgPath = Server.MapPath("~" + meal.ImageName);
                if (System.IO.File.Exists(oldImgPath))
                    System.IO.File.Delete(oldImgPath);
            }
        }
    }
}
