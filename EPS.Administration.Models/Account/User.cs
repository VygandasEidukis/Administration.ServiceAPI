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

        /// <summary>
        /// Password for the user
        /// </summary>
        public string Password { get; set; }

        public void TruncateSecretData()
        {
            Password = null;
        }
    }
}
