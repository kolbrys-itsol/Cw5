using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Cw5.DTOs.Requests;
using Cw5.DTOs.Responses;
using Cw5.Models;

namespace Cw5.Services

{
    public class SqlServerDbService : IStudentsDbService
    {
        private const string connectionString = "Data Source=db-mssql;Initial Catalog=s18310;Integrated Security=True";

        public IEnumerable<Student> GetStudents()
        {
            List<Student> students = new List<Student>();

            using (var connection =
                new SqlConnection("Data Source=db-mssql;Initial Catalog=s18310;Integrated Security=True"))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText =
                    "select * from Student inner join Enrollment on Student.IdEnrollment=Enrollment.IdEnrollment inner join Studies on Enrollment.IdStudy=Studies.IdStudy";
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var student = new Student();
                    student.FirstName = reader["FirstName"].ToString();
                    student.LastName = reader["LastName"].ToString();
                    student.IndexNumber = reader["IndexNumber"].ToString();
                    student.BirthDate = DateTime.Parse(reader["BirthDate"].ToString());
                    student.Studies = reader["Name"].ToString();
                    student.Semester = int.Parse(reader["Semester"].ToString());
                    students.Add(student);
                }
            }

            return students;
        }

        public Student GetStudent(string index)
        {
            using (var connection =
                new SqlConnection("Data Source=db-mssql;Initial Catalog=s18310;Integrated Security=True"))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText =
                    "select * from Student inner join Enrollment on Student.IdEnrollment=Enrollment.IdEnrollment " +
                    "inner join Studies on Enrollment.IdStudy=Studies.IdStudy where IndexNumber=@index";
                command.Parameters.AddWithValue("index", index);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var student = new Student();
                    student.FirstName = reader["FirstName"].ToString();
                    student.LastName = reader["LastName"].ToString();
                    student.IndexNumber = reader["IndexNumber"].ToString();
                    student.BirthDate = DateTime.Parse(reader["BirthDate"].ToString());
                    student.Studies = reader["Name"].ToString();
                    student.Semester = int.Parse(reader["Semester"].ToString());
                    return student;
                }

                return null;
            }
        }

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            using (var connection =
                new SqlConnection("Data Source=db-mssql;Initial Catalog=s18310;Integrated Security=True"))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "select * from Studies where Name=@Name";
                command.Parameters.AddWithValue("Name", request.Studies);
                connection.Open();

                var reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    throw new Exception("No such studies: " + request.Studies);
                }

                int idStudy = (int) reader["IdStudy"];
                reader.Close();
                if (this.GetStudent(request.IndexNumber) != null)
                {
                    throw new Exception("Student " + request.IndexNumber + " already exists");
                }

                command.CommandText = "select * from Enrollment where IdStudy=@idStudy and Semester=@Semester";
                command.Parameters.AddWithValue("idStudy", idStudy);
                command.Parameters.AddWithValue("Semester", 1);
                reader = command.ExecuteReader();
                int enrollmentId = 0;
                DateTime startDate = DateTime.Now;
                if (!reader.Read())
                {
                    reader.Close();
                    command.CommandText = "select max(IdEnrollment) as currentMax from Enrollment";
                    reader = command.ExecuteReader();
                    if (!reader.Read())
                    {
                        enrollmentId = 1;
                    }
                    else
                    {
                        enrollmentId = 1 + (int) reader["currentMax"];
                    }

                    reader.Close();
                    command.CommandText = "insert into Enrollment(IdEnrollment,Semester,IdStudy,StartDate)" +
                                          " values(@newId,@Semester,@IdStudy,@StartDate)";
                    command.Parameters.AddWithValue("newId", enrollmentId);
                    command.Parameters.AddWithValue("IdStudy", idStudy);
                    command.Parameters.AddWithValue("Semester", 1);
                    command.Parameters.AddWithValue("StartDate", startDate);
                    command.ExecuteNonQuery();
                }
                else
                {
                    startDate = (DateTime) reader["StartDate"];
                    enrollmentId = (int) reader["IdEnrollment"];
                    reader.Close();

                }
                Console.WriteLine(enrollmentId);
                command.CommandText =
                    "insert into Student (IndexNumber,FirstName,LastName,BirthDate,IdEnrollment)" +
                    " values(@IndexNumber,@FirstName,@LastName,@BirthDate,@IdEnrollment)";
                command.Parameters.AddWithValue("IndexNumber", request.IndexNumber);
                command.Parameters.AddWithValue("FirstName", request.FirstName);
                command.Parameters.AddWithValue("LastName", request.LastName);
                command.Parameters.AddWithValue("BirthDate", request.BirthDate);
                command.Parameters.AddWithValue("IdEnrollment", enrollmentId);
                command.ExecuteNonQuery();
                return new EnrollStudentResponse(){IdEnrollment = enrollmentId,Semester = 1,IdStudy = idStudy,StartDate = startDate};
            }
            
        
    
        }

        public EnrollStudentResponse PromoteStudents(PromoteStudentRequest request)
        {
            using (var connection =
                new SqlConnection("Data Source=db-mssql;Initial Catalog=s18310;Integrated Security=True"))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();
                command.CommandText = "exec PromoteStudents @Studies,@Semester";
                command.Parameters.AddWithValue("Studies", request.Studies);
                command.Parameters.AddWithValue("Semester", request.Semester);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    EnrollStudentResponse response = new EnrollStudentResponse
                    {
                        IdEnrollment = (int)reader["IdEnrollment"],
                        Semester = (int)reader["Semester"],
                        IdStudy = (int)reader["IdStudy"],
                        StartDate=DateTime.Parse(reader["StartDate"].ToString())
                    };
                    return response;
                }
            }

            return null;


        }
    }
}