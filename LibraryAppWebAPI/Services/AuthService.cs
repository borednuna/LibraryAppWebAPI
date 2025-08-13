namespace LibraryAppWebAPI.Services;

using LibraryAppWebAPI.Models;
using LibraryAppWebAPI.Repositories.Interfaces;
using LibraryAppWebAPI.Services.Interfaces;

public class AuthService(IMemberRepository memberRepository) : IAuthService
{
    private readonly IMemberRepository _memberRepository = memberRepository;

    public async Task<Member?> LoginAsync(string email, string password)
    {
        var members = await _memberRepository.GetAllAsync();
        var user = members.FirstOrDefault(m => 
            m.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            return user;

        return null;
    }

    public async Task<Member> SignUpAsync(string name, string email, string password, DateTime membershipDate)
    {
        if (await IsEmailRegisteredAsync(email))
            throw new InvalidOperationException("Email is already registered.");

        var member = new Member
        {
            Name = name,
            Email = email,
            Password = BCrypt.Net.BCrypt.HashPassword(password),
            MembershipDate = membershipDate,
            MembershipId = Guid.NewGuid(),
            IsActive = true
        };

        return await _memberRepository.AddAsync(member);
    }

    public async Task<bool> IsEmailRegisteredAsync(string email)
    {
        var members = await _memberRepository.GetAllAsync();
        return members.Any(m => m.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }
}
