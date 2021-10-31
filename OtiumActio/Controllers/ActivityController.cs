using Microsoft.AspNetCore.Mvc;
using OtiumActio.Domain.Interfaces;
using System.Threading.Tasks;
using OtiumActio.Models;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using OtiumActio.Infrastructure;
using System.Linq;
using OtiumActio.EmailService;
using OtiumActio.Interfaces;
using Microsoft.AspNetCore.Http;

namespace OtiumActio.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IRepository<Activity> _repository;
        private readonly OtiumActioContext _context;
        //private readonly ITokenService _tokenService;
        private IEditActivityController _service;
        public ActivityController(IRepository<Activity> repository, OtiumActioContext context, IEditActivityController service)
        {
            _repository = repository;
            _context = context;
            _service = service;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var model = _repository.GetAll();
            //var message = new Message(new string[] { "otiumactio@gmail.com" }, "Test email", "This is the content from our email.");

            return View("Activity", model);
        }
        public IActionResult Delete(Activity activity)
        {
            var deletecActivity = _context.Activities.Where(a => a.AcId == activity.AcId).Select(a => a.AcDescription).FirstOrDefault();
            _service.Delete(activity.AcId);
            var sessionValue = $"Du har nyss tagit bort aktiviteten |{deletecActivity}|!";
            var sessionName = "SuccessDelete";
            HttpContext.Session.SetString(sessionName, sessionValue);
            TempData["DeletedActivity"] = HttpContext.Session.GetString(sessionName);
            TempData.Keep();
            return RedirectToAction("Index", "Home");

            // return View("_successMessage");
        }
        public void Update(Activity activity)
        {
            ///Activity/Update/30
            _service.Edit(activity.AcId);
        }
        [HttpGet]
        public PartialViewResult GetSelectedActivities()
        {
            var model = _context.Activities.Where(a => a.AcDate > DateTime.Now).Select(a=>a).Take(3);
            //Response.Cookies.Append("Cookie", "listFetched");
            return PartialView("_selectedActivities", model);
        }
        [HttpGet]
        public PartialViewResult GetAllActivities()
        {
            var model = _context.Activities.Select(a => a).ToList();
            //Response.Cookies.Append("Cookie", "listFetched");
            return PartialView("_selectedActivities", model);
        }

        public string Get(string key)
        {
            return Request.Cookies[key];
        }
        [HttpGet]
        public IActionResult GetActivityById(int id)
        {
            var result = _repository.GetById(id);
            //Response.Cookies.Append("Cookie", "listFetched");
            return View("Details", result);
        }
        #region Weather method that communicates with IDS4
        //[Authorize]
        //public async Task<IActionResult> Weather()
        //{
        //    var data = new List<WeatherData>();
        //    using (var client = new HttpClient())
        //    {
        //        var tokenResponse = await _tokenService.GetToken("otiumactio.read");
        //        client.SetBearerToken(tokenResponse.AccessToken);

        //        var result = client.GetAsync("https://localhost:44383/weatherforecast").Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            var model = result.Content.ReadAsStringAsync().Result;
        //            data = JsonConvert.DeserializeObject<List<WeatherData>>(model);
        //            return View(data);
        //        }
        //        else
        //        {
        //            throw new Exception("unable to get content!");
        //        }

        //    }
        //}
        #endregion

    }
}
