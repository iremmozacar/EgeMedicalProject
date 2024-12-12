using System;
using EgeApp.Backend.Entity.Concrete;

namespace EgeApp.Backend.Data.Abstract
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart> GetCartAsync(string userId);
    }
}
