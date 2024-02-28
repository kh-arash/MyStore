using MyStore.Database.Interfaces;
using MyStore.Database.Models.Category;
using MyStore.Database.Models.Product;
using static System.Net.Mime.MediaTypeNames;

namespace MyStore.Database
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationDbContext _context;
        private bool disposed;
        public UnitOfWork()
        {
            _context = ApplicationDbContextFactory.Create();
            disposed = false;
        }

        private Repository<ProductModel> productRepository;
        private Repository<CategoryModel> categoryRepository;
        public Repository<ProductModel> ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new Repository<ProductModel>(_context);
                }
                return productRepository;
            }
        }
        public Repository<CategoryModel> CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new Repository<CategoryModel>(_context);
                }
                return categoryRepository;
            }
        }
        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
