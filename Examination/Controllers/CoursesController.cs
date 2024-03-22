using Examination.Data;
using Examination.Models;
using Examination.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Examination.Controllers
{
	public class CoursesController : Controller
	{
		CourseRepo repo = new CourseRepo();
		public IActionResult Index()
		{
			ViewBag.courseCount = repo.getCoursesCount();
			return View(repo.GetAllCourses());
		}
		public IActionResult Details(int id)
		{
			var res = repo.GetCourseByID(id);
			ViewBag.res = res;
			var topics = repo.getTopics(id);
			ViewBag.topics = topics;
			return View(res);
		}
		public IActionResult Delete(int id)
		{
			var res = repo.DeleteCourseById2(id);
			if (res == 0)
			{
				TempData["ErrorMessage"] = "Cannot delete course. Students are enrolled in this course.";
				TempData["ShowAlert"] = true;
				return RedirectToAction("Index");
			}
			else
			{
				TempData["ErrorMessage"] = "Deleted Successfully";
				TempData["ShowAlert"] = true;
				return RedirectToAction("Index");
			}
			return View();
		}

		[HttpPost]
		public IActionResult Delete(int? id)
		{
			repo.DeleteCourseById2(id.Value);
			return RedirectToAction("index");
		}

		public IActionResult AddCourse()
		{
			ViewBag.Tracks=repo.GetAllTracks();
			ViewBag.allTopics = repo.GetAllTopics();
			return View();
		}
		[HttpPost]
		public IActionResult AddCourse(Course crs, List<int> topics,int Tid)
		{
			ViewBag.Tracks = repo.GetAllTracks();
			ViewBag.allTopics = repo.GetAllTopics();
			if (ModelState.IsValid)
			{
				if (repo.ISCourseUnique(crs.Cname))
				{
					var courseId = repo.AddCourse(crs.Cname, crs.Passgrade);
					repo.AddCourseToTrack(Tid, courseId);	
					if (courseId > 0)
					{
						foreach (var topicId in topics)
						{
							var topic=repo.GetTopicById(topicId);
							var t_Name=topic.TopicName;
							repo.AssociateTopicWithCourse(topicId,t_Name ,courseId);
						}
					}
					return RedirectToAction("Index");
				}
				ModelState.AddModelError(nameof(Course.Cname), "Course name already exists");
			}
			return View(crs);
		}
	
		public IActionResult IsCouseUnique(string courseName)
		{
			if (repo.ISCourseUnique(courseName))
			{
				return Json(true);
			}
			else
			{
				return Json($"This Course Name Already Exists!");
			}

		}
		public IActionResult UpdateCourse(int? id)
		{
			var res = repo.GetCourseByID(id.Value);
			return View(res);
		}
		[HttpPost]
		public IActionResult UpdateCourse(Course crs, int? id)
		{
			crs.CrId = id.Value;
			if (ModelState.IsValid)
			{
				if (repo.ISCourseUnique(crs.Cname))
				{
					repo.UpdateCourse(crs.CrId, crs.Cname, crs.Passgrade);
					return RedirectToAction("index");
				}
				ModelState.AddModelError(nameof(Course.Cname), "Course name already exists");

			}
			return View(crs);
		}

	}
}
