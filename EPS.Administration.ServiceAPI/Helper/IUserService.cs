using EPS.Administration.Models.Account;
using System.Threading.Tasks;

namespace EPS.Administration.ServiceAPI.Helper
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
    }
}
