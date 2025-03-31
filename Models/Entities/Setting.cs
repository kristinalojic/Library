using System;
using System.Collections.Generic;

namespace Library.Models.Entities;

public partial class Setting
{
    public string Theme { get; set; } = null!;

    public string Language { get; set; } = null!;

    public string Color { get; set; } = null!;

    public int Employee { get; set; }

    public virtual Employee EmployeeNavigation { get; set; } = null!;
}
