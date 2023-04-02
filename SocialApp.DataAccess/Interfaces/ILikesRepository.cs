using System.Threading.Tasks;

namespace SocialApp.DataAccess.Interfaces;

public interface ILikesRepository
{
    Task CreateLikeAsync(int sourceUserId, int targetUserId);
    Task DeleteLikeAsync(int sourceUserId, int targetUserId);
}