using Microsoft.AspNetCore.Mvc;
using MyStore.Database.Models.Authentication.Login;
using MyStore.Database.Models.Authentication.SignUp;
using MyStore.Database.Models.User;
using MyStore.Service;
using MyStore.Userinterface.Models;
using System.Diagnostics;

namespace MyStore.Userinterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AuthenticateRestClient<LoginResponse> _authenticateRestClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _authenticateRestClient = new AuthenticateRestClient<LoginResponse>(_httpClientFactory.CreateClient("AuthenticationAPI"));
        }

        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(Request.Cookies["token"]))
            {
                var result = _authenticateRestClient.Get("profile", Request.Cookies["token"].ToString()).GetAwaiter().GetResult();
                if (result.IsSuccess)
                {
                    var userInfo = result.Result.User;
                    ViewBag.Profile = new ProfileModel
                    {
                        FirstName = userInfo.FirstName,
                        LastName = userInfo.LastName,
                        Username = userInfo.Email,
                        Roles = result.Result.Roles
                    };
                }

            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticateRestClient.Post("login", model);
                if (result.IsSuccess)
                {
                    Response.Cookies.Append("token", result.Result.AccessToken.Token, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddMinutes(60)
                    });
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Message = "Username or Password is wrong.";
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                model.Roles.Add("User");
                var result = await _authenticateRestClient.Post("", model);
                if (result.IsSuccess)
                    return RedirectToAction("ConfirmEmail", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            var result = await _authenticateRestClient.Post("logout", token: Request.Cookies["token"]);
            if(result.IsSuccess)
                Response.Cookies.Delete("token");
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult ConfirmEmail()
        {
            return View();
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