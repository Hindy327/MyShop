using Entities;

namespace Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Order order);
        Task<Order> GetOrderById(int id);
        Task<double> checkOrderSum(Order order);

    }
}