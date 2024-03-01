using Microsoft.AspNetCore.Mvc;
using MyStore.Database.Models.Authentication.Login;
using MyStore.Database.Models.Authentication.SignUp;
using MyStore.Userinterface.Models;
using System.Diagnostics;

namespace MyStore.Userinterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            return new JsonResult("");
        }

        [HttpPost]
        public IActionResult Register(RegisterUserModel model)
        {
            return new JsonResult("");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}