using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.Models;

public partial class Employee
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Login { get; set; } = null!;

    public int IdRole { get; set; }

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual ICollection<WorkTask> WorkTasks { get; set; } = new List<WorkTask>();
}
