using Examination.Models;
using Microsoft.EntityFrameworkCore;

namespace Examination.Repo
{
    public interface IInstructorRepo
    {
        public List<Instructor> GetAllInstructors();
        public List<Instructor> AlternativeInstructors(int id);
        public List<Course> GetAllCourses();
        public List<Track> GetAllTarcks();
        public List<Track_Course> GetAllCoursesWithTracks();
        public void Add(Instructor ins);
        public Instructor GetInstructorById(int id);
        public Instructor_Course RemoveExistingAssociationRecord(int insId, int courseId, int trackId);
        public void RemoveFromInstructorCourse(Instructor_Course existingEntity);
        public void AddInstructorCoursesAndTrack(int isdId, int courseId, int trackId);
        public InstructorViewModel RetrieveAllRelatedDataAboutInstructor(int id);
        public void UpdeteInstructorData(Instructor OldData, Instructor NewData, int courseId, int trackId);
        Task UpdateImage(Instructor instructor, IFormFile Image);
        public bool DoesInstructorHaveCourses(int instructorId);
        public List<Course> GetInstructorCourses(int instructorId);

        public void RemoveInstructor(int instructorId);
        public List<(Course course, Track track)> GetInstructorCoursesWithTracks(int instructorId);
        public bool IsEmailValid(string email);
        public void RemoveAllInstructorCoursesAndTrackByTrackId(int trackId);



    }


    public class InstructorRepo : IInstructorRepo
    {
        ExamContext db;

        public InstructorRepo(ExamContext _db)
        {
            db = _db;
        }

        public List<Instructor> GetAllInstructors()
        {
            return db.Instructors.ToList();
        }

        /*==============GetTracksWithCourses=================*/
        public List<Course> GetAllCourses()
        {
            return db.Courses.ToList();
        }
        public List<Track_Course> GetAllCoursesWithTracks()
        {
            return db.Track_Courses.Include(tc => tc.Cr).ToList();
        }


        public List<Track> GetAllTarcks()
        {
            return db.Tracks.ToList();
        }

        public void Add(Instructor ins)
        {

            db.Instructors.Add(ins);
            db.SaveChanges();
            Console.WriteLine("-------------------Added------------------------");
        }
        public Instructor GetInstructorById(int id)
        {
            return db.Instructors.FirstOrDefault(a => a.InsId == id);
        }
        public Instructor_Course RemoveExistingAssociationRecord(int insId, int courseId, int trackId)
        {
            return db.Instructor_Courses.FirstOrDefault(ic => ic.InsId == insId && ic.CrId == courseId && ic.TId == trackId);

        }
        public void RemoveFromInstructorCourse(Instructor_Course existingEntity)
        {
            db.Instructor_Courses.Remove(existingEntity);
            db.SaveChanges();
        }
        public void AddInstructorCoursesAndTrack(int isdId, int courseId, int trackId)
        {
            // Check if there's already an existing assignment for the specified instructor, course, and track
            var existingAssignment = db.Instructor_Courses
                .FirstOrDefault(ic => ic.InsId == isdId && ic.CrId == courseId && ic.TId == trackId);

            if (existingAssignment != null)
            {
                return;
            }
            var instructorCourse = new Instructor_Course { InsId = isdId, CrId = courseId, TId = trackId };
            db.Instructor_Courses.Add(instructorCourse);
            db.SaveChanges();
        }
        public InstructorViewModel RetrieveAllRelatedDataAboutInstructor(int id)
        {
            var model = db.Instructors
                           .Include(ins => ins.Instructor_Courses)
                           .ThenInclude(ic => ic.Cr)
                           .ThenInclude(c => c.Track_Courses) // Include the Track associated with the Course
                           .FirstOrDefault(i => i.InsId == id);

            InstructorViewModel allData = new InstructorViewModel();
            if (model != null)
            {
                allData.intstrutor = new Instructor();
                allData.intstrutor.InsId = model.InsId;
                allData.intstrutor.Insname = model.Insname;
                allData.intstrutor.Insemail = model.Insemail;
                allData.intstrutor.Inspassword = model.Inspassword;
                allData.intstrutor.Insgender = model.Insgender;
                allData.intstrutor.Inssalary = model.Inssalary;
                allData.intstrutor.InsImg = model.InsImg;
            }
            else
            {
                throw new InvalidOperationException("Instructor not found");
            }

            allData.AllCourses = model.Instructor_Courses.Select(ic => ic.Cr).ToList();
            allData.AllTracks = model.Instructor_Courses.Where(ic => ic.TIdNavigation != null)
                                                        .Select(ic => ic.TIdNavigation)
                                                        .Distinct().ToList();
            allData.CoursesByTrack = new Dictionary<Track, List<Course>>();
            foreach (var track in allData.AllTracks)
            {
                var coursesInTrack = model.Instructor_Courses
                                           .Where(ic => ic.TId == track.TId)
                                           .Select(ic => ic.Cr)
                                           .ToList();

                allData.CoursesByTrack.Add(track, coursesInTrack);
            }

            return allData;
        }


