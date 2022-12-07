﻿using CarRentingSystem.Constants;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.Agent;
using CarRentingSystem.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Controllers
{
    [Authorize]
    public class AgentController : Controller
    {
        private readonly IAgentService agentService;
        private readonly IUserService userService;

        public AgentController(IAgentService agentService, IUserService userService)
        {
            this.agentService = agentService;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            if (await agentService.ExistById(User.Id()))
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new BecomeAgentModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentModel model)
        {
            var userId = User.Id();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await agentService.ExistById(userId))
            {
                TempData[AgentControllerConstants.ErrorMessage] = AgentControllerConstants.UserWithPhoneNumberExistError;
                return RedirectToAction("Index", "Home");
            }

            if (await agentService.AgentWithPhoneNumberExist(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), AgentControllerConstants.UserWithPhoneNumberExistError);
            }

            if (await userService.UserHasRents(userId))
            {
                ModelState.AddModelError("Error", AgentControllerConstants.UserHasRentsError);
            }

            await agentService.Create(userId, model.PhoneNumber);

            TempData[AgentControllerConstants.SuccessMessage] = AgentControllerConstants.UserBecomeAgent;
            return RedirectToAction("All", "Car");
        }
    }
}
