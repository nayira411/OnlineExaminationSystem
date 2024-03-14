using System;
using System.Collections.Generic;

namespace Examination.Models;

public partial class StudentAnswer
{
    public int Sid { get; set; }

    public int Eid { get; set; }

    public int Qid { get; set; }

    public string Sanswer { get; set; } = null!;

    public virtual Exam EidNavigation { get; set; } = null!;

    public virtual Question QidNavigation { get; set; } = null!;

    public virtual Student SidNavigation { get; set; } = null!;
}
