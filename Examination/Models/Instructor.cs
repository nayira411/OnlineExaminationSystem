using System;
using System.Collections.Generic;

namespace Examination.Models;

public partial class Instructor : User
{
    public int InsId { get; set; }

    public string Insname { get; set; } = null!;

    public string Insemail { get; set; } = null!;

    public string Inspassword { get; set; } = null!;

    public string Insgender { get; set; }

    public decimal Inssalary { get; set; }
    
    public string Image {  get; set; }
    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
