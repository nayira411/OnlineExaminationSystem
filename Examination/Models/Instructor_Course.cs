#nullable disable
using System;
using System.Collections.Generic;

namespace Examination.Models;

public partial class Instructor_Course
{
	public int InsId { get; set; }
	public int CrId { get; set; }
	public int TId { get; set; }
	public virtual Course Cr { get; set; }
	public virtual Instructor Ins { get; set; }
	public virtual Track TIdNavigation { get; set; }
}