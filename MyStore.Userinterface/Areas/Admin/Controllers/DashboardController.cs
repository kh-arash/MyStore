using Microsoft.AspNetCore.Mvc;
using MyStore.Userinterface.Models;

namespace MyStore.Userinterface.Areas.Admin.Controllers
{
    [Area("Admin")]
    [MyStoreAuthorize(Roles: "Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
