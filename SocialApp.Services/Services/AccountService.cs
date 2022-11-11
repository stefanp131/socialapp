using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.CustomExceptions;
using Services.DTOs;
using Services.Interfaces;
using SocialApp.DataAccess.Entities;
using SocialApp.DataAccess.Interfaces;

namespace Services.Services;

public class AccountService : IAccountService
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(ITokenGenerator tokenGenerator, IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUnitOfWork unitOfWork)
    {
        _tokenGenerator = tokenGenerator;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<AccountDto> Login(LoginDto loginDto)
    {
        var user = await this._userManager.Users
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

        if (user == null) throw new BadCredentialsException();

        var result = await this._signInManager
            .CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) throw new BadCredentialsException();

        return new AccountDto()
        {
            Username = user.UserName,
            Token = await this._tokenGenerator.CreateToken(user),
        };
    }
}