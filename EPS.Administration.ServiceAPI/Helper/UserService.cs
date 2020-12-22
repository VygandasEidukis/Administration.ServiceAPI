using EPS.Administration.Models.Account;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.Administration.ServiceAPI.Helper
{
    public class UserService : IUserService
    {
        private List<User> _users { get; set; }
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;

        public UserService(IConfiguration configuration, ILogger<UserService> logger)
        {
            this._configuration = configuration;
            this._logger = logger;
            Init();
        }

        private void Init()
        {
            _users = new List<User>();

            var configUsers = _configuration.GetSection("ServiceUsers")?.GetChildren();

            if (configUsers == null || !configUsers.Any())
            {
                _logger.LogError("Failed to find ANY 'ServiceUsers' in configuration file");
                return;
            }

            foreach (var user in configUsers)
            {
                var username = user.GetSection("Username");
                if (username == null || !username.Exists() || string.IsNullOrEmpty(username.Value))
                {
                    _logger.LogWarning("User has no username or its not set... skipping user...");
                    continue;
                }

                var password = user.GetSection("Password");
                if (password == null || !password.Exists() || string.IsNullOrEmpty(password.Value))
                {
                    _logger.LogWarning($"User has no 'Password' or its not set... skipping user '{username}'...");
                    continue;
                }

                var idAsString = user.GetSection("UniqueIdentity");
                if (idAsString == null || !idAsString.Exists() || string.IsNullOrEmpty(idAsString.Value))
                {
                    _logger.LogWarning($"User has no 'UniqueIdentity' or its not set... skipping user '{username}'...");
                    continue;
                }

                if (!int.TryParse(idAsString.Value, out var id))
                {
                    _logger.LogWarning($"Failed to parse 'UniqueIdentity' as integer... value parsed: '{idAsString.Value}', skipping user '{username}'...");
                    continue;
                }

                if (_users.Any(user => user.Id == id))
                {
                    _logger.LogWarning($"'UniqueIdentity' IS NOT UNIQUE... value parsed: '{idAsString.Value}', skipping user '{username}'...");
                    continue;
                }

                var newUser = new User
                {
                    Id = id,
                    Username = username.Value,
                    Password = password.Value
                };
                _users.Add(newUser);
            }
        }

        public async Task<User> Authenticate(string username, string password)
        {
            string keyStorage = _configuration.GetValue<string>("KeyStorage");
            string key = KeyStorageHelper.GetKey(keyStorage);

            if (string.IsNullOrEmpty(key))
            {
                _logger.LogError($"No key storage set in storage '{keyStorage ?? ""}'");
                return null;
            }

            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username.ToLower() == username.ToLower() && EncryptionHelper.Decrypt(x.Password, key) == password));
            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            return user;
        }
    }
}
