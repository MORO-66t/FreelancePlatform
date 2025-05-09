using FreelancePlatform.Domain.Enums;
namespace FreelancePlatform.Domain.Entities;
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public UserType Type { get; set; } // Client or Freelancer
    public string? Skills { get; set; } // For freelancers
    public string? Bio { get; set; }
    public DateTime CreatedAt { get; set; }
}