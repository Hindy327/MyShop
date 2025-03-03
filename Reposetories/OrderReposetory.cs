using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Reposetories
{
    public class OrderReposetory : IOrderReposetory
    {
        _327725412WebApiContext ConectDb;

        public OrderReposetory(_327725412WebApiContext _327725412WebApiContext)
        {
            ConectDb = _327725412WebApiContext;
        }
        public async Task<Order> CreateOrder(Order order)
        {
            var a = await ConectDb.Orders.AddAsync(order);
            await ConectDb.SaveChangesAsync();
            order.OrderId=a.Entity.OrderId;
            return order;
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await ConectDb.Orders.Include(O=>O.User).FirstOrDefaultAsync(order => order.OrderId == id);
        }
    }
}
