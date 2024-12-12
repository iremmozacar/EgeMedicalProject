using EgeApp.Backend.Data.Abstract;
using EgeApp.Backend.Entity.Concrete;
using EgeApp.Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EgeApp.Backend.Data.Concrete.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _dbContext;

      
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       
        public async Task SaveChangesAsync()
        {
           
            await _dbContext.SaveChangesAsync();
        }
    }
}