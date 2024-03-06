using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyStore.Database.Models.User;
using MyStore.Service;
using MyStore.Service.Models;
using MyStore.Service.Services.Category.Implementation;
using MyStore.Service.Services.Product;
using MyStore.Service.Services.Product.ViewModels;
using MyStore.Userinterface.Models;

namespace MyStore.Userinterface.Areas.Admin.Controllers
{
    [Area("Admin")]
    [MyStoreAuthorize(Roles: "Admin")]
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly AuthenticateRestClient<LoginResponse> _authenticateRestClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IProductService productService, IHttpClientFactory httpClientFactory, ICategoryService categoryService)
        {
            _productService = productService;
            _httpClientFactory = httpClientFactory;
            _authenticateRestClient = new AuthenticateRestClient<LoginResponse>(_httpClientFactory.CreateClient("AuthenticationAPI"));
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> LoadData(JqueryDatatableParam param)
        {
            var products = await _productService.GetAll();
            //if (!string.IsNullOrEmpty(param.sSearch))
            //{
            //    products = products.Where(x => x.Title.ToLower().Contains(param.sSearch.ToLower())
            //    || x.Model.ToLower().Contains(param.sSearch.ToLower())).ToList();
            //}
            //int start = int.Parse(Request.Form["start"].FirstOrDefault()??"0");
            //int length = int.Parse(Request.Form["length"].FirstOrDefault()??"10");

            var totalRecords = products.Count();
            return new JsonResult(new
            {
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = products
            });
        }

        [HttpGet]
        public IActionResult Add()
        {
            var categories = _categoryService.GetAll().GetAwaiter().GetResult();
            ViewBag.Categories = new SelectList(categories, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProductCreateViewModel productModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _authenticateRestClient.Post("validate-token", Request.Cookies["token"].ToString(), Request.Cookies["token"].ToString()).GetAwaiter().GetResult();
                    productModel.UserId = result.Result.User.Id;
                    await _productService.Create(productModel);
                    ViewBag.Message = "Product Created Successfully";
                    return RedirectToAction("Index");
                }
                return View(productModel);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                throw;
            }
        }
    }
}
