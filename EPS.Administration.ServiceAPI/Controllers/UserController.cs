using System.Threading.Tasks;
using EPS.Administration.Models.Account;
using EPS.Administration.Models.APICommunication;
using EPS.Administration.ServiceAPI.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EPS.Administration.ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);
            
            if (user == null)
            {
                //TODO: MEDIUM Add logging.
                return Ok(new LogInResponse { Message = "User name or password is incorrect", Error = ErrorCode.BadUsernameOrPassword });
            }

            return Ok(new LogInResponse(user.Token));
        }
    }
}
