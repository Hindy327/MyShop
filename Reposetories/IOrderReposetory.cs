using Entities;

namespace Reposetories
{
    public interface IOrderReposetory
    {
        public Task<Order> CreateOrder(Order order);
        Task<Order> GetOrderById(int id);
    }
}