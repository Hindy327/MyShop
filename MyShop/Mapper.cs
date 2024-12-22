using AutoMapper;
using Entities;
using DTO;

namespace MyShop
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Order, OrderPostDTO>();
            CreateMap<OrderPostDTO, Order>();
            CreateMap<OrderItem, OrderItemDTO>();
            CreateMap<OrderItemDTO, OrderItem>();
            CreateMap<UserRegisterDTO, User>();
        }
        
    }
}
