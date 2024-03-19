using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examination.Models;


public partial class Course
{
    public int CrId { get; set; }
    [Required]
    [StringLength(10,MinimumLength =1)]//letters only
	[RegularExpression(@"^[A-Za-z]{2,10}$", ErrorMessage = "Cname must contain only letters and have a length between 1 and 10 characters.")]
	public string Cname { get; set; } = null!;
    [Required]
    [Range(50,100)]
    public int Passgrade { get; set; }
    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<TrackCourse> TrackCourses { get; set; } = new List<TrackCourse>();

    public virtual ICollection<Student> Sids { get; set; } = new List<Student>();
}