        public void UpdeteInstructorData(Instructor OldData, Instructor NewData, int courseId, int trackId)
        {

            try
            {
                OldData.Insname = NewData.Insname;
                OldData.Insemail = NewData.Insemail;
                OldData.Inspassword = NewData.Inspassword;
                OldData.Insgender = NewData.Insgender;
                OldData.Inssalary = NewData.Inssalary;
               // OldData.InsImg = NewData.InsImg;

                // Update instructor's courses and tracks associations if needed
                // This part depends on your application logic for updating courses and tracks

                db.SaveChanges();
            }
            catch
            {
                throw;
            }
    
        }

       public async Task UpdateImage(Instructor instructor,IFormFile Image)
        {
            // Save the image file
            string fileName = $"{instructor.InsId}.{Image.FileName.Split('.').Last()}";
            using (var fileStream = new FileStream($"wwwroot/Images/{fileName}", FileMode.Create))
            {
                await Image.CopyToAsync(fileStream);
            }
            instructor.InsImg = fileName;
        }
  
    

        public bool DoesInstructorHaveCourses(int instructorId)
        {
            return db.Instructor_Courses.Any(ic => ic.InsId == instructorId);
        }
        public List<Instructor> AlternativeInstructors(int id)
        {
           return db.Instructors
                   .Where(i => i.InsId != id)
                   .ToList();
        }
        public List<Course> GetInstructorCourses(int instructorId)
        {
            return db.Instructor_Courses
                .Where(ic => ic.InsId == instructorId)
                .Select(ic => ic.Cr)
                 .Distinct()
                .ToList();
        }
        public void RemoveInstructor(int instructorId)
        {
            try
            {
                var instructor = db.Instructors
                                    .Include(i => i.Instructor_Courses)
                                    .Include(i => i.Tracks) // Include Tracks
                                    .FirstOrDefault(a => a.InsId == instructorId);

                // Check if the instructor exists
                if (instructor != null)
                {
                    foreach (var instructorCourse in instructor.Instructor_Courses.ToList())
                    {
                        instructor.Instructor_Courses.Remove(instructorCourse);
                        instructorCourse.Ins = null;
                        db.Instructor_Courses.Remove(instructorCourse);
                    }

                    db.Instructors.Remove(instructor);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw new Exception("An error occurred while removing the instructor.", ex);
            }
        }


        public List<(Course course, Track track)> GetInstructorCoursesWithTracks(int instructorId)
        {
            return db.Instructor_Courses
                .Where(ic => ic.InsId == instructorId)
                .Select(ic => new
                {
                    course = ic.Cr,
                    track = ic.TIdNavigation 
                })
                .AsEnumerable()
                .Select(x => (x.course, x.track))
                .Distinct()
                .ToList();
        }

        public bool IsEmailValid(string email)
        {

           return db.Instructors.Any(a=>a.Insemail!= email);
        }

        public void RemoveAllInstructorCoursesAndTrackByTrackId(int trackId)
        {
            var associationsToRemove = db.Instructor_Courses
                .Where(ict => ict.TId == trackId)
                .ToList();

            db.Instructor_Courses.RemoveRange(associationsToRemove);
            db.SaveChanges();
        }

    }
}
