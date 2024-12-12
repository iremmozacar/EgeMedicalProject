using EgeApp.Backend.Data.Abstract;
using EgeApp.Backend.Entity.Concrete;
using EgeApp.Backend.Models;

namespace EgeApp.Backend.Data;

public interface IProductRepository : IGenericRepository<Product>
{
    
    Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);
}
