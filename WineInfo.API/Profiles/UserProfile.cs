using AutoMapper;

namespace WineInfo.API.Profiles
{
    public class UserProfile : Profile 
    {
        public UserProfile()
        {
            CreateMap<Entities.User, Models.UserDto>();
        }
    }
}
