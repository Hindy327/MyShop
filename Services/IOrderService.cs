using Entities;

namespace Services
{
    public interface IOrderService
    {
        Task CreateOrder(Order order);
        Task<Order> GetOrderById(int id);
    }
}