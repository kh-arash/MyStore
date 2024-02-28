using MyStore.Database.Models.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Database.Models.Product
{
    [Table("Product")]
    public class ProductModel:BaseModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }
        public string Model { get; set; }
        public string Image { get; set; }
        public List<ProductImagesModel> ProductImages { get; set; }
        public List<CategoryModel> Categories { get; set; }

        public string Name
        {
            get
            {
                return Title + " / " + Model;
            }
        }
    }
}
