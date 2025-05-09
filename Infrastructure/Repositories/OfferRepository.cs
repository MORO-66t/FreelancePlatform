// FreelancePlatform.Infrastructure/Repositories/ProposalRepository.cs
using FreelancePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FreelancePlatform.Domain.Enums;
using FreelancePlatform.Infrastructure.Data;
using System.Collections.Generic;
using FreelancePlatform.Application.Common.Interfaces;
//using FreelancePlatform.Application.Common.Interfaces;
using System.Linq;
using System.Threading.Tasks;


namespace FreelancePlatform.Infrastructure.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly AppDbContext _context;

        public OfferRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Offer> GetByIdAsync(int id)
        {
            return await _context.Proposals
                .Include(p => p.Freelancer)
                .Include(p => p.Job)
                .ThenInclude(j => j.Client)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Offer>> GetAllAsync()
        {
            return await _context.Proposals
                .Include(p => p.Freelancer)
                .Include(p => p.Job)
                .ToListAsync();
        }

        public async Task AddAsync(Offer entity)
        {
            await _context.Proposals.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Offer entity)
        {
            _context.Proposals.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Offer entity)
        {
            _context.Proposals.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Offer>> GetProposalsByJobAsync(int jobId)
        {
            return await _context.Proposals
                .Where(p => p.JobId == jobId)
                .Include(p => p.Freelancer)
                .ToListAsync();
        }

        public async Task<IEnumerable<Offer>> GetProposalsByFreelancerAsync(int freelancerId)
        {
            return await _context.Proposals
                .Where(p => p.FreelancerId == freelancerId)
                .Include(p => p.Job)
                .ThenInclude(j => j.Client)
                .ToListAsync();
        }

        //public async Task AcceptProposalAsync(int proposalId)
        //{
        //    var proposal = await _context.Proposals.FindAsync(proposalId);
        //    if (proposal != null)
        //    {
        //        proposal.Status = OfferStatus.Accepted;
                
        //        // Reject all other proposals for this job
        //        var otherProposals = await _context.Proposals
        //            .Where(p => p.JobId == proposal.JobId && p.Id != proposalId)
        //            .ToListAsync();

        //        foreach (var otherProposal in otherProposals)
        //        {
        //            otherProposal.Status = OfferStatus.Rejected;
        //        }

        //        await _context.SaveChangesAsync();
        //    }
        //}

        //public async Task RejectProposalAsync(int proposalId)
        //{
        //    var proposal = await _context.Proposals.FindAsync(proposalId);
        //    if (proposal != null)
        //    {
        //        proposal.Status = OfferStatus.Rejected;
        //        await _context.SaveChangesAsync();
        //    }
        //}

        public async Task<IEnumerable<Offer>> GetAcceptedProposalsByFreelancerAsync(int freelancerId)
        {
            return await _context.Proposals
                .Where(p => p.FreelancerId == freelancerId && p.Status == OfferStatus.Accepted)
                .Include(p => p.Job)
                .ThenInclude(j => j.Client)
                .ToListAsync();
        }
        public async Task<Offer> GetFreelancerProposalForJob(int freelancerId, int jobId)
        {
            return await _context.Proposals
                .FirstOrDefaultAsync(p => p.FreelancerId == freelancerId && p.JobId == jobId);
        }
        public async Task AcceptProposalAsync(int proposalId)
        {
            var proposal = await _context.Proposals.FindAsync(proposalId);
            if (proposal != null)
            {
                proposal.Status = OfferStatus.Accepted;

                // Reject all other proposals for this job
                var otherProposals = await _context.Proposals
                    .Where(p => p.JobId == proposal.JobId && p.Id != proposalId)
                    .ToListAsync();

                foreach (var otherProposal in otherProposals)
                {
                    otherProposal.Status = OfferStatus.Rejected;
                }

                await _context.SaveChangesAsync();
            }
        }
        public async Task RejectProposalAsync(int proposalId)
        {
            var proposal = await _context.Proposals.FindAsync(proposalId);
            if (proposal != null)
            {
                proposal.Status = OfferStatus.Rejected;
                await _context.SaveChangesAsync();
            }
        }
    }
}
// using cloud.Application.Common.Interfaces;
// using cloud.Domain.Entities;
// using cloud.Domain.Enums;
// using Cloud.Application.Common.Interfaces;
// using Cloud.Infrastructure.Data;
// using Microsoft.EntityFrameworkCore;

// namespace Cloud.Infrastructure.Repositories;

// public class OfferRepository : IOfferRepository
// {
//     private readonly ApplicationDbContext _context;

//     public OfferRepository(ApplicationDbContext context)
//     {
//         _context = context;
//     }

//     public async Task<Offer?> GetByIdAsync(Guid id)
//     {
//         return await _context.Offers
//             .Include(o => o.Freelancer)
//             .Include(o => o.Job)
//             .FirstOrDefaultAsync(o => o.Id == id);
//     }

//     public async Task<IEnumerable<Offer>> GetOffersByJobIdAsync(Guid jobId)
//     {
//         return await _context.Offers
//             .Where(o => o.JobId == jobId)
//             .Include(o => o.Freelancer)
//             .OrderByDescending(o => o.CreatedDate)
//             .ToListAsync();
//     }

//     public async Task<IEnumerable<Offer>> GetOffersByFreelancerIdAsync(Guid freelancerId)
//     {
//         return await _context.Offers
//             .Where(o => o.FreelancerId == freelancerId)
//             .Include(o => o.Job)
//             .OrderByDescending(o => o.CreatedDate)
//             .ToListAsync();
//     }

//     public async Task AddAsync(Offer offer)
//     {
//         await _context.Offers.AddAsync(offer);
//         await _context.SaveChangesAsync();
//     }

//     public async Task UpdateAsync(Offer offer)
//     {
//         _context.Offers.Update(offer);
//         await _context.SaveChangesAsync();
//     }

//     public async Task UpdateStatusAsync(Guid offerId, OfferStatus status)
//     {
//         var offer = await _context.Offers.FindAsync(offerId);
//         if (offer != null)
//         {
//             offer.Status = status;
//             await _context.SaveChangesAsync();
//         }
//     }
// }
