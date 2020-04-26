using System.Collections.Generic;
using System.Security.Claims;
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
        public bool CheckPassword(LoginRequest request);
        public Claim[] GetClaims(string id);
        public void SetRefreshToken(string id, string token);
        public string CheckRefreshToken(string token);
    }
}