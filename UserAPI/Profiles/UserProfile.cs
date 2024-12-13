using AutoMapper;
using UserAPI.Models.DTOs;

namespace UserAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dst => dst.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ReverseMap()
                .ForMember(dst => dst.UserId, opt => opt.Ignore())
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => GetFirstName(src.FullName)))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => GetLastName(src.FullName)));
        }

        private static string GetFirstName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return string.Empty;
            var parts = fullName.Split(' ');
            return parts.Length > 0 ? parts[0] : string.Empty;
        }

        private static string GetLastName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return string.Empty;
            var parts = fullName.Split(' ');
            return parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : string.Empty;
        }
    }
}
