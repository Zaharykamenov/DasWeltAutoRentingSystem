using CarRentingSystem.Constants;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.Agent;
using CarRentingSystem.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarRentingSystem.Controllers
{
    /// <summary>
    /// Controller for agent
    /// </summary>
    [Authorize]
    public class AgentController : Controller
    {
        private readonly IAgentService agentService;
        private readonly IUserService userService;
        private readonly IErrorLogService errorLogService;

        /// <summary>
        /// Constructor for AgentController
        /// </summary>
        /// <param name="agentService"></param>
        /// <param name="userService"></param>
        public AgentController(IAgentService agentService, IUserService userService, IErrorLogService errorLogService)
        {
            this.agentService = agentService;
            this.userService = userService;
            this.errorLogService = errorLogService;
        }

        /// <summary>
        /// GET method specific user become agent
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Become()
        {
            try
            {
                if (await agentService.ExistById(User.Id()))
                {
                    return RedirectToAction("Index", "Home");
                }

                var model = new BecomeAgentModel();

                return View(model);
            }
            catch (Exception ex)
            {
                await this.errorLogService.SaveError(ex, User.Id());
                return RedirectToAction("Error", "Home");
            }
        }

        /// <summary>
        /// POST method specific user become agent 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentModel model)
        {
            try
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
            catch (Exception ex)
            {
                await this.errorLogService.SaveError(ex, User.Id());
                return RedirectToAction("Error", "Home");
            }

        }
    }
}
