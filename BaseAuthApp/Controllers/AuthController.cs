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
                return BadRequest(ModelState);
            }

            var result = await _serviceUser.RegisterUserAsync(userCreateModel);
            return GetResponse(result);
        }
    }
}
