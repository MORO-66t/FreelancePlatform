using FreelancePlatform.Domain.Common;
using FreelancePlatform.Domain.Enums;
namespace FreelancePlatform.Domain.Entities;
using FreelancePlatform.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Offer : BaseEntity
{
    public int Id { get; set; }
    
    [Required]
    public int FreelancerId { get; set; }
    
    [Required]
    public int JobId { get; set; }
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal BidAmount { get; set; }
    
    [Required]
    public string CoverLetter { get; set; } = string.Empty;
    
    [Required]
    public OfferStatus Status { get; set; } = OfferStatus.Pending;

    [Required]
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual User? Freelancer { get; set; }
    public virtual Job? Job { get; set; }
    // public int Id { get; set; }
    // public int FreelancerId { get; set; }
    // public User Freelancer { get; set; }
    // public int JobId { get; set; }
    // public Job Job { get; set; }
    // public decimal BidAmount { get; set; }
    // public string CoverLetter { get; set; }
    // public OfferStatus Status { get; set; } = OfferStatus.Pending;
    // public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
}
