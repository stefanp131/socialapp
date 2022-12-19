using System.Collections.Generic;
using System.Threading.Tasks;
using SocialApp.DataAccess.Entities;

namespace SocialApp.DataAccess.Interfaces;

public interface IUsersRepository
{
    Task<AppUser> GetUserByIdAsync(int id);
    Task<List<AppUser>> GetAllUsersAsync();
}