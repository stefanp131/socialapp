using AutoMapper;
using Services.DTOs;
using SocialApp.DataAccess.Entities;

namespace Services.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<RegisterDto, AppUser>();
    }

}