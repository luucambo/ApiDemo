using bobo.Interfaces;
using bobo.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace bobo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService userService;
        public AuthenticationController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("authenticate")]
        
        [AllowAnonymous]
        public IActionResult Authenticate(UserCred userCred)
        {
            var result = userService.Authenticate(userCred);
            if (result.Success)
                return Ok(result);
            return Unauthorized(result);
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(UserCred userCred)
        {
            var result = userService.Register(userCred);
            return Ok(result);
        }
    }
}
