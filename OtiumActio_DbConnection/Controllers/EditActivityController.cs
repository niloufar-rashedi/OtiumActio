﻿using Microsoft.AspNetCore.Mvc;
using OtiumActio.Models;
using System.Collections.Generic;
using OtiumActio.Helpers;
using OtiumActio.DAL;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtiumActio.Interfaces;

namespace OtiumActio.Controllers
{
    public class EditActivityController : Controller, IEditActivityController
    {
        [Route("Activity/Update/{id}")]
        public IActionResult Edit(int id)
        {
            DataAccessLayer adl = new DataAccessLayer();
            var activity = adl.Activities.Where(a => a.Id == id).FirstOrDefault();

            var allCategories = adl.Categories.ToList();
            List<SelectListItem> categories = new List<SelectListItem>();
            foreach (var category in allCategories)
            {
                categories.Add(new SelectListItem { Text = category.Name.ToString(), Value = category.Id.ToString() });
            }
            ViewData["SelectableCategories"] = categories;

            return View("EditActivity", activity);
            //return View("EditActivity");
        }
        public IActionResult UpdateActivity(Activity activity)
        {

            DataAccessLayer adl = new DataAccessLayer();
            //var updatedActivity = new Activity
            //{
            //    Id = activity.Id,
            //    Category = (int)activity.Category,
            //    Description = activity.Description,
            //    Participants = activity.Participants,
            //    Date = activity.Date

            //};
            try
            {
                adl.UpdateActivity(activity);
                TempData["Success"] = "Aktiviteten redigerades!";

            }
            catch (System.Exception)
            {
                ViewData["Error"] = "Oops! Försök igen senare!";
            }
            //ModelState.Clear();
            var allCategories = adl.Categories.ToList();
            List<SelectListItem> categories = new List<SelectListItem>();
            foreach (var category in allCategories)
            {
                categories.Add(new SelectListItem { Text = category.Name.ToString(), Value = category.Id.ToString() });
            }
            ViewData["SelectableCategories"] = categories;

            return View("_successMessage");

        }

    }
}
