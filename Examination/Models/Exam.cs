﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Examination.Models;

public partial class Exam
{
    public int EId { get; set; }

    public DateOnly ExamDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int CrId { get; set; }

    public int InsId { get; set; }

    public int? TId { get; set; }

    public virtual Course Cr { get; set; }

    public virtual Instructor Ins { get; set; }

    public virtual ICollection<Student_Answer> Student_Answers { get; set; } = new List<Student_Answer>();

    public virtual Track TIdNavigation { get; set; }

    public virtual ICollection<Question> QIds { get; set; } = new List<Question>();
}