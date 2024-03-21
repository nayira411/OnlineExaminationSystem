using Examination.Data;
using System.Collections.Generic;
using Examination.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Linq;
namespace Examination.Repo
{
    public class AStudentRepo
    {
        ExamContext Context = new ExamContext();

        public List<Student> GetAllAtudents()
        {
            var students = Context.Students.FromSqlRaw("EXEC GetStudentsAndCourses").ToList();
            return students;
        }

        public void Delete(int id)
        {

            Context.Database.ExecuteSqlRaw("EXEC DeleteStudent @stId", new SqlParameter("@stId", id));
            Context.SaveChanges();
        }
        public Student GetStudentDetailsById(int studentId)
        {
            var studentDetails = Context.Students.FromSqlRaw("EXEC GetStudentsAndCourses").ToList()
                 .FirstOrDefault(s => s.Sid == studentId);

            return studentDetails;

        }
        public List<Course> GetCourses()
        {
            
            return Context.Courses.ToList();
        }

        public List<Track> GetTracks()
        {
            return Context.Tracks.ToList();
        }
        public void UpdateStudent(int studentId, string name, string email, string password, string gender, int trackId, int courseId)
        {
            try
            {
                Context.Database.ExecuteSqlRaw(
                    "EXEC UpdateStudent @stId, @stName, @stEmail, @stPassword, @stGender, @TrackId, @crsId",
                    new SqlParameter("@stId", studentId),
                    new SqlParameter("@stName", name),
                    new SqlParameter("@stEmail", email),
                    new SqlParameter("@stPassword", password),
                    new SqlParameter("@stGender", gender),
                    new SqlParameter("@TrackId", trackId),
                    new SqlParameter("@crsId", courseId)
                );

               
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating student: {ex.Message}");
                throw;
            }
        }



        public string AddStudent(string name, string email, string password, string gender, int trackId)
        {
            if (Context.Students.Count() == 8)
            {
                return "Can't Add";
            }
            else
            {
                try
                {
                    Context.Database.ExecuteSqlRaw(
                        "EXEC AddStudent @stName, @stEmail, @stPassword, @stGender, @TrackId",
                        new SqlParameter("@stName", name),
                        new SqlParameter("@stEmail", email),
                        new SqlParameter("@stPassword", password),
                        new SqlParameter("@stGender", gender),
                        new SqlParameter("@TrackId", trackId)
                    );

                    Context.SaveChanges();

                    return "Student added successfully";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding student: {ex.Message}");
                    throw;
                }

            }

         
            
        }


    
    public int GetStudentCount()
        {
            try
            {
                var count = Context.Students.Count();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting student count: {ex.Message}");
                throw;
            }
        }








    }
}
