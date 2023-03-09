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
    public class GenderController : ControllerBase
    {
        private GenderHelper genderHelper;
        public GenderController(GenderHelper genderHelper)
        {
            this.genderHelper = genderHelper;
        }
        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get()
        {
            try
            {
                var objJSON = new GenderOutput();
                objJSON.payload = genderHelper.GetAllGenders();
                return new OkObjectResult(objJSON);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
