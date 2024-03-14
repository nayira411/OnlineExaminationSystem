using System;
using System.Collections.Generic;

namespace Examination.Models;

public partial class Answer
{
    public int AnswerId { get; set; }

    public string Answerbody { get; set; } = null!;

    public int Qid { get; set; }

    public bool IsCorrect { get; set; }

    public virtual Question QidNavigation { get; set; } = null!;
}
