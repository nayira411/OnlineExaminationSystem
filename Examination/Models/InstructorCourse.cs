using System;
using System.Collections.Generic;

namespace Examination.Models;

public partial class InstructorCourse
{
    public int InsId { get; set; }

    public int CrId { get; set; }

    public int? Tid { get; set; }
}
