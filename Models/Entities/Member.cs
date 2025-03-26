using System;
using System.Collections.Generic;

namespace Library.Models.Entities;

public partial class Member
{
    public int MembershipCardNumber { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public virtual ICollection<MembershipFee> MembershipFees { get; set; } = new List<MembershipFee>();
}
