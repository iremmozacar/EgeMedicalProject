using EgeApp.Backend.Entity.Concrete;
using EgeApp.Backend.Models;

namespace EgeApp.Backend.Data.Concrete.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
