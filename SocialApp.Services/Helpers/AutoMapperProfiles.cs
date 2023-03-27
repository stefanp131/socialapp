using AutoMapper;
using Services.DTOs;
using SocialApp.DataAccess.Entities;

namespace Services.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<RegisterDto, AppUser>();
        CreateMap<UpdateProfileDto, AppUser>();
        CreateMap<AppUser, ProfileDto>();
        CreateMap<UserLike, UserLikeDto>();
        CreateMap<AppUser, UserDto>();
    }

}