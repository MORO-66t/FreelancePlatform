//using cloud.Application.Common.Models;
//using cloud.Domain.Common;
//namespace cloud.Application.Common.Interfaces;

//public interface IIdentityService
//{
//    Task<string?> GetUserNameAsync(string userId);

//    Task<bool> IsInRoleAsync(string userId, string role);

//    Task<bool> AuthorizeAsync(string userId, string policyName);

//    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

//    Task<Result> DeleteUserAsync(string userId);

//    Task<string> RegisterAsync(User user, string password);
//    Task<string> LoginAsync(string email, string password);
//    Task<User?> GetUserByIdAsync(Guid userId);
//}


//using cloud.Domain.Entities;
//using Cloud.Application.Common.Models.Security;

//namespace Cloud.Application.Common.Interfaces;

//public interface IIdentityService
//{
//    Task<string> GetUserNameAsync(string userId);
//    Task<bool> IsInRoleAsync(string userId, string role);
//    Task<bool> AuthorizeAsync(string userId, string policyName);
//    Task<AuthenticationResult> CreateUserAsync(string email, string password, string role);
//    Task<bool> DeleteUserAsync(string userId);

//    Task<AuthenticationResult> RegisterUserAsync(
//       string firstName,
//       string lastName,
//       string email,
//       string password,
//       string role,
//       string? companyName = null,
//       string? skills = null,
//       decimal? hourlyRate = null);

//    Task<AuthenticationResult> LoginAsync(string email, string password);
//    Task<bool> UserExistsAsync(string email);
//}
