using BootcampAPI.Helper;
using BootcampAPI.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BootcampAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private StudentHelper studentHelper;
        public StudentController(StudentHelper studentHelper)
        {
            this.studentHelper = studentHelper;
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        public IActionResult Delete(int id)
        {
            try
            {
                var objJSON = studentHelper.DeleteStudent(id);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Produces("application/json")]
        public IActionResult Post(StudentInput data)
        {
            try
            {
                var objJSON = studentHelper.CreateStudent(data);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Produces("application/json")]
        public IActionResult Patch(StudentInput data)
        {
            try
            {
                var objJSON = studentHelper.UpdateStudent(data);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
