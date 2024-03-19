﻿using Examination.Data;
using Examination.Models;
using Microsoft.Build.ObjectModelRemoting;
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
			var res = context.Topics.AsNoTracking().FirstOrDefault(a => a.TopicId == id);
			return res;
		}
		public List<Topic> GetAllTopics()
		{
			var topics = context.Topics.AsNoTracking().ToList();
			var uniqueTopics = new List<Topic>();
			var topicNamesSet = new HashSet<string>();

			foreach (var topic in topics)
			{
				if (topicNamesSet.Add(topic.TopicName))
				{
					uniqueTopics.Add(topic);
				}
			}

			return uniqueTopics;
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
		public List<Track> GetAllTracks()
		{
			return context.Tracks.ToList();
		}
		public int DeleteCourseById2(int crsId)
		{
			int hasRelatedEntities = context.Database.ExecuteSqlRaw("exec ExistenceOfStds @crsId={0}", crsId);
			if (hasRelatedEntities == -1)
			{
				return 0;
			}
			else 
			{
				context.Courses.FromSqlRaw("exec [DeleteCourse] @crsId={0}", crsId).ToList();
				context.SaveChanges();
				return 1;
			}
		}
		public int AddCourse(string CourseName, int passGrade)
		{
			var insertedCourseIdParam = new SqlParameter("@InsertedCourseId", SqlDbType.Int)
			{
				Direction = ParameterDirection.Output
			};
			context.Database.ExecuteSqlRaw("exec AddCourse @crsName={0}, @passGrade={1}, @InsertedCourseId={2} OUTPUT",
											CourseName, passGrade, insertedCourseIdParam);
			context.SaveChanges();
			int insertedCourseId = (int)insertedCourseIdParam.Value;
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
			var courseTopics = context.Topics.FromSqlRaw("exec GetCourseTopics @CourseId={0}", crsId).ToList();
			return courseTopics;
		}
		public void AssociateTopicWithCourse(int topicId, string topicname, int courseId)
		{
			var courseTopic = new Topic
			{
				TopicId = topicId,
				TopicName = topicname,
				CrId = courseId
			};
			context.Topics.Add(courseTopic);
			context.SaveChanges();
		}
		public void AddCourseToTrack(int Tid, int crId)
		{
			var track = new TrackCourse { Tid = Tid, Crid = crId };
			context.TrackCourses.Add(track);
			context.SaveChanges();
		}
		public bool ISCourseUnique(string courseName)
		{
			return !context.Courses.Any(a => a.Cname == courseName);
		}
	}
}
