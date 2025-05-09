using FreelancePlatform.Domain.Entities;
namespace FreelancePlatform.Application.Common.Interfaces;
public interface IOfferRepository : IRepository<Offer>
{
    Task<IEnumerable<Offer>> GetProposalsByJobAsync(int jobId);
    Task<IEnumerable<Offer>> GetProposalsByFreelancerAsync(int freelancerId);
    Task AcceptProposalAsync(int proposalId);
    Task RejectProposalAsync(int proposalId);
    Task<Offer> GetFreelancerProposalForJob(int freelancerId, int id);
}

//using cloud.Domain.Entities;
//using cloud.Domain.Enums;

//namespace cloud.Application.Common.Interfaces;
//public interface IOfferRepository
//{
//    Task<Offer> GetByIdAsync(Guid id);
//    Task<IEnumerable<Offer>> GetByJobIdAsync(Guid jobId);
//    Task<IEnumerable<Offer>> GetByFreelancerIdAsync(Guid freelancerId);
//    Task AddAsync(Offer offer);
//    Task UpdateAsync(Offer offer);
//}




// using cloud.Domain.Entities;
// using cloud.Domain.Enums;
// namespace Cloud.Application.Common.Interfaces;

// public interface IOfferRepository
// {
//     Task<Offer?> GetByIdAsync(Guid id);
//     Task<IEnumerable<Offer>> GetOffersByJobIdAsync(Guid jobId);
//     Task<IEnumerable<Offer>> GetOffersByFreelancerIdAsync(Guid freelancerId);
//     Task AddAsync(Offer offer);
//     Task UpdateAsync(Offer offer);
//     Task UpdateStatusAsync(Guid offerId, OfferStatus status);
// }
