using System;
using System.Collections.Generic;

namespace Examination.Models;

public partial class Question
{
    public int Qid { get; set; }

    public string Qbody { get; set; } = null!;

    public string Qtype { get; set; } = null!;

    public int Qmark { get; set; }

    public int CrId { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual Course Cr { get; set; } = null!;

    public virtual ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();

    public virtual ICollection<Exam> Eids { get; set; } = new List<Exam>();
}
