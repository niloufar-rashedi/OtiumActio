using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OtiumActio.Models;
using System.Security.Claims;
using System.Web;

namespace OtiumActio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //DataAccessLayer adl = new DataAccessLayer();
            //List<Category> cats = adl.Categories.ToList();
            //return View(cats);
            //var claimsIdentity = this.User.Identity as ClaimsIdentity;
            ////var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            //var userName = string.Empty;
            ////if (claimsIdentity is null)
            ////{
            //    userName = claimsIdentity.FindFirst(ClaimTypes.Name);
            ////}

            return View();

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return View();
        }
    }
}
