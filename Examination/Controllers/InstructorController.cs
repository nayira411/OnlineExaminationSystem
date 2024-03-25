﻿using Examination.Models;
using Examination.Repo;
using Examination.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.DependencyResolver;

namespace Examination.Controllers
{
    public class InstructorController : Controller
    {
        private IInstructorRepo insRepo;
        private TrackCourseRepo crsRepo;

        public InstructorController(IInstructorRepo _insRepo, TrackCourseRepo _crsRepo)
        {
            insRepo = _insRepo;
            crsRepo = _crsRepo;
        }
        public IActionResult Index()
        {
            return View(insRepo.GetAllInstructors());
        }
		public IActionResult Create()
		{

			Dictionary<Track, List<Course>> trackCourses = crsRepo.GetTrackCoursesDictionary();
			ViewBag.TrackCourses = trackCourses;
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Create(Instructor instructor, Dictionary<int, int[]> selectedCoursesByTrack, IFormFile Image)
		{
			try
			{
				if (ModelState.IsValid)
				{
					if (insRepo.IsEmailValid(instructor.Insemail))
					{
						insRepo.Add(instructor);

						if (Image != null && Image.Length > 0)
						{
							string[] allowedExtensions = { ".png", ".jpeg", ".jpg" };
							var fileExtension = Path.GetExtension(Image.FileName).ToLower();
							if (!allowedExtensions.Contains(fileExtension))
							{
								ModelState.AddModelError("Image", "Invalid file format. Please upload a PNG, JPEG, or JPG file.");
								return View();
							}

							instructor = insRepo.GetInstructorById(instructor.InsId);

							string fileName = $"{instructor.InsId}.{fileExtension}";
							var filePath = Path.Combine("wwwroot/Images", fileName);
							using (var fileStream = new FileStream(filePath, FileMode.Create))
							{
								await Image.CopyToAsync(fileStream);
							}
							instructor.InsImg = fileName;
						}

						// Iterate over each track and its associated selected courses
						foreach (var kvp in selectedCoursesByTrack)
						{
							int trackId = kvp.Key;
							int[] selectedCourseIds = kvp.Value;

							foreach (var courseId in selectedCourseIds)
							{
								insRepo.AddInstructorCoursesAndTrack(instructor.InsId, courseId, trackId);
							}
						}

						return RedirectToAction("Index");
					}
					else
					{
						ViewBag.ErrorMsg = "Email Is Already Exist";
						return View();
					}
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "An error occurred while saving the instructor data.");
			}
			return View(instructor);
		}
		public IActionResult showDetails(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            InstructorViewModel model = insRepo.RetrieveAllRelatedDataAboutInstructor(id.Value);
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            InstructorViewModel model = insRepo.RetrieveAllRelatedDataAboutInstructor(id);
            Dictionary<Track, List<Course>> userChoices = model.CoursesByTrack;

            Dictionary<Track, List<Course>> trackCourses = crsRepo.GetTrackCoursesDictionary();
            Dictionary<Track, (List<Course> userChoices, List<Course> allCourses)> mergedData =
                trackCourses.ToDictionary(
                    kvp => kvp.Key,
                    kvp => (
                        userChoices.ContainsKey(kvp.Key) ? userChoices[kvp.Key] : new List<Course>(),
                        kvp.Value
                    )
                );

            ViewBag.TrackCourses = mergedData;

            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var instructor = insRepo.GetInstructorById(id);
            if (instructor == null)
                return BadRequest();
            if (insRepo.DoesInstructorHaveCourses(id) && instructor != null)
            {
                var alternativeInstructors = insRepo.AlternativeInstructors(instructor.InsId);

                ViewBag.AlternativeInstructors = new SelectList(alternativeInstructors, "InsId", "Insname");
                var coursesToReassign = insRepo.GetInstructorCourses(instructor.InsId);
                ViewBag.CoursesToReassign = new SelectList(coursesToReassign, "CrId", "Cname");

                return View(instructor);
            }
            if (instructor != null)
            {
                insRepo.RemoveInstructor(instructor.InsId);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id, int alternativeInstructorId)
        {
            var instructor = insRepo.GetInstructorById(id);
            if (instructor == null)
            {
                return RedirectToAction("Index");
            }

            var coursesWithTracksToReassign = insRepo.GetInstructorCoursesWithTracks(id);
            foreach (var courseWithTracks in coursesWithTracksToReassign)
            {
                var course = courseWithTracks.course;
                var track = courseWithTracks.track;

                insRepo.AddInstructorCoursesAndTrack(alternativeInstructorId, course.CrId, track.TId);
            }

            insRepo.RemoveInstructor(id);

            return RedirectToAction("Index");
        }




    }
}
