using System.Security.Claims;
using bobo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bobo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentService _service;

        public StudentController(ILogger<StudentController> logger, IStudentService service)
        {
            _logger = logger;
            _service = service;
        }

        //get
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            var response = _service.GetStudents();
            return new JsonResult(response);
        }

        //create
        [HttpPost]

        public IActionResult Post(Student student)
        {
            var user = User;
            var response = _service.SaveStudent(student);
            return new JsonResult(response);
        }

        //update
        [HttpPut]
        public IActionResult Put(Student student)
        {
            var response = _service.SaveStudent(student);
            return new JsonResult(response);
        }

        //id
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var response = _service.DeleteStudent(id);
            return new JsonResult(response);
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate()
        {

            return Ok();
        }
    }
}
