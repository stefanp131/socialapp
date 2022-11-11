using System.Threading.Tasks;
using SocialApp.DataAccess.Interfaces;

namespace SocialApp.DataAccess.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly SocialAppContext _context;

    public UnitOfWork(SocialAppContext context)
    {
        _context = context;
    }
    
    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }
}