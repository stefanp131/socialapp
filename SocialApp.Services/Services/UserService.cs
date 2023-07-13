using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Services.DTOs;
using Services.Interfaces;
using SocialApp.DataAccess.Entities;
using SocialApp.DataAccess.Interfaces;

namespace Services.Services;

public class UserService: IUserService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILikesRepository _likesRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUsersRepository usersRepository, ILikesRepository likesRepository, IMapper mapper, IUnitOfWork unitOfWork)
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
    
    public async Task<List<UserDto>> GetUsersByStringTermAsync(string stringTerm)
    {
        var users = await _usersRepository.GetUsersByStringTermAsync(stringTerm);
        var userDtos = _mapper.Map<List<UserDto>>(users);

        return userDtos;
    }

    public async Task CreateLikeAsync(int sourceUserId, int targetUserId)
    {
        await _likesRepository.CreateLikeAsync(sourceUserId, targetUserId);
        await _unitOfWork.Complete();
    }

    public async Task DeleteLikeAsync(int sourceUserId, int targetUserId)
    {
        await _likesRepository.DeleteLikeAsync(sourceUserId, targetUserId);
        await _unitOfWork.Complete();
    }

    public async Task SaveViewsAsync(int loggedInUser, int viewedProfileId, string userName)
    {
        var serializedViews = await this._usersRepository.GetViewsAsync(viewedProfileId);

        
        var dictionaryViews = serializedViews != null ? JsonSerializer.Deserialize<Dictionary<int, string>>(serializedViews) : new Dictionary<int, string>();

        if (dictionaryViews.ContainsKey(loggedInUser))
        {
            return;
        }
        else
        { 
            dictionaryViews.Add(loggedInUser, userName);
        }

        var updatedDictionaryViews = JsonSerializer.Serialize<Dictionary<int, string>>(dictionaryViews);

        await _usersRepository.SaveViewsAsync(viewedProfileId, updatedDictionaryViews);
    }

    public async Task ClearViewsAsync(int loggedInUser)
    {
        await _usersRepository.ClearViewsAsync(loggedInUser);
    }
}