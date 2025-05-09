// FreelancePlatform.Application/Services/ProposalService.cs
using FreelancePlatform.Domain.Entities;
using FreelancePlatform.Domain.Exceptions;
using FreelancePlatform.Domain.Common;
using FreelancePlatform.Application.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelancePlatform.Domain.Enums;
//using FreelancePlatform.Application.Interfaces;
using FreelancePlatform.Application.Common.Interfaces;
//using FreelancePlatform.Application.Common.Interfaces;
//using FreelancePlatform.Domain.Exceptions;

namespace FreelancePlatform.Application.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _proposalRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IUserRepository _userRepository;

        public OfferService(
            IOfferRepository proposalRepository,
            IJobRepository jobRepository,
            IUserRepository userRepository)
        {
            _proposalRepository = proposalRepository;
            _jobRepository = jobRepository;
            _userRepository = userRepository;
        }

        public async Task<Offer> SubmitProposalAsync(OfferDto proposalDto, int freelancerId)
        {
            var freelancer = await _userRepository.GetByIdAsync(freelancerId);
            if (freelancer == null || freelancer.Type != UserType.Freelancer)
                throw new AppException("Only freelancers can submit proposals");

            var job = await _jobRepository.GetByIdAsync(proposalDto.JobId);
            if (job == null)
                throw new AppException("Job not found");

            if (job.Status != JobStatus.Open)
                throw new AppException("This job is no longer accepting proposals");

            if (job.ClientId == freelancerId)
                throw new AppException("You cannot submit a proposal to your own job");

            var existingProposal = await _proposalRepository.GetFreelancerProposalForJob(freelancerId, job.Id);
            if (existingProposal != null)
                throw new AppException("You have already submitted a proposal for this job");

            var proposal = new Offer
            {
                FreelancerId = freelancerId,
                JobId = job.Id,
                BidAmount = proposalDto.BidAmount,
                CoverLetter = proposalDto.CoverLetter,
                Status = OfferStatus.Pending,
                SubmittedAt = DateTime.UtcNow
            };

            await _proposalRepository.AddAsync(proposal);
            return proposal;
        }

        public async Task AcceptProposalAsync(int proposalId, int clientId)
        {
            var proposal = await _proposalRepository.GetByIdAsync(proposalId);
            if (proposal == null)
                throw new AppException("Proposal not found");

            var job = await _jobRepository.GetByIdAsync(proposal.JobId);
            if (job == null || job.ClientId != clientId)
                throw new AppException("You are not authorized to accept this proposal");

            if (proposal.Status != OfferStatus.Pending)
                throw new AppException("This proposal has already been processed");

            await _proposalRepository.AcceptProposalAsync(proposalId);
            await _jobRepository.UpdateJobStatusAsync(job.Id, JobStatus.InProgress);
        }

        public async Task RejectProposalAsync(int proposalId, int clientId)
        {
            var proposal = await _proposalRepository.GetByIdAsync(proposalId);
            if (proposal == null)
                throw new AppException("Proposal not found");

            var job = await _jobRepository.GetByIdAsync(proposal.JobId);
            if (job == null || job.ClientId != clientId)
                throw new AppException("You are not authorized to reject this proposal");

            if (proposal.Status != OfferStatus.Pending)
                throw new AppException("This proposal has already been processed");

            await _proposalRepository.RejectProposalAsync(proposalId);
        }

        public async Task<IEnumerable<Offer>> GetProposalsForJobAsync(int jobId)
        {
            return await _proposalRepository.GetProposalsByJobAsync(jobId);
        }

        public async Task<IEnumerable<Offer>> GetProposalsByFreelancerAsync(int freelancerId)
        {
 try
    {
        var proposals = await _proposalRepository.GetProposalsByFreelancerAsync(freelancerId);
        return proposals?.Where(p => p != null) ?? Enumerable.Empty<Offer>();
    }
    catch (Exception ex)
    {
        // Log the exception (if you have a logging mechanism)
        return Enumerable.Empty<Offer>();
    }
        }


        public async Task<Offer> GetByIdAsync(int id)
        {
            return await _proposalRepository.GetByIdAsync(id);
        }
    }
   
}