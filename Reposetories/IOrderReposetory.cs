using Entities;

namespace Reposetories
{
    public interface IOrderReposetory
    {
        Task CreateOrder(Order order);
        Task<Order> GetOrderById(int id);
    }
}