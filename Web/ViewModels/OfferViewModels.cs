// FreelancePlatform.Web/ViewModels/ProposalViewModels.cs
using System.ComponentModel.DataAnnotations;

namespace FreelancePlatform.Web.ViewModels
{
    public class OfferViewModel
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }

        [Required]
        [Range(1, 100000)]
        [DataType(DataType.Currency)]
        [Display(Name = "Your Bid Amount")]
        public decimal BidAmount { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 20)]
        [Display(Name = "Cover Letter")]
        public string CoverLetter { get; set; }
    }
}