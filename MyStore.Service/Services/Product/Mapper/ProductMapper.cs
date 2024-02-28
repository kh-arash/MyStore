using MyStore.Database.Models.Product;
using MyStore.Service.Services.Product.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Service.Services.Product.Mapper
{
    public static class ProductMapper
    {
        public static List<ProductListViewModel> ToListViewModel(this IEnumerable<ProductModel> productModel)
        {
            var productList = new List<ProductListViewModel>();
            foreach (var item in productModel)
            {
                productList.Add(new ProductListViewModel
                {
                    Model = item.Model,
                    Price = item.Price,
                    Title = item.Title,
                    Category = ""
                });
            }

            return productList;
        }

        public static ProductModel ToModel(this ProductCreateViewModel viewModel)
        {
            return new ProductModel
            {
                Description = viewModel.Description,
                Model = viewModel.Model,
                Title = viewModel.Title,
                Image = "",
                Price=viewModel.Price,
            };
        }
    }
}
