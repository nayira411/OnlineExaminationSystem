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
		public int GenerateExam(string CourseName,int TrackId,int NoOfMCQ,int NoOfTF,DateTime date,string start,string end,int insId)
		{
			var existingExam=context.Exams.FirstOrDefault(a=>a.InsId==insId 
			&& a.TId==TrackId && a.CrId.ToString()==CourseName &&a.ExamDate.Day==date.Day);
            Console.WriteLine(existingExam.CrId);
            Console.WriteLine("==============================================");
			if(existingExam!=null)
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
			var res = context.Student_Courses.Where(a=>a.CrId==id).ToList();
			return res;
		}
		public void UpdateDegree(int id,int degree,int crId)
		{
			var res = context.Student_Courses.FirstOrDefault(a=>a.SId==id && a.CrId==crId);
			res.degree = degree;
			context.SaveChanges();
            Console.WriteLine("updated successfully !");
        }
	
    }
}
