// FreelancePlatform.Infrastructure/Services/AuthService.cs
using FreelancePlatform.Infrastructure.Repositories;

using FreelancePlatform.Domain.Entities;
using FreelancePlatform.Application.Common.Interfaces;
using FreelancePlatform.Domain.Exceptions;
using FreelancePlatform.Domain.Enums;


namespace FreelancePlatform.Infrastructure.Services
{
    public interface IAuthService 
    {
        Task<User> RegisterAsync(string email, string password, string username, UserType userType);
        Task<User> LoginAsync(string email, string password);
    }

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> RegisterAsync(string email, string password, string username, UserType userType)
    {
        if (await _userRepository.EmailExistsAsync(email))
            {
                throw new AppException("Email already exists");
            }

            if (await _userRepository.UsernameExistsAsync(username))
            {
                throw new AppException("Username already taken");
            }

        var user = new User
        {
            Email = email,
            Username = username,
            Type = userType,
            PasswordHash = _passwordHasher.HashPassword(password),
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);
        return user;
    }

    public async Task<User> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null || !_passwordHasher.VerifyPassword(user.PasswordHash, password))
            {
                throw new AppException("Invalid credentials");
            }

            return user;
        }
}
}



