
using FreelancePlatform.Domain.Enums;
using System.ComponentModel.DataAnnotations;
namespace FreelancePlatform.Domain.Entities;
public class Job
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Budget { get; set; }
    public DateTime Deadline { get; set; }
    public int ClientId { get; set; }
    public User Client { get; set; }
     [Required]
    [StringLength(500)]
    public string RequiredSkills { get; set; } = string.Empty;
    public JobStatus Status { get; set; } = JobStatus.Open;
    public ICollection<Offer> Proposals { get; set; }
    public DateTime CreatedAt { get; set; }
}
