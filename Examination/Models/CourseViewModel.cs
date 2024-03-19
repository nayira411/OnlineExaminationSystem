namespace Examination.Models
{
	public class CourseViewModel
	{
		public Course course { get; set; }
		List<Instructor> instructors { get; set; }
		public TrackCourse trackCourse { get; set; }
	}
}
