using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtiumActio.Models;
using OtiumActio.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;

namespace OtiumActio.Controllers
{
    public class EditActivityController : Controller, IEditActivityController
    {
        private readonly IRepository<Activity> _repository;
        private readonly OtiumActioContext _context;
        public EditActivityController(IRepository<Activity> repository, OtiumActioContext context)
        {
            _repository = repository;
            _context = context;
        }

        [Route("Activity/Update/{id}")]
        public IActionResult Edit(int id)
        {
            var activityById = _repository.GetById(id);
            List<SelectListItem> categories = new List<SelectListItem>();
            var allCategories = _context.Categories.Select(c => c).ToList();
            foreach (var category in allCategories)
            {
                categories.Add(new SelectListItem { Text = category.CatName.ToString(), Value = category.CatId.ToString() });
            }
            ViewData["SelectableCategories"] = categories;

            return View("EditActivity", activityById);
        }
        public IActionResult UpdateActivity(Activity activity)
        {
            if (!ModelState.IsValid)
            {
                return View("EditActivity");
            }
            _repository.UpdateActivity(activity);
            ModelState.Clear();
            List<SelectListItem> categories = new List<SelectListItem>();
            var allCategories = _context.Categories.Select(c => c).ToList();
            foreach (var category in allCategories)
            {
                categories.Add(new SelectListItem { Text = category.CatName.ToString(), Value = category.CatId.ToString() });
            }
            ViewData["SelectableCategories"] = categories;
            TempData["Success"] = "Aktiviteten redigerades!";


            return View("_successMessage");
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
