using CarRentingSystem.Areas.Admin.Constants;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Extensions;
using CarRentingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarRentingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService carService;
        private readonly IErrorLogService errorLogService;

        public HomeController(ICarService carService, IErrorLogService errorLogService)
        {
            this.carService = carService;
            this.errorLogService = errorLogService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                if (this.User.IsInRole(AdminConstants.AdminRoleName))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }

                var model = await carService.LastThreeCars();

                return View(model);
            }
            catch (Exception ex)
            {
                await this.errorLogService.SaveError(ex, User.Id());
                return RedirectToAction("Error", "Home");
            }

        }

        public IActionResult Error()
        {
            return View();
        }
    }
}