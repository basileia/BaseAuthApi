using BaseAuthApp_BAL.Models;
using BaseAuthApp_BAL.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaseAuthApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly ServiceUser _serviceUser;

        public AuthController(ServiceUser serviceUser)
        {
            _serviceUser = serviceUser;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register(UserCreateModel userCreateModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var error = new Error("ValidationError", "Model validation failed", errors);
                return BadRequest(error);
            }

            var result = await _serviceUser.RegisterUserAsync(userCreateModel);
            return GetResponse(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var error = new Error("ValidationError", "Model validation failed", errors);
                return BadRequest(error);
            }

            var result = await _serviceUser.ValidateUserWithResultAsync(loginModel.Username, loginModel.Password);
            return GetResponse(result);            
        }

        [HttpGet("profile")]
        public ActionResult GetProfile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest(AuthenticationError.UnauthorizedAccess);
            }

            var userName = User.Identity.Name;

            return Ok(new { UserName = userName });
        }
    }
}
