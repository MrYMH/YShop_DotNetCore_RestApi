using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YShop.API.Errors;

namespace YShop.API.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
