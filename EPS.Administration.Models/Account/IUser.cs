using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Administration.Models.Account
{
    internal interface IUser
    {
        /// <summary>
        /// Identification number for the user
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// User name used for user identification
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// Date of account creation
        /// </summary>
        DateTime CreationDate { get; set; }

        /// <summary>
        /// Roles assigned for the user
        /// </summary>
        IEnumerable<UserRoles> Roles { get; set; }
    }
}
