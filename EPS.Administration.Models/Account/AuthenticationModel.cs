using System.ComponentModel.DataAnnotations;

namespace EPS.Administration.Models.Account
{
    public class AuthenticationModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}