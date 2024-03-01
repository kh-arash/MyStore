using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace MyStore.Service.Services.Category.ViewModels
{
    public class CategoryCreateViewModel
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        [DisplayName("Parent")]
        public int? ParentId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
