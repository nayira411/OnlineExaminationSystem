using System.Collections.Generic;
using Examination.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Examination.ViewModel;
using System.Data.Entity;
using Microsoft.Data.SqlClient;
namespace Examination.Repo
{
    public class AStudentRepo
    {
        ExamContext Context = new ExamContext();

        public List<StudentCourseTrackModel> GetAllStudents()
        {
            List<StudentCourseTrackModel> studentCourseTrackModels = new List<StudentCourseTrackModel>();
            List<Student> Students = Context.Students
             .FromSqlRaw("EXEC GETStudentsAndCourses").ToList();





            foreach (var Student in Students)
            {
                StudentCourseTrackModel stc = new StudentCourseTrackModel();
                stc.SId = Student.SId;
                stc.Sname = Student.Sname;
                stc.Sgender = Student.Sgender;
                stc.Semail = Student.Semail;
                stc.password = Student.password;
                stc.TrackName = Student.Track.Tname;
                stc.TrackId = Student.TrackId;
                foreach (Student_Course course in Student.Student_Courses)
                {
                    stc.CourseName = course.Cr.Cname;
                    stc.CourseId = course.CrId;

                }
                studentCourseTrackModels.Add(stc);
            }
            return studentCourseTrackModels;
        }
        public void Delete(int id)
        {
            Context.Database.ExecuteSqlRaw("EXEC DeleteStudent @stId", new Microsoft.Data.SqlClient.SqlParameter("@stId", id));
            Context.SaveChanges();
        }
        public StudentCourseTrackModel GetStudentDetailsById(int studentId)
        {
            Student Student = Context.Students
                .FromSqlRaw("EXEC GETStudentsAndCourses")
                .AsEnumerable()
                .FirstOrDefault(s => s.SId == studentId);


            StudentCourseTrackModel stc = new StudentCourseTrackModel();
            stc.SId = Student.SId;
            stc.Sname = Student.Sname;
            stc.Sgender = Student.Sgender;
            stc.Semail = Student.Semail;
            stc.password = Student.password;
            stc.TrackName = Student.Track.Tname;
            stc.TrackId = Student.TrackId;
            foreach (Student_Course course in Student.Student_Courses)
            {
                stc.CourseName = course.Cr.Cname;
                stc.CourseId = course.CrId;

            }
            return stc;
        }
        public List<Course> GetStudentCourses(int studentId)
        {
            var studentCourses = Context.Set<Course>()
                .FromSqlRaw("EXEC GetStudentCourses @stId", new Microsoft.Data.SqlClient.SqlParameter("@stId", studentId))
                .ToList();

            return studentCourses;
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
                    new System.Data.SqlClient.SqlParameter("@stId", studentId),
                    new System.Data.SqlClient.SqlParameter("@stName", name),
                    new System.Data.SqlClient.SqlParameter("@stEmail", email),
                    new System.Data.SqlClient.SqlParameter("@stPassword", password),
                    new System.Data.SqlClient.SqlParameter("@stGender", gender),
                    new System.Data.SqlClient.SqlParameter("@TrackId", trackId),
                    new System.Data.SqlClient.SqlParameter("@crsId", courseId)
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
            if (Context.Students.Count() == 25)
            {
                return "Can't Add";
            }
            else
            {
                try
                {
                    Context.Database.ExecuteSqlRaw(
                        "EXEC AddStudent @stName, @stEmail, @stPassword, @stGender, @TrackId",
                        new Microsoft.Data.SqlClient.SqlParameter("@stName", name),
                        new Microsoft.Data.SqlClient.SqlParameter("@stEmail", email),
                        new Microsoft.Data.SqlClient.SqlParameter("@stPassword", password),
                        new Microsoft.Data.SqlClient.SqlParameter("@stGender", gender),
                        new Microsoft.Data.SqlClient.SqlParameter("@TrackId", trackId)
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
        public List<StudentCourseModel> GetStudetGrade(int studentId)
        {
            var studentgrade = Context.Set<StudentCourseModel>()
                .FromSqlRaw("EXEC GetStudentGrade @StudentId = {0}", studentId)
                .ToList();

            return studentgrade;
        }


        public void UpdateStudentCourse(int studentId, int newCourseId)
{
    var student = Context.Students.SingleOrDefault(s => s.SId == studentId);

    if (student != null)
    {
        foreach (var studentCourse in student.Student_Courses)
        {
            studentCourse.CrId = newCourseId;
        }
        Context.SaveChanges();
    }
}

        public Student GetStudentById(int id)
        {
            return Context.Students.FirstOrDefault(s => s.SId == id);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }

}


