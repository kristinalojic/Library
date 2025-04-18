﻿using Library.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAO
{
    interface IMember
    {
        Task<List<Member>> GetAllMembersAsync();

        Task<bool> AddMemberAsync(Member member);

        Task<bool> UpdateMemberAsync(Member member);

        Task<bool> UpdateMemberWithFeeAsync(Member member);
    }
}
