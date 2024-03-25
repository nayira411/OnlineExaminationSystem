using CRUD.CustomFilters;
using Examination.Models;
using Examination.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Security.Claims;

namespace Examination.Controllers
{
    [AuthFilter]
    public class StudentController : Controller
    {
        IStudentRepo Srepo;
        int ExamID;
        int StudentID;
        public StudentController(IStudentRepo _StudRepo)
        {
            Srepo = _StudRepo;

        }

        [HttpGet]
        public IActionResult StartExam(int ExamId)
        {
            var user = HttpContext.User;
            var studentId = user.FindFirst(c => c.Type == ClaimTypes.Sid).Value;
            int sId = int.Parse(studentId);
            ViewBag.studentId = sId;
            ViewBag.ExamtId = ExamId;
            List<Question> questions = Srepo.GetExamQuestions(ExamId);
            ExamID = ExamId;
            ViewBag.questions = questions;
            ViewBag.ExamName = Srepo.GetCourseById(Srepo.GetExam(ExamId).CrId).Cname;
            ViewBag.NumberOfQuestion = questions.Count();
            TimeOnly endTime = Srepo.GetExam(ExamId).EndTime;
            TimeOnly startTime = Srepo.GetExam(ExamId).StartTime;
            TimeSpan examDuration = endTime - startTime;
            ViewBag.ExamDuration = examDuration.TotalMinutes;
            return View();
        }
        [HttpPost]
        public IActionResult StartExam(Dictionary<int, Student_Answer> answers)
        {
      
            Student_Answer std = new Student_Answer();
            foreach (var entry in answers)
            {
                std.QId = entry.Key;
                Student_Answer answer = entry.Value;
                std.SId = answer.SId;
                std.EId = answer.EId;
                std.SAnswer = answer.SAnswer ?? "0";
                Srepo.StudentAnswers(std);
            }
            int Eid = answers.Values.First().EId;
            int Sid = answers.Values.First().SId;

            return RedirectToAction("Result", new { Eid = Eid, Sid = Sid });
        }
        public IActionResult Result(int Eid, int Sid)
        {
            ViewBag.Score = Srepo.CalculateExamScore(Eid, Sid);
            ViewBag.TotalScore = Srepo.CalculateTotalExamScore(Eid);
            return View("Result");
        }
    }
}
