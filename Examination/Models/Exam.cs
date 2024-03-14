using System;
using System.Collections.Generic;

namespace Examination.Models;

public partial class Exam
{
    public int Eid { get; set; }

    public DateOnly ExamDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int CrId { get; set; }

    public int InsId { get; set; }

    public int? Tid { get; set; }

    public virtual Course Cr { get; set; } = null!;

    public virtual Instructor Ins { get; set; } = null!;

    public virtual ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();

    public virtual Track? TidNavigation { get; set; }

    public virtual ICollection<Question> Qids { get; set; } = new List<Question>();
}
