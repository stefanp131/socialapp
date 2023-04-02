using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialApp.DataAccess.Data;
using SocialApp.DataAccess.Entities;
using SocialApp.DataAccess.Interfaces;

namespace SocialApp.DataAccess.Repositories;

public class LikesRepository : ILikesRepository
{
    private readonly SocialAppContext _context;

    public LikesRepository(SocialAppContext context)
    {
        _context = context;
    }

    public async Task CreateLikeAsync(int sourceUserId, int targetUserId)
    {
        await _context.Likes.AddAsync(new UserLike
        {
            SourceUserId = sourceUserId,
            TargetUserId = targetUserId
        });
    }

    public async Task DeleteLikeAsync(int sourceUserId, int targetUserId)
    {
        var like =  await _context.Likes.FirstOrDefaultAsync(like => like.SourceUserId == sourceUserId && like.TargetUserId == targetUserId);
        _context.Likes.Remove(like);
    }
}