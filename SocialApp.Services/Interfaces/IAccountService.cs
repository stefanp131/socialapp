using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;

namespace Services.Interfaces;

public interface IAccountService
{
    Task<AccountDto> LoginAsync(LoginDto loginDto);
    Task<AccountDto> RegisterAsync(RegisterDto registerDto);
    Task UpdateProfileAsync(int id, UpdateProfileDto updateProfile);
    Task<ProfileDto> GetProfileAsync(int id);
}