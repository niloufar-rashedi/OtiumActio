using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using OtiumActio.Models;

namespace OtiumActio.Controllers
{
    [Authorize]
    public class SuggestedActivityController : Controller
    {
        private readonly IRepository<Activity> _repository;
        private readonly OtiumActioContext _context;

        public SuggestedActivityController(IRepository<Activity> repository, OtiumActioContext context)
        {
            _repository = repository;
            _context = context;
        }
        public IActionResult Index()
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            var allCategories = _context.Categories.Select(c => c).ToList();
            foreach (var category in allCategories)
            {
                categories.Add(new SelectListItem { Text = category.CatName.ToString(), Value = category.CatId.ToString() });
            }
            ViewData["SelectableCategories"] = categories;
            return View();
        }
        [HttpPost]
        public IActionResult AddNewActivity(Activity activity)
        {
            if (ModelState.IsValid)
            {
                _repository.AddNewActivity(activity);
                ModelState.Clear();
            }
            List<SelectListItem> categories = new List<SelectListItem>();
            var allCategories = _context.Categories.Select(c => c).ToList();
            foreach (var category in allCategories)
            {
                categories.Add(new SelectListItem { Text = category.CatName.ToString(), Value = category.CatId.ToString() });
            }
            ViewData["SelectableCategories"] = categories;

            //return View("SuggestedActivity");
            return View("Index");
        }
    }
}

