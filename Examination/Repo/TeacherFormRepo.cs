using Examination.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.DependencyResolver;
using NuGet.Protocol;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Examination.Repo
{
	public class TeacherFormRepo
	{
		ExamContext context = new ExamContext();
		public List<Course> GetInsCourses(int? id)
		{
			var res = context.Courses.FromSqlRaw("exec GetInstructorCourses @InstructorId={0}", id).ToList();
			return res;
		}
		public List<Track> GetInsTracks(int? id)
		{
			var res = context.Tracks.FromSqlRaw("exec GetInsTracks @insId={0}", id).ToList();
			return res;
		}
		public int GenerateExam(string CourseName, int TrackId, int NoOfMCQ, int NoOfTF, DateTime date, string start, string end, int insId)
		{
			var existingExam = context.Exams.FirstOrDefault(a => a.InsId == insId
			&& a.TId == TrackId && a.CrId.ToString() == CourseName && a.ExamDate.Day == date.Day);
			Console.WriteLine("==============================================");
			if (existingExam != null)
			{
				return -1;
			}
			else
			{
				var res = context.Database.ExecuteSqlRaw("exec GenerateExam @NoOfMCQ={0},@NoOfTF={1} ,@CourseName={2} ,@Date={3},@start={4},@end={5},@insId={6},@TrackId={7}",
						NoOfMCQ, NoOfTF, CourseName, date, start, end, insId, TrackId);
				context.SaveChanges();
				Console.WriteLine("saved correctly");
				return 1;
			}
		}
		public List<Student_Course> GetStudentsWithInstructor(int id)
		{
			var res = context.Student_Courses.Where(a => a.CrId == id).ToList();
			return res;
		}
		public void UpdateDegree(int id, int degree, int crId)
		{
			var res = context.Student_Courses.FirstOrDefault(a => a.SId == id && a.CrId == crId);
			res.grade = degree;
			context.SaveChanges();
			Console.WriteLine("updated successfully !");
		}
		public double CalculateSuccessPercentage(int id)
		{
			var totalNumOfSuccess = 0;
			var res = GetInsCourses(id);
			int TotalStudents = 1;
			foreach (var a in res)
			{
				foreach (var studentCourse in a.Student_Courses)
				{
					TotalStudents++;
					if (studentCourse.grade > studentCourse.Cr.Passgrade)
					{
						totalNumOfSuccess++;
					}
				}
			}
			double successPercentage = ((double)totalNumOfSuccess / TotalStudents) * 100;
			double roundedSuccessPercentage = Math.Round(successPercentage, 1);
			return roundedSuccessPercentage;
		}
		public int GetTrackIdOfStudent(int id)
		{
			var res = context.Students.FirstOrDefault(a => a.SId == id);
			return res.TrackId;
		}
        public List<Student_Course> GetStudentCourses(int id)
        {
            var res = context.Student_Courses.Where(a => a.SId == id).ToList();
            if (res.Count > 0)
            {
                return res;
            }
            else
            {
               
                return new List<Student_Course>(); 
            }
        }

        public List<Exam> HaveExam(int tid, List<int> crsIds, int sid)
        {
            List<Exam> res = new List<Exam>();

            using (var context = new ExamContext())
            {
                foreach (var cId in crsIds)
                {
                    var stdExam = context.Exams
                        .Include(e => e.Cr) 
                        .Where(a => a.TId == tid && a.CrId == cId)
                        .ToList(); 

                    foreach (var sexam in stdExam)
                    {
                        var studentAnswers = context.Student_Answers.FirstOrDefault(sA => sA.SId == sid && sA.EId == sexam.EId);
                        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                        if (sexam?.ExamDate > today && studentAnswers == null)
                        {
                            res.Add(sexam);
                        }
                    }
                }
            }

            return res;
        }

        public List<Student_Course> GetStdDegrees(int id)
        {
            var Stdcourses = context.Student_Courses
                                .Where(s => s.SId == id && s.grade != null) 
                                .ToList();
            return Stdcourses;
        }

        public int getCountOfStdsCourses(int id)
        {
            var count = context.Student_Courses.Where(s => s.SId == id && s.grade != null).Count();
            return count;
        }
        public double CalculateGPA(List<int> degrees)
        {
            if (degrees == null || degrees.Count == 0)
                return 0;

            double totalDegrees = 0;
            foreach (var degree in degrees)
            {
                totalDegrees += degree;
            }


            double gpa = totalDegrees / degrees.Count;
            return gpa;
        }
    }
}
