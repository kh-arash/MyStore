using MyStore.Database.Models.Assets;
using MyStore.Database.Models.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStore.Database.Models.Category
{
    [Table("Subcategory")]
    public class SubCategoryModel : BaseModel
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        public AssetsModel Asset { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
