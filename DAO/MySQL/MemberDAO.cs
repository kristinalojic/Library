using Library.Models.Entities;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace Library.DAO.MySQL
{
    class MemberDAO : IMember
    {
        public async Task<bool> AddMemberAsync(Member member)
        {
            using (var _db = new LibraryDbContext())
            {
                if (_db.Members.Any(m => m.Phone == member.Phone || m.Email == member.Email))
                    return false;
                _db.Members.Add(member);
                int affectedRows = await _db.SaveChangesAsync();

                if (affectedRows > 0)
                {
                    var m = _db.Members.FirstOrDefault(m => m.Phone == member.Phone);
                    var MembershipFee = new MembershipFee
                    {
                        Member = m.MembershipCardNumber,
                        Issuence = DateOnly.FromDateTime(DateTime.Today),
                        Expiration = DateOnly.FromDateTime(DateTime.Today.AddDays(365))
                    };
                    _db.MembershipFees.Add(MembershipFee);
                    await _db.SaveChangesAsync();
                }

                return affectedRows > 0;
            }
        }

        public async Task<List<Member>> GetAllMembersAsync()
        {
            using (var _db = new LibraryDbContext())
            {
                return await _db.Members.Include(m => m.MembershipFee).ToListAsync();
            }
        }

        public async Task<bool> UpdateMemberAsync(Member member)
        {
            using (var _context = new LibraryDbContext())
            {
                try
                {
                    var memb = await _context.Members.FindAsync(member.MembershipCardNumber);

                    if (memb != null)
                    {
                        if (_context.Members.Any(m => (m.Phone == member.Phone && m.MembershipCardNumber != member.MembershipCardNumber) || (m.Email == member.Email && m.MembershipCardNumber != member.MembershipCardNumber)))
                            return false;
                        memb.Name = member.Name;
                        memb.Surname = member.Surname;
                        memb.Phone = member.Phone;
                        memb.Email = member.Email;
                        memb.Address = member.Address;
                        int affectedRows = await _context.SaveChangesAsync();
                        return affectedRows > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Došlo je do greške: {ex.Message}");
                }
                return false;
            }
        }

        public async Task<bool> UpdateMemberWithFeeAsync(Member member)
        {
            using (var _context = new LibraryDbContext())
            {
                try
                {
                    var memb = await _context.Members.FindAsync(member.MembershipCardNumber);

                    if (memb != null)
                    {
                        if (_context.Members.Any(m => (m.Phone == member.Phone && m.MembershipCardNumber != member.MembershipCardNumber) || (m.Email == member.Email && m.MembershipCardNumber != member.MembershipCardNumber)))
                            return false;
                        memb.Name = member.Name;
                        memb.Surname = member.Surname;
                        memb.Phone = member.Phone;
                        memb.Email = member.Email;
                        memb.Address = member.Address;
                        var fee = await _context.MembershipFees.FindAsync(member.MembershipCardNumber);
                        if (fee != null)
                        {
                            fee.Issuence = member.MembershipFee.Issuence;
                            fee.Expiration = member.MembershipFee.Expiration;
                            int affectedRows = await _context.SaveChangesAsync();
                            return affectedRows > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Došlo je do greške: {ex.Message}");
                }
                return false;
            }
        }
    }
}
