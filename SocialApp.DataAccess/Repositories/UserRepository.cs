using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialApp.DataAccess.Data;
using SocialApp.DataAccess.Entities;
using SocialApp.DataAccess.Interfaces;

namespace SocialApp.DataAccess.Repositories;

public class UserRepository : IUsersRepository
{
    private readonly SocialAppContext _context;
    private readonly UserManager<AppUser> _userManager;

    public UserRepository(SocialAppContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
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

    public async Task<string> GetViewsAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user?.Views;
    }
    
    public async Task SaveViewsAsync(int id, string views)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user != null)
        {
            user.Views = views;
            await _userManager.UpdateAsync(user);
        }
    }
    
    public async Task ClearViewsAsync(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user != null)
        {
            user.Views = null;
            await _userManager.UpdateAsync(user);
        }
    }
}