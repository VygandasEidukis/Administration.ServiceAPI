using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Administration.Models.Account
{
    public class User : IUser
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public DateTime CreationDate { get; set; }

        public IEnumerable<UserRoles> Roles { get; set; }

        public string Token
        {
            get
            {
                return Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes($"{Username}:{Password}"));
            }
        }

        /// <summary>
        /// Password for the user
        /// </summary>
        public string Password { get; set; }

        public void TruncateSecretData()
        {
            Password = null;
        }

        public User()
        {
        }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}