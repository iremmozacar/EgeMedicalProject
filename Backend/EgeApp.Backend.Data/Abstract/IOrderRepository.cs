using System;
using EgeApp.Backend.Entity.Concrete;

namespace EgeApp.Backend.Data.Abstract
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetSortedOrdersAsnyc(string? userId = null);
    }
}
