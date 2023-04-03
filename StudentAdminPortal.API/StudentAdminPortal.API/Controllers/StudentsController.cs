using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace StudentAdminPortal.API.Controllers
{
    // Everytime you create a new controller - Annotate and tag it with APIContoller DataAnnotation 
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        // The route for this action method (for this controller) is the name itself.
        [HttpGet]
        [Route("api/GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            var students = studentRepository.GetStudents().ToList();

            // Return this list as Ok object because this is a RESTFul API
            return Ok(mapper.Map<List<Student>>(students));
        }
    }
}
