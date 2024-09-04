﻿using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.Models;

public partial class Status
{
    public int Id { get; set; }

    public string Status1 { get; set; } = null!;

    public virtual WorkTask? WorkTask { get; set; }
}
