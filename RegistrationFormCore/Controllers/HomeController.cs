using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            var umodel = new UserDataModel();
            var users = umodel.GetAll();

            return View(users);
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
        public async Task<IActionResult> SubmitRegistration(UserDataModel model)
        {

            var umodel = new UserDataModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                Address1 = model.Address1,
                Address2 = model.Address2,
                City = model.City,
                State = model.State,
                ZipCode = model.ZipCode,
                Birthday = model.Birthday,
                AdditionalInfo = model.AdditionalInfo
            };

            using (var memoryStream = new MemoryStream())
            {
                await model.DLPhoto.CopyToAsync(memoryStream);
                umodel.DLPhotoBytes = memoryStream.ToArray();
            }

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
