using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyVacationsProject.Models;
using System;
using System.Diagnostics;
using System.Globalization;
using Vacations_DAL;
using Vacations_DomainModel.Models.Identity;

namespace MyVacationsProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {

            _logger = logger;
            this._userManager = userManager;
            this._roleManager = roleManager;
            
        }

        public IActionResult Index()
        {
            if (this.User==null)
                return View();
            string _userId= _userManager.GetUserId(this.User);

            Redirect ("Views/Vacations/Index");
            string _month = DateTimeFormatInfo.CurrentInfo.MonthNames[DateTime.Now.Month - 1];
            return RedirectToAction(nameof(VacationsController.Index), "Vacations", new {month=_month, year=DateTime.Now.Year});
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
    }
}
