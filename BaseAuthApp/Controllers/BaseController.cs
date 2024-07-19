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

        protected ActionResult ValidateModelState()
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

            return null;
        }
    }
}
