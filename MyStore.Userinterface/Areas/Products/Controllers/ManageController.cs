using Microsoft.AspNetCore.Mvc;

namespace MyStore.Userinterface.Areas.Products.Controllers
{
    [Area("Products")]
    public class ManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
