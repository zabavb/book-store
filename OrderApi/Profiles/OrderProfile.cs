using AutoMapper;
using OrderApi.Models;

namespace OrderApi.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id));

            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
