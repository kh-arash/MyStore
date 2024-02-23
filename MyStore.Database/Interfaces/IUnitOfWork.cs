using MyStore.Database.Models.Category;
using MyStore.Database.Models.Product;

namespace MyStore.Database.Interfaces
{
    public interface IUnitOfWork
    {
        public Repository<CategoryModel> CategoryRepository { get; }
        public Repository<ProductModel> ProductRepository { get; }
    }
}
