using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Services.DTOs;
using Services.Interfaces;
using SocialApp.DataAccess.Interfaces;

namespace Services.Services;

public class UserService: IUserService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILikesRepository _likesRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUsersRepository usersRepository, ILikesRepository likesRepository, IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _likesRepository = likesRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _usersRepository.GetAllUsersAsync();
        var userDtos = _mapper.Map<List<UserDto>>(users);

        return userDtos;
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _usersRepository.GetUserByIdAsync(id);
        var userDto = _mapper.Map<UserDto>(user);

        return userDto;
    }

    public async Task CreateLikeAsync(int sourceUserId, int targetUserId)
    {
        await _likesRepository.CreateLikeAsync(sourceUserId, targetUserId);
        await _unitOfWork.Complete();
    }
}