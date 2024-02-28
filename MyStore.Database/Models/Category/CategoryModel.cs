using MyStore.Database.Models.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStore.Database.Models.Category
{
    [Table("Category")]
    public class CategoryModel : BaseModel
    {
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public string? Image { get; set; }
        public CategoryModel? ParentCategory { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
