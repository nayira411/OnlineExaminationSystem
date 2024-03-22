using System;
using System.Collections.Generic;

namespace Examination.Models;

public partial class Student 
{
    public int Sid { get; set; }

    public string Sname { get; set; } = null!;

    public string Semail { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Sgender { get; set; }

    public int TrackId { get; set; }

    public virtual ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();

    public virtual Track Track { get; set; } = null!;

    public virtual ICollection<Course> Crs { get; set; } = new List<Course>();
}
