using CarRentingSystem.Areas.Admin.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Areas.Admin.Controllers
{
    [Area(AdminConstants.AdminAreaName)]
    [Route("Admin/[controller]/[Action]/{id?}")]
    [Authorize(Roles = AdminConstants.AdminRoleName)]
    public class AdminController : Controller
    {

    }
}
