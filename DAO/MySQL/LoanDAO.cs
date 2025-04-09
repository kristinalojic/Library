using Library.Models.Entities;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library.DAO.MySQL
{
    class LoanDAO : ILoan
    {
        public async Task<bool> AddLoanAsync(Loan loan)
        {
            using (var _db = new LibraryDbContext())
            {
                if (_db.Loans.Any(l => l.Member == loan.Member && l.Book == loan.Book && (l.IsReturned == false || l.IssueDate == loan.IssueDate)))
                    return false;
                _db.Loans.Add(loan);
                int affectedRows = await _db.SaveChangesAsync();

                return affectedRows > 0;
            }
        }

        public async Task<List<Loan>> GetLoansByBookIdAsync(int id)
        {
            using (var _db = new LibraryDbContext())
            {
                return await _db.Loans.Include(l => l.MemberNavigation).Where(l => l.Book == id).ToListAsync();
            }
        }

        public async Task<List<Loan>> GetLoansByMemberAsync(Member member)
        {
            using (var _db = new LibraryDbContext())
            {
                return await _db.Loans.Include(l => l.MemberNavigation).Include(l => l.BookNavigation).Where(l => l.Member == member.MembershipCardNumber).ToListAsync();
            }
        }

        public async Task<bool> UpdateLoanAsync(Loan loan)
        {
            using (var _db = new LibraryDbContext())
            {
                var lo = await _db.Loans.FirstOrDefaultAsync(l => l.Book == loan.Book && l.Member == loan.Member && l.IssueDate == loan.IssueDate);

                if (lo != null)
                {
                    lo.DueDate = loan.DueDate;
                    lo.IsReturned = loan.IsReturned; 
                    lo.HasBeenExtended = loan.HasBeenExtended;

                    int affectedRows = await _db.SaveChangesAsync();
                    return affectedRows > 0;
                }

                return false; 
            }
        }

    }
}
