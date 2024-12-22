using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reposetories;
using Entities;


namespace Services
{
    public class OrderService : IOrderService
    {
        IOrderReposetory orderReposetory;

        public OrderService(IOrderReposetory orderReposetory)
        {
            this.orderReposetory = orderReposetory;
        }
        public async Task CreateOrder(Order order)
        {
            await orderReposetory.CreateOrder(order);
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await orderReposetory.GetOrderById(id);
        }

    }
}
