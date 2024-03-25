using Examination.Models;

namespace Examination.ViewModel
{
    public class InstructorViewModel
    {
        public Instructor intstrutor { get; set; }
        public List<Track> AllTracks { get; set; } = new List<Track>() { };
        public List<Course> AllCourses { get; set; } = new List<Course>() { };
        public Dictionary<Track, List<Course>> CoursesByTrack { get; set; } 

        public List<int> SelectedTracks { get; set; }
        public List<int> SelectedCourses { get; set; }
    }
}
