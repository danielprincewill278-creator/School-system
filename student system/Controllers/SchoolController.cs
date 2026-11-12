using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using student_system.Dto;
using student_system.Repositories;

namespace student_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly IStudentRepositoy _studentRepositoy;

        public SchoolController(IStudentRepositoy studentRepositoy)
        {
            _studentRepositoy = studentRepositoy;
        }

        [HttpGet("GetAllStudents")]
        [Authorize(Roles = "Admin,User")]

        public IActionResult GetAllStudents()
        {
            var students = _studentRepositoy.GetAllStudents();
            return Ok(students);
        }

        [HttpGet("GetStudent/{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetStudent(int id)
        {
            var student = _studentRepositoy.GetStudent(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost("AddStudent")]
        [Authorize(Roles ="Admin")]

        public IActionResult AddStudent([FromBody] StudentDto student)
        {
            var result = _studentRepositoy.AddStudent(student);
            return Ok(result);
        }

        [HttpPut("UpdateStudent")]
        [Authorize(Roles = "Admin")]

        public IActionResult UpdateStudent([FromBody] StudentDtoUpdate student)
        {
            var result = _studentRepositoy.UpdateStudent(student);
            return Ok(result);
        }

        [HttpDelete("DeleteStudent/{studentId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteStudent(int studentId)
        {
            var result = _studentRepositoy.DeleteStudent(studentId);
            return Ok(result);
        }

    }
}
