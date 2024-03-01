using MyStore.Service.Services.Category.ViewModels;

namespace MyStore.Service.Services.Category.Implementation
{
    public interface ICategoryService
    {
        Task Create(CategoryCreateViewModel model);
        Task Delete();
        Task Update();
        Task<List<CategoryListViewModel>> GetAll();
        Task<List<CategoryListViewModel>> GetByParentId(int id);
    }
}
