using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStore.Database.Models.User;
using MyStore.Service;
using MyStore.Service.Models;
using MyStore.Service.Services.Category.Implementation;
using MyStore.Service.Services.Category.ViewModels;
using MyStore.Service.Services.Product.ViewModels;
using MyStore.Userinterface.Models;

namespace MyStore.Userinterface.Areas.Admin.Controllers
{
    [Area("Admin")]
    [MyStoreAuthorize(Roles: "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly AuthenticateRestClient<LoginResponse> _authenticateRestClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(ICategoryService categoryService, IHttpClientFactory httpClientFactory)
        {
            _categoryService = categoryService;
            _httpClientFactory = httpClientFactory;
            _authenticateRestClient = new AuthenticateRestClient<LoginResponse>(_httpClientFactory.CreateClient("AuthenticationAPI"));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> LoadData(JqueryDatatableParam param)
        {
            var categories = await _categoryService.GetAll();

            var totalRecords = categories.Count();
            return new JsonResult(new
            {
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = categories
            });
        }

        [HttpGet]
        public async Task<JsonResult> LoadSubCategory(int parentId)
        {
            var categories = new List<CategoryListViewModel>();
            if (parentId > 0)
                categories = await _categoryService.GetByParentId(parentId);

            return new JsonResult(new
            {
                categories
            });
        }

        [HttpGet]
        public IActionResult Add(int? id = null)
        {
            var categories = _categoryService.GetAll().GetAwaiter().GetResult();
            ViewBag.Categories = new SelectList(categories, "Id", "Title", id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CategoryCreateViewModel categoryModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _authenticateRestClient.Post("validate-token", Request.Cookies["token"].ToString(), Request.Cookies["token"].ToString()).GetAwaiter().GetResult();
                    categoryModel.UserId = result.Result.User.Id;
                    await _categoryService.Create(categoryModel);
                    ViewBag.Message = "Category Created Successfully";
                    return RedirectToAction("Index");
                }
                return View(categoryModel);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                throw;
            }
        }
    }
}
