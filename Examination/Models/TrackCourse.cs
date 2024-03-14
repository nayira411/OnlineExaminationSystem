using System;
using System.Collections.Generic;

namespace Examination.Models;

public partial class TrackCourse
{
    public int Tid { get; set; }

    public int Crid { get; set; }

    public virtual Course Cr { get; set; } = null!;
}
