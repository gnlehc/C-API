using BootcampAPI.Helper;
using BootcampAPI.Output;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BootcampAPI.Controller
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ReligionController : ControllerBase
    {
        private ReligionHelper religionHelper;
        public ReligionController(ReligionHelper religionHelper)
        {
            this.religionHelper = religionHelper;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get()
        {
            try
            {
                var objJSON = new ReligionOutput();
                objJSON.payload = religionHelper.ReligionList();
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
