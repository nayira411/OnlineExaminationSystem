using System;
using System.Collections.Generic;

namespace Examination.Models;

public partial class Course
{
    public int CrId { get; set; }

    public string Cname { get; set; } = null!;

    public int Passgrade { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<TrackCourse> TrackCourses { get; set; } = new List<TrackCourse>();

    public virtual ICollection<Student> Sids { get; set; } = new List<Student>();
}
