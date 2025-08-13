using LibraryAppWebAPI.Data;
using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class MemberRepository(LibraryDbContext context) : IMemberRepository
{
    private readonly LibraryDbContext _context = context;

    public async Task<IEnumerable<Member>> GetAllAsync()
    {
        return await _context.Members.ToListAsync();
    }

    public async Task<Member?> GetByIdAsync(int id)
    {
        return await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Member?> GetByMembershipIdAsync(Guid membershipId)
    {
        return await _context.Members.FirstOrDefaultAsync(m => m.MembershipId == membershipId);
    }

    public async Task<IEnumerable<Member>> SearchByNameAsync(string name)
    {
        return await _context.Members.ToListAsync();
    }

    public async Task<Member> AddAsync(Member member)
    {
        _context.Members.Add(member);
        await _context.SaveChangesAsync();
        return member;
    }

    public async Task<Member?> UpdateAsync(Member member)
    {
        var existingMember = await _context.Members.FindAsync(member.Id);
        if (existingMember == null)
            return null;

        existingMember.Id = member.Id;
        existingMember.IsActive = member.IsActive;
        existingMember.Name = member.Name;

        await _context.SaveChangesAsync();
        return existingMember;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var member = await _context.Members.FindAsync(id);
        if (member == null)
            return false;

        member.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Members.AnyAsync(m => m.Id == id);
    }
}
