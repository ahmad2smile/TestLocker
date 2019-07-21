using Microsoft.AspNetCore.Mvc;

namespace TestLocker.Utils
{
    public class ApiResponse : ControllerBase
    {
        public override OkObjectResult Ok(object data)
        {
            return base.Ok(new
            {
                code = 200,
                data
            });
        }

        public override BadRequestObjectResult BadRequest(object data)
        {
            return base.BadRequest(new
            {
                code = 400,
                data
            });
        }

        public override NotFoundObjectResult NotFound(object data)
        {
            return base.NotFound(new
            {
                code = 404,
                data
            });
        }
    }
}
