using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.Models;

public partial class WorkTask
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public Guid? IdEmployee { get; set; }

    public int IdStatus { get; set; }

    public virtual Employee? IdEmployeeNavigation { get; set; }

    public virtual Status IdStatusNavigation { get; set; } = null!;
}
