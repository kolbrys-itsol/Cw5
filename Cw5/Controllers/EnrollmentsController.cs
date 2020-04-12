using System;
using System.Data.SqlClient;
using Cw5.DTOs.Requests;
using Cw5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cw5.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : Controller
    {
        private readonly IStudentsDbService _dbService;

        public EnrollmentsController(IStudentsDbService iStudentsDbService)
        {
            _dbService = iStudentsDbService;
        }


        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {

            try
            {
                return Ok(_dbService.EnrollStudent(request));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudent(PromoteStudentRequest request)
        {
            try
            {
                return Ok(_dbService.PromoteStudents(request));
            }
            catch (SqlException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}