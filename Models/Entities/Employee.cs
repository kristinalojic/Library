using System;
using System.Collections.Generic;

namespace Library.Models.Entities;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsAcive { get; set; }

    public sbyte AccountType { get; set; }

    public string Jbmg { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual Setting? Setting { get; set; }
}
