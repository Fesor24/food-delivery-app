using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Whatever path is in this route attribute must match that which we are passing to in the middleware
    /// </summary>
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        public IActionResult Errors(int code)
        {
            return new ObjectResult(new ApiResponse { ErrorMessage = "Endpoint does not exist", ErrorResult = code });
        }
    }
}
