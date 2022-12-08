using CarRentingSystem.Areas.Admin.Constants;
using CarRentingSystem.Constants;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.EngineCategory;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Areas.Admin.Controllers
{
    public class EngineCategoryController : AdminController
    {
        private readonly IEngineCategoryService engineCategoryService;

        public EngineCategoryController(IEngineCategoryService engineCategoryService)
        {
            this.engineCategoryService = engineCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> All(string searchString)
        {
            var model = await this.engineCategoryService.GetAllEngineCategory();

            if (!String.IsNullOrEmpty(searchString))
            {
                model = model
                    .Where(x => x.Fuel.ToLower().Contains(searchString.ToLower()) 
                                    || x.Description.ToLower().Contains(searchString.ToLower()));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (!(await this.engineCategoryService.IsExistEngineCategoryById(id)))
            {
                TempData[AgentControllerConstants.ErrorMessage] = EngineCategoryControllerConstants.MissingEngineCategory;
                return RedirectToAction(nameof(All));
            }

            var model = await this.engineCategoryService.GetEngineCategoryById(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new EngineCategoryCreateModel();
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EngineCategoryCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", EngineCategoryControllerConstants.WrongEngineCategoryModel);

                return View(model);
            }

            int id = await this.engineCategoryService.CreateEngineCategory(model);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.engineCategoryService.GetEngineCategoryById(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EngineCategoryDetailsModel model)
        {
            if ((await this.engineCategoryService.IsExistEngineCategoryById(model.Id)) == false)
            {
                ModelState.AddModelError("", "Engine Category doesn`t exist!");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.engineCategoryService.UpdateEngineCategory(model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await this.engineCategoryService.GetEngineCategoryById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EngineCategoryDetailsModel model)
        {
            await this.engineCategoryService.DeleteEngineCategory(model.Id);
            return RedirectToAction(nameof(All));
        }

    }
}
