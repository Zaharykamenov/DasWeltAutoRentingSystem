using CarRentingSystem.Areas.Admin.Constants;
using CarRentingSystem.Areas.Identity.Pages.Account;
using CarRentingSystem.Constants;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Extensions;
using CarRentingSystem.Core.Models.Car;
using CarRentingSystem.Extensions;
using CarRentingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarRentingSystem.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly ICarService carService;
        private readonly IAgentService agentService;

        public CarController(ICarService carService, IAgentService agentService)
        {
            this.carService = carService;
            this.agentService = agentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllCarsQueryModel allCarsQueryModel)
        {
            var result = await this.carService.All(
                allCarsQueryModel.Category,
                allCarsQueryModel.SearchTerm,
                allCarsQueryModel.Sorting,
                allCarsQueryModel.CurrentPage,
                AllCarsQueryModel.CarsPerPage);


            allCarsQueryModel.TotalCarsCount = result.TotalCarsCount;
            allCarsQueryModel.Categories = await this.carService.AllCategoriesNames();
            allCarsQueryModel.Cars = result.Cars;

            return View(allCarsQueryModel);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            if (this.User.IsInRole(AdminConstants.AdminRoleName))
            {
                return RedirectToAction("Mine", "Car", new { area = "Admin" });
            }

            IEnumerable<CarServiceModel> myCars;

            var userId = User.Id();

            if ((await this.agentService.ExistById(userId)))
            {
                int agentId = await this.agentService.GetAgentId(userId);

                myCars = await this.carService.AllCarsByAgentId(agentId);
            }
            else
            {
                myCars = await this.carService.AllCarsByUserId(userId);
            }

            return View(myCars);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id, string information)
        {
            if ((await this.carService.CarExist(id) == false))
            {
                TempData[AgentControllerConstants.ErrorMessage] = CarControllerConstants.CarDoesNotExistError;
                return RedirectToAction(nameof(Mine));
                //return BadRequest();
            }

            CarDetailsModel model = await this.carService.CarDetailsById(id);


            //if (information != model.GetInformation())
            //{
            //    TempData[AgentControllerConstants.ErrorMessage] = CarControllerConstants.CarDetailsNoPermisionError;
            //}

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //дали си агент ?
            if (await agentService.ExistById(User.Id()) == false)
            {
                TempData[AgentControllerConstants.ErrorMessage] = CarControllerConstants.AgentDoesNotExistError;
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            var model = new CarModel()
            {
                CarCategories = await this.carService.AllCategories()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CarModel model)
        {
            //дали си агент ?
            if (await this.agentService.ExistById(User.Id()) == false)
            {
                TempData[AgentControllerConstants.SuccessMessage] = CarControllerConstants.AgentDoesNotExistError;
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            if ((await this.carService.CategoryExist(model.EngineCategoryId) == false))
            {
                ModelState.AddModelError(nameof(model.EngineCategoryId), CarControllerConstants.CarControllerCategoryNotExistError);
            }

            if (!ModelState.IsValid)
            {
                model.CarCategories = await carService.AllCategories();

                return View(model);
            }
            int agentId = await agentService.GetAgentId(User.Id());

            int id = await carService.Create(model, agentId);

            return RedirectToAction(nameof(Details), new { id = id, information = model.GetInformation() });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if ((await carService.CarExist(id)) == false)
            {
                TempData[AgentControllerConstants.ErrorMessage] = CarControllerConstants.CarDoesNotExistError;
                return RedirectToAction(nameof(All));
            }

            if ((await carService.HasAgentWithId(id, User.Id())) == false && !User.IsAdmin())
            {
                TempData[AgentControllerConstants.ErrorMessage] = CarControllerConstants.CarDetailsNoPermisionError;
                return RedirectToAction(nameof(All));
                //return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            var car = await carService.CarDetailsById(id);
            var carCategoryId = await carService.GetCarCategoryId(id);

            var model = new CarModel()
            {
                Id = car.Id,
                Address = car.Address,
                EngineCategoryId = carCategoryId,
                Description = car.Description,
                CarCategories = await this.carService.AllCategories(),
                ImageUrl = car.ImageUrl,
                PricePerMonth = car.PricePerMonth,
                Title = car.Title
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarModel carModel)
        {
            if ((await carService.CarExist(carModel.Id)) == false)
            {
                ModelState.AddModelError("", "Car doesn`t exist!");
                carModel.CarCategories = await this.carService.AllCategories();
                return View(carModel);
            }

            if ((await carService.HasAgentWithId(carModel.Id, User.Id())) == false && !User.IsAdmin())
            {
                TempData[AgentControllerConstants.ErrorMessage] = CarControllerConstants.CarDetailsNoPermisionError;
                return RedirectToAction(nameof(All));
                //return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if ((await this.carService.CategoryExist(carModel.EngineCategoryId)) == false)
            {
                ModelState.AddModelError(nameof(carModel.EngineCategoryId), "Engine category doesn`t exist!");
                carModel.CarCategories = await this.carService.AllCategories();
                return View(carModel);
            }

            if (!ModelState.IsValid)
            {
                carModel.CarCategories = await this.carService.AllCategories();
                return View(carModel);
            }

            await this.carService.CarEdit(carModel.Id, carModel);

            TempData[AgentControllerConstants.SuccessMessage] = CarControllerConstants.CarEditedSuccessfully;
            return RedirectToAction(nameof(Details), new { id = carModel.Id, information = carModel.GetInformation() });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if ((await carService.CarExist(id)) == false)
            {
                TempData[AgentControllerConstants.ErrorMessage] = CarControllerConstants.CarDoesNotExistError;
                return RedirectToAction(nameof(All));
            }

            if ((await carService.HasAgentWithId(id, User.Id())) == false && !User.IsAdmin())
            {
                TempData[AgentControllerConstants.ErrorMessage] = CarControllerConstants.CarDetailsNoPermisionError;
                return RedirectToAction(nameof(All));
            }

            var car = await carService.CarDetailsById(id);

            var carDeleteModel = new CarDeleteModel()
            {
                Address = car.Address,
                ImageUrl = car.ImageUrl,
                Title = car.Title
            };

            return View(carDeleteModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CarDeleteModel carDeleteModel)
        {
            if ((await carService.CarExist(id)) == false)
            {
                TempData[AgentControllerConstants.ErrorMessage] = CarControllerConstants.CarDoesNotExistError;
                return RedirectToAction(nameof(All));
            }

            if ((await carService.HasAgentWithId(id, User.Id())) == false && !User.IsAdmin())
            {
                TempData[AgentControllerConstants.ErrorMessage] = CarControllerConstants.AgentDoesNotExistError;
                return RedirectToAction(nameof(All));
                //return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            await this.carService.DeleteCar(id);


            TempData[AgentControllerConstants.SuccessMessage] = CarControllerConstants.CarDeletedSuccessfully;
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            if ((await carService.CarExist(id)) == false)
            {
                TempData[AgentControllerConstants.ErrorMessage] = CarControllerConstants.CarDoesNotExistError;
                return RedirectToAction(nameof(All));
            }

            if ((await agentService.ExistById(User.Id())) && !User.IsAdmin())
            {
                TempData[AgentControllerConstants.ErrorMessage] = CarControllerConstants.AgentDoesNotExistError;
                return RedirectToAction(nameof(All));
                //return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if ((await this.carService.IsRented(id)))
            {
                TempData[AgentControllerConstants.ErrorMessage] = CarControllerConstants.CarIsAlreadyRented;
                return RedirectToAction(nameof(All));
            }

            await this.carService.RentCar(id, User.Id());

            TempData[AgentControllerConstants.SuccessMessage] = CarControllerConstants.CarRentedSuccessfully;
            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            if ((await carService.CarExist(id)) == false)
            {
                TempData[AgentControllerConstants.ErrorMessage] = CarControllerConstants.CarDoesNotExistError;
                return RedirectToAction(nameof(All));
            }

            if ((await this.carService.IsRented(id)) == false)
            {
                TempData[AgentControllerConstants.ErrorMessage] = CarControllerConstants.AgentDoesNotExistError;
                return RedirectToAction(nameof(All));
            }

            if ((await this.carService.IsRentedByUserWithId(id, User.Id())) == false)
            {
                TempData[AgentControllerConstants.ErrorMessage] = CarControllerConstants.CarIsAlreadyRented;
                return RedirectToAction(nameof(All));
                //return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            await this.carService.LeaveCar(id);

            TempData[AgentControllerConstants.SuccessMessage] = CarControllerConstants.CarLeavedSuccessfully;
            return RedirectToAction(nameof(Mine));
        }
    }
}
