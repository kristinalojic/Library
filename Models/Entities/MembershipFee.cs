using System;
using System.Collections.Generic;

namespace Library.Models.Entities;

public partial class MembershipFee
{
    public int Id { get; set; }

    public DateOnly Issuence { get; set; }

    public DateOnly Expiration { get; set; }

    public int Member { get; set; }

    public virtual Member MemberNavigation { get; set; } = null!;
}
