using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPS.Administration.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EPS.Administration.ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        [Authorize(Roles = "System_Administrator")]
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return new User();
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public bool Authenticate()
        {
            return true;
        }
    }
}
