using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BaseAuthApp.Controllers
{
    public class BaseController : ControllerBase
    {
        public ActionResult<T> GetResponse<T, TError>(Result<T, TError> result)
        {
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
