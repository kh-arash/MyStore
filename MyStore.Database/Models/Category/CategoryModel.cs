using MyStore.Database.Models.Assets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Database.Models.Category
{
    [Table("Category")]
    public class CategoryModel : BaseModel
    {
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public AssetsModel Asset { get; set; }
        public List<SubCategoryModel> SubCategories { get; set; }
    }
}
