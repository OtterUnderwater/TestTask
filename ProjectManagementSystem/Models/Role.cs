using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Role1 { get; set; } = null!;

    public virtual Employee? Employee { get; set; }
}
