using System;
using System.Collections.Generic;

namespace Examination.Models;

public partial class Admin : User
{
    public int Aid { get; set; }

    public string Aname { get; set; } = null!;

    public string Aemail { get; set; } = null!;

    public string Apassword { get; set; } = null!;
}
