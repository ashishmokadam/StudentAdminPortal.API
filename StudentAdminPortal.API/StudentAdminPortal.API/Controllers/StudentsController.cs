using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.Repositories;
using System.Linq;

namespace StudentAdminPortal.API.Controllers
{
    // Everytime you create a new controller - Annotate and tag it with APIContoller DataAnnotation 
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        // The route for this action method (for this controller) is the name itself.
        [HttpGet]        
        [Route("api/GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            // Return this list as Ok object because this is a RESTFul API
            return Ok(studentRepository.GetStudents().ToList());
        }
    }
}
