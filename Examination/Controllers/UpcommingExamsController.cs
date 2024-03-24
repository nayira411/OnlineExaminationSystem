using CRUD.CustomFilters;
using Examination.Repo;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Examination.Controllers
{
	[AuthFilter]
	public class UpcommingExamsController : Controller
	{
		//this will be from auth data
		TeacherFormRepo repo = new TeacherFormRepo();
		public IActionResult Index()
		{
            var user = HttpContext.User;
            var studentId = user.FindFirst(c => c.Type == ClaimTypes.Sid).Value;
			var StdId = int.Parse(studentId);
            var StdTrack = repo.GetTrackIdOfStudent(StdId);
			var StdCourses = repo.GetStudentCourses(StdId);
			List<int> coursesIds = new List<int>();
			foreach (var Courses in StdCourses)
			{
				coursesIds.Add(Courses.CrId);
			}
			var data = repo.HaveExam(StdTrack, coursesIds);
			foreach (var Courses in data)
			{
                Console.WriteLine(Courses);
            }
		    ViewBag.exam= data;
			return View();
		}
	}
}
