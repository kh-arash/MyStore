using MyStore.Database.Models.Assets;
using MyStore.Database.Models.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Database.Models.Product
{
    public class ProductModel:BaseModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }
        public string Model { get; set; }
        public List<AssetsModel> Assets { get; set; }
        public List<SubCategoryModel> Categories { get; set; }

        public string Name
        {
            get
            {
                return Title + " / " + Model;
            }
        }
    }
}
