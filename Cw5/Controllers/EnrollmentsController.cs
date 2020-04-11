using System;
using Cw5.DTOs.Requests;
using Cw5.DTOs.Responses;
using Cw5.Models;
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
            // try
            // {
                return Ok(_dbService.EnrollStudent(request));
            // }
            // catch (Exception e)
            // {
            //     return BadRequest(e.Message);
            // }
        }
    }
}