using Microsoft.AspNetCore.Mvc;
using Prototype.Domain;
using Prototype.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Prototype.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Signin([FromBody] SigninDTO signinUser)
        {
            var result = await _authenticationService.SignIn(signinUser.UserName, signinUser.Password);
            if (result == null)
            {
                return BadRequest(new { code = "LoginError", message = "Invalid Credentials" });
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> SignUp([FromBody] SignupDTO signupUser)
        {
            try 
            {
                var result = await _authenticationService.SignUp(signupUser);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest(new { code = result.Errors.Select(err => err.Code), message = result.Errors.Select(err => err.Description) });
            }
            catch(Exception ex)
            {
                return BadRequest(new { code = new String[1] { "SystemError" }, message = new String[1] { ex.Message } });
            }
        }
    }
}
