using CarRentingSystem.Areas.Admin.Constants;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CarRentingSystem.Areas.Admin.Controllers
{
    public class UserController : AdminController
    {
        private readonly IUserService userService;
        private readonly IMemoryCache memoryCache;

        public UserController(IUserService userService, IMemoryCache memoryCache)
        {
            this.userService = userService;
            this.memoryCache = memoryCache;
        }

        public async Task<IActionResult> All()
        {
            //var model = this.memoryCache.Get<IEnumerable<UserServiceModel>>(AdminConstants.UsersCacheKey);

            //if (model == null)
            //{
            //    model = await this.userService.All();

            //    var cacheOptions = new MemoryCacheEntryOptions()
            //        .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

            //    this.memoryCache.Set(AdminConstants.UsersCacheKey, model, cacheOptions);
            //}

            var model = await this.userService.All();

            return View(model);
        }
    }
}
