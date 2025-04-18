﻿using System;
using System.Collections.Generic;

namespace Library.Models.Entities;

public partial class Loan
{
    public int Book { get; set; }

    public int Member { get; set; }

    public DateOnly IssueDate { get; set; }

    public DateOnly DueDate { get; set; }

    public bool HasBeenExtended { get; set; }

    public bool IsReturned { get; set; }

    public virtual Book BookNavigation { get; set; } = null!;

    public virtual Member MemberNavigation { get; set; } = null!;

    public string FullName => $"{MemberNavigation.Name} {MemberNavigation.Surname}";
}
