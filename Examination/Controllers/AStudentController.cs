using Examination.Models;
using Examination.ViewModel;

using Examination.Repo;
using Examination.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using CRUD.CustomFilters;

namespace Examination.Controllers
{
    [AuthFilter]
    public class AStudentController : Controller
    {

        private readonly AStudentRepo _stdrepo;

        public AStudentController(AStudentRepo stdrepo)
        {
            _stdrepo = stdrepo;
        }
        public IActionResult Display()
                                                                                                                                                     {
            try
            {
                var studentsWithTracks = _stdrepo.GetAllStudents();

                if (studentsWithTracks != null && studentsWithTracks.Any())
                {
                    int studentCount = studentsWithTracks.Count();
                    ViewBag.StudentCount = studentCount;

                    return View(studentsWithTracks);
                }
                else
                {
                    ViewBag.StudentCount = 0;
                    return View(new List<StudentCourseTrackModel>());
                }
            }
            catch (Exception ex)
            {
                ViewBag.StudentCount = 0;
                return View("Error");
            }
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            _stdrepo.Delete(id.Value);

            return RedirectToAction("Display");
        }
        public IActionResult GetStudentDetails(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var student = _stdrepo.GetStudentDetailsById(id.Value);
            var courses = _stdrepo.GetStudentCourses(id.Value);

            if (student == null)
            {
                return NotFound();
            }

            ViewBag.Courses = courses;
            return View(student);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var coursesName = _stdrepo.GetStudentCourses(id.Value);
            var studentedit = _stdrepo.GetStudentDetailsById(id.Value);

            if (studentedit == null)
            {
                return NotFound();
            }

            ViewBag.Courses = _stdrepo.GetCourses();
            ViewBag.Tracks = _stdrepo.GetTracks();
            ViewBag.CoursesName = coursesName;


            return View(studentedit);
        }

        [HttpPost]

        public IActionResult Edit(StudentCourseTrackModel model)
        {
            try
            {
                // Retrieve the student to update
                var studentToUpdate = _stdrepo.GetStudentById(model.SId);

                if (studentToUpdate == null)
                {
                    Console.WriteLine("Student not found.");
                    ViewBag.ErrorMessage = "Student not found.";
                    return RedirectToAction("Display");
                }

                // Update student information
                studentToUpdate.Sname = model.Sname;
                studentToUpdate.Semail = model.Semail;
                studentToUpdate.password = model.password;
                studentToUpdate.Sgender = model.Sgender;
                studentToUpdate.TrackId = model.TrackId;

                // Update courses for the student
                var studentCourse = new Student_Course { SId = model.SId, CrId = model.CourseId };
                studentToUpdate.Student_Courses = new List<Student_Course> { studentCourse };

                // Save changes to the database
                _stdrepo.SaveChanges();

                Console.WriteLine("Student details updated successfully.");
                ViewBag.ConfirmationMessage = "Student details updated successfully.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating student: {ex.Message}");
                ViewBag.ErrorMessage = "An error occurred while updating the student.";
            }

            return RedirectToAction("Display");
        }




        public IActionResult Add()
        {

            ViewBag.Courses = _stdrepo.GetCourses();
            ViewBag.Tracks = _stdrepo.GetTracks();
            return View(new Student());
        }

        [HttpPost]
        public IActionResult Add(Student student)
        {
            try
            {
                _stdrepo.AddStudent(student.Sname, student.Semail, student.password, student.Sgender, student.TrackId);
                ViewBag.ConfirmationMessage = "Student details added successfully.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while adding the student.";

            }

            return RedirectToAction("Display");
        }

        public IActionResult GetStudetGrade(int? stdid)
        {
            try
            {
                if (stdid == null)
                {
                    return BadRequest("Student ID is missing.");
                }

                var student = _stdrepo.GetStudentDetailsById(stdid.Value);
                if (student == null)
                {
                    return NotFound("Student not found.");
                }

                var courses = _stdrepo.GetStudetGrade(stdid.Value);
                if (courses == null || courses.Count == 0)
                {
                    return NotFound("No courses found for the student.");
                }

                ViewBag.Courses = courses;
                return View(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



    }
}
