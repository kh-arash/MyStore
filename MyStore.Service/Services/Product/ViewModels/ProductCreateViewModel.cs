using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Service.Services.Product.ViewModels
{
    public class ProductCreateViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(60)]
        public string Model { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? UserId { get; set; }
    }
}
