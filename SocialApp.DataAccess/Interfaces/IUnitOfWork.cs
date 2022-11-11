using System.Threading.Tasks;

namespace SocialApp.DataAccess.Interfaces;

public interface IUnitOfWork
{
    Task<int> Complete();
}