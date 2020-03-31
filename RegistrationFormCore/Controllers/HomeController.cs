using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RegistrationFormCore.Models;

namespace RegistrationFormCore.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Profile()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetDetails()
        {
            UserDataModel umodel = new UserDataModel();
            umodel.Name = HttpContext.Request.Form["txtName"].ToString();
            umodel.Age = Convert.ToInt32(HttpContext.Request.Form["txtAge"]);
            umodel.City = HttpContext.Request.Form["txtCity"].ToString();
            int result = umodel.SaveDetails();
            if (result > 0)
            {
                ViewBag.Result = "Data Saved Successfully";
            }
            else
            {
                ViewBag.Result = "Something Went Wrong";
            }
            return View("Profile");
        }
    }
}
