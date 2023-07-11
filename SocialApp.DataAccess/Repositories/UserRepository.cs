using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialApp.DataAccess.Data;
using SocialApp.DataAccess.Entities;
using SocialApp.DataAccess.Interfaces;

namespace SocialApp.DataAccess.Repositories;

public class UserRepository : IUsersRepository
{
    private readonly SocialAppContext _context;

    public UserRepository(SocialAppContext context)
    {
        _context = context;
    }

    public async Task<AppUser> GetUserByIdAsync(int id)
    {
        return await _context.Users
            .Include(user => user.LikedByUsers)
            .FirstOrDefaultAsync(user => user.Id == id);
    }
    
    public async Task<List<AppUser>> GetUsersByStringTermAsync(string stringTerm)
    {
        return await _context.Users
            .Include(user => user.LikedByUsers)
            .Where(user => user.UserName.Contains(stringTerm))
            .ToListAsync();
    }

    public async Task<List<AppUser>> GetAllUsersAsync()
    {
        return await _context.Users
            .Include(user => user.LikedByUsers)
            .ToListAsync();
    }
}