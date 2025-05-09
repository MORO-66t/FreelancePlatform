using FreelancePlatform.Domain.Common;
using FreelancePlatform.Domain.Enums;
namespace FreelancePlatform.Domain.Entities;
public class Offer : BaseEntity
{
    public int Id { get; set; }
    public int FreelancerId { get; set; }
    public User Freelancer { get; set; }
    public int JobId { get; set; }
    public Job Job { get; set; }
    public decimal BidAmount { get; set; }
    public string CoverLetter { get; set; }
    public OfferStatus Status { get; set; } = OfferStatus.Pending;
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
}
