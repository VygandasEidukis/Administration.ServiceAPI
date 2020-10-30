using EPS.Administration.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.Administration.ServiceAPI.Helper
{
    public class UserService : IUserService
    {
        //TODO: HIGH change this to database
        private List<User> _users = new List<User>
        {
            new User { Id = 1, CreationDate = DateTime.Now.AddDays(-3), Password = "TEST", Username = "TEST", Roles = new List<UserRoles>() { UserRoles.Support, UserRoles.System_Administrator } }
        };

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));
            user.TruncateSecretData();
            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            return user;
        }
    }
}
