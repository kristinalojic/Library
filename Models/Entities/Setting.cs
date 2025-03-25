using System;
using System.Collections.Generic;

namespace Library.Models.Entities;

public partial class Setting
{
    public int Id { get; set; }

    public string Theme { get; set; } = null!;

    public string Language { get; set; } = null!;

    public string Color { get; set; } = null!;

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }
}
