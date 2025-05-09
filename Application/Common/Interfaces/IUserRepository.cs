using FreelancePlatform.Domain.Entities;
public interface IUserRepository : IRepository<User>
{
    Task<User> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);

    Task<bool> UsernameExistsAsync(string username);
}