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

        public StudentsController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        // The route for this action method (for this controller) is the name itself.
        [HttpGet]
        [Route("api/GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            var students = studentRepository.GetStudents().ToList();

            var domainStudentModels = new List<Student>();

            foreach (var student in students)
            {
                domainStudentModels.Add(new Student
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    DateOfBirth = student.DateOfBirth,
                    Email = student.Email,
                    Mobile = student.Mobile,
                    ProfileImageUrl = student.ProfileImageUrl,
                    GenderId = student.GenderId
                });
            }

            // Return this list as Ok object because this is a RESTFul API
            return Ok(domainStudentModels);
        }
    }
}
