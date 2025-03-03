using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reposetories;
using Entities;
using Microsoft.Extensions.Logging;


namespace Services
{
    public class OrderService : IOrderService
    {
        IOrderReposetory orderReposetory;
        private readonly ILogger<OrderService> _logger;
        IProductReposetory ProductReposetory;

        public OrderService(IOrderReposetory orderReposetory, IProductReposetory ProductReposetory, ILogger<OrderService> logger)
        {
            this.orderReposetory = orderReposetory;
            _logger = logger;
            this.ProductReposetory = ProductReposetory;
        }
        public async Task<Order> CreateOrder(Order order)
        {
           double newOrder =await checkOrderSum(order);
            if (newOrder != order.OrderSum)
            {
                order.OrderSum = newOrder;
                _logger.LogCritical($"OrderSum{order.OrderSum} not valid ");

            }
            return await orderReposetory.CreateOrder(order);
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await orderReposetory.GetOrderById(id);
        }

        public async Task<double> checkOrderSum(Order order)
        {
         
            double sum = 0;
            foreach (var item in order.OrderItems)
            {
                Product p = await ProductReposetory.GetProductById(item.ProductId);
                sum += (double)p.Price *(double)item.Quantity;
            }
            return sum;

        }

    }
}
