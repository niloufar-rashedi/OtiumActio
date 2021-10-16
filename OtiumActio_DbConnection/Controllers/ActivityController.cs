using Microsoft.AspNetCore.Mvc;
using OtiumActio.Models;
using System.Collections.Generic;
using OtiumActio.Helpers;
using OtiumActio.DAL;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtiumActio.Interfaces;
using System;

namespace OtiumActio.Controllers
{
    public class ActivityController : Controller
    {
        private IEditActivityController _service;
        public ActivityController(IEditActivityController service)
        {
            _service = service;
        }
        public IActionResult Index(string search, string sort, string startDate, string endDate)
        {
            DataAccessLayer adl = new DataAccessLayer();
            var allActivities = adl.Activities.ToList();

            if (!string.IsNullOrEmpty(search))
            {
                return View("Activity", allActivities.Where(d=>d.Description.Contains(search)));
            }
            if (!string.IsNullOrEmpty(sort))
            {
                return View("Activity", allActivities.OrderBy(c => c.CategoryName));
            }
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                DateTime sDate = Convert.ToDateTime(startDate);
                DateTime eDate = Convert.ToDateTime(endDate);
                return View("Activity", allActivities.Where(date => date.Date <= eDate && date.Date >= sDate).ToList());
            }
            return View("Activity", allActivities);
        }
        public IActionResult Delete(Activity activity)
        {
            DataAccessLayer adl = new DataAccessLayer();
            try
            {
                adl.DeleteActivity(activity.Id);
                TempData["Success"] = "Aktiviteten tas bort!";
            }
            catch (System.Exception)
            {
                ViewData["Error"] = "Oops! Försök igen senare!";
            }
            return View("_successMessage");
        }
        public void Update(Activity activity)
        {
            ///Activity/Update/30
            _service.Edit(activity.Id);
        }
    }
}
