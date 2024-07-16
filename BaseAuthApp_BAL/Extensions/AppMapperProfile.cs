using AutoMapper;
using BaseAuthApp_BAL.Models;
using BaseAuthApp_DAL.Entities;


namespace BaseAuthApp_BAL.Extensions
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserCreateModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));
        }
    }
}
