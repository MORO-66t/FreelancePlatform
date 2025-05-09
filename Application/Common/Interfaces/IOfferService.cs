// FreelancePlatform.Application/Interfaces/IProposalService.cs
using FreelancePlatform.Domain.Entities;
using FreelancePlatform.Application.Dtos;

using System.Collections.Generic;
using System.Threading.Tasks;

//namespace FreelancePlatform.Application.Common.Interfaces;
// FreelancePlatform.Application/Interfaces/IProposalService.cs

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreelancePlatform.Application.Services
{
    public interface IOfferService
    {
        Task<Offer> SubmitProposalAsync(OfferDto proposalDto, int freelancerId);
        Task AcceptProposalAsync(int proposalId, int clientId);
        Task RejectProposalAsync(int proposalId, int clientId);
        Task<IEnumerable<Offer>> GetProposalsForJobAsync(int jobId);
        Task<IEnumerable<Offer>> GetProposalsByFreelancerAsync(int freelancerId);
        Task<Offer> GetByIdAsync(int id);
    }
}