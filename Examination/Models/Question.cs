﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examination.Models;

public partial class Question
{
    public int QId { get; set; }

    [Required(ErrorMessage = "Question body is required")]

    public string Qbody { get; set; }

    [Required(ErrorMessage = "Question type is required")]
    public string Qtype { get; set; }

    [Required(ErrorMessage = "Question mark is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Question mark must be greater than 0")]
    public int Qmark { get; set; }

    [Required(ErrorMessage = "Course is required")]
    public int CrId { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Course Cr { get; set; }

    public virtual ICollection<Student_Answer> Student_Answers { get; set; } = new List<Student_Answer>();

    public virtual ICollection<Exam> EIds { get; set; } = new List<Exam>();
}