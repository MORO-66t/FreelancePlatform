namespace Cloud.Application.Common.Interfaces;

public interface IAuthService
{
    Task<Guid?> Login(string email, string password);
}
