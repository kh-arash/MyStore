using MyStore.Database.Interfaces;
using MyStore.Service.Services.Category.Mapper;
using MyStore.Service.Services.Category.ViewModels;
using MyStore.Service.Services.File;

namespace MyStore.Service.Services.Category.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        public CategoryService(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task Create(CategoryCreateViewModel model)
        {
            try
            {
                var objModel = model.ToModel();
                objModel.CreatedDate = DateTime.Now;
                objModel.CreatedUserId = Guid.Parse("af6a6896-6b30-43c6-abbd-33febb129804");
                if (model.Image != null)
                    objModel.Image = await _fileService.Upload(model.Image);

                await _unitOfWork.CategoryRepository.Insert(objModel);
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

        public async Task<List<CategoryListViewModel>> GetAll()
        {
            var categories = await _unitOfWork.CategoryRepository.Get(filter: x => x.ParentId == null);

            return categories.ToListViewModel();
        }

        public async Task<List<CategoryListViewModel>> GetByParentId(int id)
        {
            var categories = await _unitOfWork.CategoryRepository.Get(filter: x => x.ParentId == id);
            return categories.ToListViewModel();
        }

        public Task Update()
        {
            throw new NotImplementedException();
        }
    }
}
