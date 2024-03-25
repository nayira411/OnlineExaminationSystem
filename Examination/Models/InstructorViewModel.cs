namespace Examination.Models
{
    public class InstructorViewModel
    {
        public Instructor intstrutor { get; set; }
        public List<Track> AllTracks { get; set; } = new List<Track>() { };
        public List<Course> AllCourses { get; set; } = new List<Course>() { };
        public Dictionary<Track, List<Course>> CoursesByTrack { get; set; } // New property

         public List<int> SelectedTracks { get; set; }
         public List<int> SelectedCourses { get; set; }
    }
}
