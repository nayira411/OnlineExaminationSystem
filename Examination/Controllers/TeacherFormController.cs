﻿using Examination.Models;
using Examination.Repo;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Examination.Controllers
{
	public class TeacherFormController : Controller
	{
		int insId = 2;//will be from db

		TeacherFormRepo repo = new TeacherFormRepo();
		public IActionResult Index()
		{
			ViewBag.TeacherCourses = repo.GetInsCourses(insId);
			ViewBag.TeacherTracks = repo.GetInsTracks(insId);
			return View();
		}
		[HttpPost]
		public IActionResult CreateExam(string CourseName, int TrackId, int NoOfMCQ, int NoOfTF, DateTime date, string start, string end, int insId)
		{
			ViewBag.TeacherCourses = repo.GetInsCourses(insId);
			ViewBag.TeacherTracks = repo.GetInsTracks(insId);
			var res = repo.GenerateExam(CourseName, TrackId, NoOfMCQ, NoOfTF, date, start, end, insId);
			if (res == 1)
			{
				TempData["ErrorMessage"] = "Saved Correctly";
				TempData["ShowAlert"] = true;
				return RedirectToAction("confirmation", "teacherForm");
			}
			else
			{
				TempData["ErrorMessage"] = "Sorry, there is already an exam scheduled for this track and course on that day.";
				TempData["ShowAlert"] = true;
				return View("index");
			}
		}
		public IActionResult confirmation()
		{
			return View();
		}
		public IActionResult ShowTeacherCourses()
		{
			ViewBag.TeacherCourses = repo.GetInsCourses(insId);
			return View();
		}
		public IActionResult ShowInstructorStudents(int? Id)
		{
			if (Id == null)
			{
				return BadRequest();
			}
			ViewBag.students = repo.GetStudentsWithInstructor(Id.Value);
			return View();
		}
        [HttpPost]
        public IActionResult EditDegree(List<int> degrees, List<int> studentIds,int crId)
        {
            for (int i = 0; i < studentIds.Count; i++)
            {
                int studentId = studentIds[i];
                int degree = degrees[i];

                Console.WriteLine("Student ID: " + studentId);
                Console.WriteLine("Degree: " + degree);

               repo.UpdateDegree(studentId, degree,crId);
            }
            return RedirectToAction("ShowTeacherCourses");
        }
		public IActionResult TeacherHomePage()
		{
            var courses = repo.GetInsCourses(insId);
            List<string> StudentsNames = new List<string>();
            List<int> stdsDegrees = new List<int>();
            foreach (var crs in courses)
            {
                foreach (var studentCourse in crs.Student_Courses)
                {
                        StudentsNames.Add(studentCourse.SIdNavigation.Sname+"-"+ studentCourse.Cr.Cname); 
                        stdsDegrees.Add(studentCourse.grade.Value);
                }
            }
            var viewModel = new ChartViewModel
            {
                Labels = StudentsNames.ToArray(),
                Data = stdsDegrees.ToArray()
            };
            ViewBag.TeacherCourses = repo.GetInsCourses(insId);
            ViewBag.success = repo.CalculateSuccessPercentage(insId);
            return View(viewModel);
		}
		

    }
}
