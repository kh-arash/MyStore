using Microsoft.AspNetCore.Mvc;
using MyStore.Service.Models;
using MyStore.Service.Services.Product;
using MyStore.Service.Services.Product.ViewModels;

namespace MyStore.Userinterface.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
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
