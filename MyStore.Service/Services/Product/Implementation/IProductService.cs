using MyStore.Database.Models.Product;
using MyStore.Service.Services.Product.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Service.Services.Product
{
    public interface IProductService
    {
        Task Create(ProductCreateViewModel model);
        Task Delete();
        Task Update();
        Task<List<ProductListViewModel>> GetAll();

    }
}
