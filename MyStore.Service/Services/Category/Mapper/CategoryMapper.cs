using MyStore.Database.Models.Category;
using MyStore.Service.Services.Category.ViewModels;

namespace MyStore.Service.Services.Category.Mapper
{
    public static class CategoryMapper
    {
        public static List<CategoryListViewModel> ToListViewModel(this IEnumerable<CategoryModel> categoryModel)
        {
            var categoryList = new List<CategoryListViewModel>();
            foreach (var item in categoryModel)
            {
                categoryList.Add(new CategoryListViewModel
                {
                    Title = item.Title,
                    Description = item.Description == null ? "" : item.Description,
                    Id = item.Id,
                });
            }

            return categoryList;
        }

        public static CategoryModel ToModel(this CategoryCreateViewModel viewModel)
        {
            return new CategoryModel
            {
                Description = viewModel.Description,
                Title = viewModel.Title,
                Image = "",
                ParentId = viewModel.ParentId
            };
        }
    }
}
