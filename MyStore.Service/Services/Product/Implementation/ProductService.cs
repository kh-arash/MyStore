using MyStore.Database.Interfaces;
using MyStore.Database.Models.Product;
using MyStore.Service.Services.File;
using MyStore.Service.Services.Product.Mapper;
using MyStore.Service.Services.Product.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Service.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        public ProductService(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task Create(ProductCreateViewModel model)
        {
            try
            {
                var objModel = model.ToModel();
                objModel.CreatedDate = DateTime.Now;
                objModel.CreatedUserId = Guid.Parse("af6a6896-6b30-43c6-abbd-33febb129804");
                objModel.Image = await _fileService.Upload(model.Image);

                await _unitOfWork.ProductRepository.Insert(objModel);

                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Task Delete()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductListViewModel>> GetAll()
        {
            var products = await _unitOfWork.ProductRepository.Get();

            return products.ToListViewModel();
        }

        public Task Update()
        {
            throw new NotImplementedException();
        }
    }
}
