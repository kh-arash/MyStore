using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Database.Models.Product
{
    [Table("ProductImages")]
    public class ProductImagesModel:BaseModel
    {
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
    }
}
