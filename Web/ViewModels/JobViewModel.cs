//namespace Web.ViewModels;
// FreelancePlatform.Web/ViewModels/JobViewModels.cs
// FreelancePlatform.Web/ViewModels/JobViewModels.cs
using System;
using System.ComponentModel.DataAnnotations;
//using FreelancePlatform.Core.Entities;
using global::FreelancePlatform.Domain.Entities;

namespace FreelancePlatform.Web.ViewModels;

    public class JobViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 20)]
        public string Description { get; set; }

        [Required]
        [Range(1, 100000)]
        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Deadline")]
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "Please specify at least one required skill")]
    [Display(Name = "Required Skills (comma separated)")]
    public string RequiredSkills { get; set; } = string.Empty;
    }

    public class JobDetailsViewModel
    {
        public Job Job { get; set; }
        public IEnumerable<Offer> Proposals { get; set; }
        public bool IsClient { get; set; }
        public bool CanSubmitProposal { get; set; }
        //[Display(Name = "Skills Required (comma separated)")]

    }
