using FreelancePlatform.Domain.Entities;
using FreelancePlatform.Domain.Enums;

namespace FreelancePlatform.Application.Common.Interfaces; 
public interface IJobRepository : IRepository<Job>
{
    Task<IEnumerable<Job>> GetJobsByStatusAsync(JobStatus status);
    Task<IEnumerable<Job>> GetJobsByClientAsync(int clientId);
    Task UpdateJobStatusAsync(int jobId, JobStatus status);
}

//using cloud.Domain.Entities;

//namespace cloud.Application.Common.Interfaces;
//public interface IJobRepository
//{
//    Task<Job> GetByIdAsync(Guid id);
//    Task<IEnumerable<Job>> GetAllAsync();
//    Task<IEnumerable<Job>> GetByClientIdAsync(Guid clientId);
//    Task<IEnumerable<Job>> GetOpenJobsAsync();
//    Task AddAsync(Job job);
//    Task UpdateAsync(Job job);
//}


// using cloud.Domain.Entities;

// namespace Cloud.Application.Common.Interfaces;

// public interface IJobRepository
// {
//     Task<Job?> GetByIdAsync(Guid id);
//     Task<IEnumerable<Job>> GetOpenJobsAsync();
//     Task<IEnumerable<Job>> GetJobsByClientIdAsync(Guid clientId);
//     Task AddAsync(Job job);
//     Task UpdateAsync(Job job);
//     Task DeleteAsync(Job job);
// }
