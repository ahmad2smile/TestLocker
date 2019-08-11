using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TestLocker.Utils
{
    public class ApiResponse : ControllerBase
    {
        public override OkObjectResult Ok(object data)
        {
            return base.Ok(new
            {
                code = HttpStatusCode.OK.GetHashCode(),
                data
            });
        }

        public override BadRequestObjectResult BadRequest(object data)
        {
            return base.BadRequest(new
            {
                code = HttpStatusCode.BadRequest.GetHashCode(),
                data
            });
        }

        public override UnauthorizedObjectResult Unauthorized(object data)
        {
            return base.Unauthorized(new
            {
                code = HttpStatusCode.Unauthorized.GetHashCode(),
                data
            });
        }

        public override NotFoundObjectResult NotFound(object data)
        {
            return base.NotFound(new
            {
                code = HttpStatusCode.NotFound.GetHashCode(),
                data
            });
        }
    }
}
