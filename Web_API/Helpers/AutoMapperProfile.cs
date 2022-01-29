using AutoMapper;
using Entities;

namespace Web_API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserResponse, User>();
            CreateMap<User, UserResponse>();
            CreateMap<UserRequest, User>();
            CreateMap<User, UserRequest>();
        }
    }
}
