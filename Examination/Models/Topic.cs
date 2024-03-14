using System;
using System.Collections.Generic;

namespace Examination.Models;

public partial class Topic
{
    public int TopicId { get; set; }

    public string? TopicName { get; set; }

    public string? TopicDescription { get; set; }

    public int CrId { get; set; }
}
