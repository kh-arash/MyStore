using Microsoft.AspNetCore.Mvc;

namespace MyStore.Userinterface.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
