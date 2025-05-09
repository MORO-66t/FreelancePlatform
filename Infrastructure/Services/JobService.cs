using FreelancePlatform.Application.Common.Interfaces; 
//using FreelancePlatform.Application.Dtos;
using FreelancePlatform.Domain.Entities;
using FreelancePlatform.Domain.Enums;
// using FreelancePlatform.Infrastructure.Dtos;

using FreelancePlatform.Application.Dtos;
// using FreelancePlatform.Application.Common.Interfaces;

// FreelancePlatform.Application/Services/JobService.cs


namespace FreelancePlatform.Application.Services;


public interface IJobService
{
    Task<Job> CreateJobAsync(JobDto jobDto, int clientId);
    Task<IEnumerable<Job>> GetOpenJobsAsync();
    Task<Job> GetJobByIdAsync(int id);
    Task<IEnumerable<Job>> GetJobsByClientAsync(int clientId);
    //Task CreateJobAsync(JobDto jobDto, int value);
}



public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IUserRepository _userRepository;

        public JobService(IJobRepository jobRepository, IUserRepository userRepository)
        {
            _jobRepository = jobRepository;
            _userRepository = userRepository;
        }

        // In JobService.cs
public async Task<Job> CreateJobAsync(JobDto jobDto, int clientId)
{
    var client = await _userRepository.GetByIdAsync(clientId);
    
    if (client == null)
    {
        throw new ArgumentException("User not found", nameof(clientId));
    }
    
    if (client.Type != UserType.Client)
    {
        throw new UnauthorizedAccessException("Only clients can post jobs");
    }

    var job = new Job
    {
        Title = jobDto.Title,
        Description = jobDto.Description,
        Budget = jobDto.Budget,
        Deadline = jobDto.Deadline,
        ClientId = clientId,
        Status = JobStatus.Open,
        RequiredSkills = jobDto.RequiredSkills ?? "General",
        CreatedAt = DateTime.UtcNow
    };

    await _jobRepository.AddAsync(job);
    return job;
}

        public async Task<IEnumerable<Job>> GetOpenJobsAsync()
        {
            return await _jobRepository.GetJobsByStatusAsync(JobStatus.Open);
        }

       public async Task<Job> GetJobByIdAsync(int id)
{
    return await _jobRepository.GetByIdAsync(id) 
           ?? throw new KeyNotFoundException($"Job with ID {id} not found");
}

        public async Task<IEnumerable<Job>> GetJobsByClientAsync(int clientId)
        {
            return await _jobRepository.GetJobsByClientAsync(clientId);
        }

 
    }
