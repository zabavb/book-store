using AutoMapper;
using Library.UserEntities;
using UserAPI.Models.DTOs;

namespace UserAPI.Profiles
{
    public class SubscriptionProfile : Profile
    {
        public SubscriptionProfile() {
            CreateMap<Subscription, SubscriptionDTO>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.SubscriptionId))
                .ReverseMap()
                .ForMember(dst => dst.SubscriptionId, opt => opt.Ignore());
        }
    }
}
