using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.CustomExceptions;
using Services.DTOs;
using Services.Interfaces;

namespace SocialApp.API.Controllers;

public class AccountController : BaseApiController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AccountDto>> Login(LoginDto loginDto)
    {
        try
        {
            var accountDto = await _accountService.Login(loginDto);
            return Ok(accountDto);
        }
        catch(BadCredentialsException)
        {
            return Unauthorized("Wrong credentials");
        }
    }

}