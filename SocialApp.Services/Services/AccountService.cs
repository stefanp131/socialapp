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

    public async Task<AccountDto> LoginAsync(LoginDto loginDto)
    {
        var user = await this._userManager.Users
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

        if (user == null) throw new BadCredentialsException();

        var result = await this._signInManager
            .CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) throw new BadCredentialsException();

        return new AccountDto()
        {
            Token = await this._tokenGenerator.CreateToken(user),
        };
    }

    public async Task<AccountDto> RegisterAsync(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username)) throw new RegistrationFailed("Username taken");

        var user = this._mapper.Map<AppUser>(registerDto);

        user.UserName = registerDto.Username.ToLower();

        var result = await this._userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) throw new RegistrationFailed(result.Errors.ToString());

        var roleResult = await this._userManager.AddToRoleAsync(user, "Member");

        if (!roleResult.Succeeded) throw new RegistrationFailed(result.Errors.ToString());

        return new AccountDto()
        {
            Token = await this._tokenGenerator.CreateToken(user),
        };
    }

    public async Task UpdateProfileAsync(int id, UpdateProfileDto updateProfile)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        _mapper.Map(updateProfile, user);

        await _unitOfWork.Complete();
    }

    public async Task<ProfileDto> GetProfileAsync(int id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        var profileDto = _mapper.Map<ProfileDto>(user);

        return profileDto;
    }

    private async Task<bool> UserExists(string username)
    {
        return await this._userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
}