using Server.Models;

namespace Server.IRepository
{
    public interface IRefreshToken
    {
        Task Insert(RefreshToken refreshToken);

        Task Delete(string token);
    }
}
 