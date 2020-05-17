using System;
using System.ComponentModel.DataAnnotations;

namespace Cw5.DTOs.Requests
{
    public class UpdateStudentRequest
    {
        [Required]
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Enrollment { get; set; }
    }
}