using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;

namespace Services.Interfaces;

public interface IAccountService
{
    Task<AccountDto> Login(LoginDto loginDto);
}