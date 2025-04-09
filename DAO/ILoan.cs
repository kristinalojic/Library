using Library.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAO
{
    interface ILoan
    {
        Task<bool> AddLoanAsync(Loan loan);

        Task<List<Loan>> GetLoansByBookIdAsync(int id);

        Task<List<Loan>> GetLoansByMemberAsync(Member member);

        Task<bool> UpdateLoanAsync(Loan loan);
    }
}
