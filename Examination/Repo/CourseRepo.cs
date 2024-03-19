using Examination.Data;
using Examination.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Examination.Repo
{
	public class CourseRepo
	{
		ExamContext context = new ExamContext();
		public List<Course> GetAllCourses()
		{
			var courses = context.Courses.FromSqlRaw("exec GetAllCourses").ToList();//remove as
			return courses;
		}
		public int getCoursesCount()
		{
			int count = context.Courses.Count();
			return count;
		}
		public Course GetCourseByID(int id)
		{
			var res = context.Courses.SingleOrDefault(x => x.CrId == id);
			return res;
		}
		public List<Topic> getTopics(int id)
		{
			var res = context.Topics.Where(a => a.CrId == id).ToList();
			return res;
		}
		public Topic GetTopicById(int id)
		{
			var res = context.Topics.SingleOrDefault(a => a.CrId == id);
			return res;
		}
		public List<Topic> getAllTopics()
		{
			return context.Topics.ToList();
		}
		public void AddTopicsToClass(Topic topic)
		{
			context.Topics.Add(topic);
			context.SaveChanges();
		}
		public Course GetCourseById(int crsId)
		{
			var courseDetails = context.Courses.FirstOrDefault(a => a.CrId == crsId && a.TrackCourses.Any(tc => tc.Crid == crsId));
			return courseDetails;
		}
		public void DeleteCourseById(int crsId)
		{
			var res = context.Courses.Include(a => a.Sids).FirstOrDefault(a => a.CrId == crsId);
			context.Courses.Remove(res);
			context.SaveChanges();
		}
		public int DeleteCourseById2(int crsId)
		{
			bool hasRelatedEntities = context.TrackCourses.Any(tc => tc.Crid == crsId) ||
				context.InstructorCourses.Any(a => a.CrId == crsId);
			if (hasRelatedEntities)
			{
				return -1;
			}
			else
			{
				var courses = context.Courses.FromSqlRaw("exec [DeleteCourse] @crsId={0}", crsId).ToList();
				context.SaveChanges();
				return 1;
			}

		}

		public int AddCourse(string CourseName, int passGrade)
		{
			// Create a SqlParameter to capture the output value of the stored procedure
			var insertedCourseIdParam = new SqlParameter("@InsertedCourseId", SqlDbType.Int)
			{
				Direction = ParameterDirection.Output
			};

			// Execute the stored procedure
			context.Database.ExecuteSqlRaw("exec AddCourse @crsName={0}, @passGrade={1}, @InsertedCourseId={2} OUTPUT",
											CourseName, passGrade, insertedCourseIdParam);

			// Save changes to the context
			context.SaveChanges();

			// Retrieve the ID of the inserted course from the output parameter
			int insertedCourseId = (int)insertedCourseIdParam.Value;

			// Return the ID of the inserted course
			return insertedCourseId;
		}


		//public int AddCourse(string CourseName, int passGrade)
		//{
		//	int x;
		//	var result = context.Database.ExecuteSqlRaw("exec AddCourse @crsName={0}, @passGrade={1},@InsertedCourseId={2}", CourseName, passGrade,x);
		//	context.SaveChanges();
		//	return result;
		//}
		public void UpdateCourse(int crsId, string crsName, int passGrade)
		{
			context.Database.ExecuteSqlRaw("exec UpdateCourse @crsId={0}, @crsName={1}, @passGrade={2}", crsId, crsName, passGrade);
			Console.WriteLine(crsName);
			context.SaveChanges();
		}
		public List<Topic> getCourseTopics(int? crsId)
		{
			var courseTopics =context.Topics.FromSqlRaw("exec GetCourseTopics @CourseId={0}", crsId).ToList();
			return courseTopics;
		}
		public void AssociateTopicWithCourse(int topicId,string topicname, int courseId)
		{
			var courseTopic = new Topic
			{
				TopicId = topicId,
				CrId = courseId,
				TopicName= topicname
			};
			context.Topics.Add(courseTopic);
			context.SaveChanges();
		}

		public bool ISCourseUnique(string courseName)
		{
			return !context.Courses.Any(a => a.Cname == courseName);
		}
	}
}
