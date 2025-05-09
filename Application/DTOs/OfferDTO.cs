using System.ComponentModel.DataAnnotations;
namespace FreelancePlatform.Application.Dtos;

    public class OfferDto
    {
        [Required]
        public int JobId { get; set; }

        [Required]
        [Range(1, 100000)]
        [DataType(DataType.Currency)]
        public decimal BidAmount { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 20)]
        public string CoverLetter { get; set; }
    }
