using System.Threading.Tasks;
using SocialApp.DataAccess.Entities;

namespace Services.Interfaces;

public interface ITokenGenerator
{
    Task<string> CreateToken(AppUser user);
}