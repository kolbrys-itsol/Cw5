using System.Collections.Generic;
using Cw5.DTOs.Requests;
using Cw5.DTOs.Responses;
using Cw5.Models;

namespace Cw5.Services
{
    public interface IStudentsDbService
    {
        public IEnumerable<Student> GetStudents();
        public Student GetStudent(string id);
        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);
        public EnrollStudentResponse PromoteStudents(PromoteStudentRequest request);
    }
}