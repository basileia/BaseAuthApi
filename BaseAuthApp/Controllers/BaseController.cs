using BaseAuthApp_BAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaseAuthApp.Controllers
{
    public class BaseController : ControllerBase
    {
        public ActionResult<T> GetResponse<T, TError>(Result<T, TError> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }        
    }
}
