namespace LibraryAppWebAPI.Repositories.Interfaces;

using LibraryAppWebAPI.Models;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllAsync();
    Task<Member?> GetByIdAsync(int id);
    Task<Member?> GetByMembershipIdAsync(Guid membershipId);
    Task<IEnumerable<Member>> SearchByNameAsync(string name);
    Task<Member> AddAsync(Member member);
    Task<Member?> UpdateAsync(Member member);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
