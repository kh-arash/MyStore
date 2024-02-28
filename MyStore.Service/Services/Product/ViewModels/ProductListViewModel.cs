using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Service.Services.Product.ViewModels
{
    public class ProductListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
    }
}
