using Microsoft.AspNetCore.Mvc;
using OtiumActio.Domain.Interfaces;
using OtiumActio.Domain.Activities;
using System.Threading.Tasks;
using OtiumActio.Models;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;

namespace OtiumActio.Controllers
{
    public class ActivityController : Controller
    {
        //private IEditActivityController _service;
        private readonly IRepository<Activity> _repository;
        private readonly ITokenService _tokenService;
        //public ActivityController(IEditActivityController service)
        //{
        //    _service = service;
        //}
        public ActivityController(IRepository<Activity> repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }
        //public IActionResult Index(string search, string sort, string startDate, string endDate)
        //{
        //DataAccessLayer adl = new DataAccessLayer();
        //var allActivities = adl.Activities.ToList();

        //if (!string.IsNullOrEmpty(search))
        //{
        //    return View("Activity", allActivities.Where(d=>d.Description.Contains(search)));
        //}
        //if (!string.IsNullOrEmpty(sort))
        //{
        //    return View("Activity", allActivities.OrderBy(c => c.CategoryName));
        //}
        //if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
        //{
        //    DateTime sDate = Convert.ToDateTime(startDate);
        //    DateTime eDate = Convert.ToDateTime(endDate);
        //    return View("Activity", allActivities.Where(date => date.Date <= eDate && date.Date >= sDate).ToList());
        //}
        //return View();
        //View("Activity", allActivities);
        //}
        [HttpGet]
        public ActionResult Index()
        {
            var model = _repository.GetAll();
            return View("Activity", model);
        }
        [Authorize]
        public async Task<IActionResult> Weather()
        {
            var data = new List<WeatherData>();
            using (var client = new HttpClient())
            {
                var tokenResponse = await _tokenService.GetToken("otiumactio.read");
                client.SetBearerToken(tokenResponse.AccessToken);

                var result = client.GetAsync("https://localhost:44383/weatherforecast").Result;
                if (result.IsSuccessStatusCode)
                {
                    var model = result.Content.ReadAsStringAsync().Result;
                    data = JsonConvert.DeserializeObject<List<WeatherData>>(model);
                    return View(data);
                }
                else
                {
                    throw new Exception("unable to get content!");
                }

            }
        }
        public IActionResult Delete(Activity activity)
        {
            //DataAccessLayer adl = new DataAccessLayer();
            //try
            //{
            //    adl.DeleteActivity(activity.Id);
            //    TempData["Success"] = "Aktiviteten tas bort!";
            //}
            //catch (System.Exception)
            //{
            //    ViewData["Error"] = "Oops! Försök igen senare!";
            //}
            return View("_successMessage");
        }
        public void Update(Activity activity)
        {
            ///Activity/Update/30
            //_service.Edit(activity.AcId);
        }
    }
}
