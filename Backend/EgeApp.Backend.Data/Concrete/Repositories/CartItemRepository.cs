using System;
using EgeApp.Backend.Data.Abstract;
using EgeApp.Backend.Entity.Concrete;

namespace EgeApp.Backend.Data.Concrete.Repositories
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
