using System;
using Microsoft.EntityFrameworkCore;
using EgeApp.Backend.Data.Abstract;
using EgeApp.Backend.Entity.Concrete;

namespace EgeApp.Backend.Data.Concrete.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Order>> GetSortedOrdersAsnyc(string? userId = null)
        {

            if (userId == null)
            {
                return await _dbContext
                    .Orders
                    .OrderByDescending(x => x.OrderDate)
                    .Include(x => x.OrderItems)
                    .ThenInclude(y => y.Product)
                    .ToListAsync();
            }
            return await _dbContext
                    .Orders
                    .Where(x => x.UserId == userId)
                    .OrderByDescending(x => x.OrderDate)
                    .Include(x => x.OrderItems)
                    .ThenInclude(y => y.Product)
                    .ToListAsync();
        }
    }
}
