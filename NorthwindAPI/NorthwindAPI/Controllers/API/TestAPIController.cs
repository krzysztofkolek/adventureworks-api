using System.Web.Http;

namespace NorthwindAPI.Controllers.API
{
    public class TestAPIController : ApiController
    {
        [HttpGet]
        public string Test()
        {
            return "ok";
        }
    }
}
