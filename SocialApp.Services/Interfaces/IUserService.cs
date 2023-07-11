﻿using Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<List<UserDto>> GetUsersByStringTermAsync(string stringTerm);
        Task CreateLikeAsync(int sourceUserId, int targetUserId);
        Task DeleteLikeAsync(int sourceUserId, int targetUserId);
    }
}
