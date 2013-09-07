﻿using System;
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

namespace Mittagessen.Web.Areas.Sprava.Controllers
{
    [Authorize(Roles="admin")]
    public class MealController : Controller
    {
        [SetterProperty]
        public ISimpleRepository<Meal> MealRepository { get; set; }

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
        public ActionResult Create(Meal meal, HttpPostedFileBase file)
        {
            SaveMealImage(meal, file);

            MealRepository.Insert(meal);
            return RedirectToAction("Index");
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
            SaveMealImage(meal, file);
            MealRepository.Update(meal);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var meal = MealRepository.Get(id);
            DeleteExistingImage(meal);
            MealRepository.Delete(meal);
            return RedirectToAction("Index");
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
