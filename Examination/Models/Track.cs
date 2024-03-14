using System;
using System.Collections.Generic;

namespace Examination.Models;

public partial class Track
{
    public int Tid { get; set; }

    public string Tname { get; set; } = null!;

    public int SupervisorId { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual Instructor Supervisor { get; set; } = null!;
}
