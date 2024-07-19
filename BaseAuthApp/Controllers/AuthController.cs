using BaseAuthApp_BAL.Models;
using BaseAuthApp_BAL.Services;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<UserModel>> Register([FromForm] UserCreateModel userCreateModel)
        {
            var validationResponse = ValidateModelState();
            if (validationResponse != null)
            {
                return validationResponse;
            }

            var result = await _serviceUser.RegisterUserAsync(userCreateModel);
            return GetResponse(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login([FromForm] LoginModel loginModel)
        {
            var validationResponse = ValidateModelState();
            if (validationResponse != null)
            {
                return validationResponse;
            }

            var result = await _serviceUser.ValidateUserWithResultAsync(loginModel.Username, loginModel.Password);
            return GetResponse(result);            
        }

        [Authorize]
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
