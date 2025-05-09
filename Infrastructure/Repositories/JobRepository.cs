// FreelancePlatform.Infrastructure/Repositories/JobRepository.cs
using FreelancePlatform.Domain.Entities;
using FreelancePlatform.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using FreelancePlatform.Domain.Enums;
using FreelancePlatform.Infrastructure.Data;
using System.Collections.Generic;
// using FreelancePlatform.Application.Common.Interfaces;

namespace FreelancePlatform.Infrastructure.Repositories
{

    public class JobRepository : IJobRepository
    {
        private readonly AppDbContext _context;

        public JobRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Job> GetByIdAsync(int id)
        {
            return await _context.Jobs
                .Include(j => j.Client)
                .Include(j => j.Proposals)
                .ThenInclude(p => p.Freelancer)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            return await _context.Jobs
                .Include(j => j.Client)
                .ToListAsync();
        }

        public async Task AddAsync(Job entity)
        {
            await _context.Jobs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Job entity)
        {
            _context.Jobs.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Job entity)
        {
            _context.Jobs.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByStatusAsync(JobStatus status)
        {
            return await _context.Jobs
                .Where(j => j.Status == status)
                .Include(j => j.Client)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByClientAsync(int clientId)
        {
            return await _context.Jobs
                .Where(j => j.ClientId == clientId)
                .Include(j => j.Proposals)
                .ThenInclude(p => p.Freelancer)
                .ToListAsync();
        }

        public async Task UpdateJobStatusAsync(int jobId, JobStatus status)
        {
            var job = await _context.Jobs.FindAsync(jobId);
            if (job != null)
            {
                job.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Job>> SearchJobsAsync(string searchTerm)
        {
            return await _context.Jobs
                .Where(j => j.Status == JobStatus.Open && 
                          (j.Title.Contains(searchTerm) || j.Description.Contains(searchTerm)))
                .Include(j => j.Client)
                .ToListAsync();
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

// public class JobRepository : IJobRepository
// {
//     private readonly ApplicationDbContext _context;

//     public JobRepository(ApplicationDbContext context)
//     {
//         _context = context;
//     }

//     public async Task<Job?> GetByIdAsync(Guid id)
//     {
//         return await _context.Jobs
//             .Include(j => j.Client)
//             .Include(j => j.Offers)
//             .FirstOrDefaultAsync(j => j.Id == id);
//     }

//     public async Task<IEnumerable<Job>> GetOpenJobsAsync()
//     {
//         return await _context.Jobs
//             .Where(j => j.Status == JobStatus.Open)
//             .Include(j => j.Client)
//             .OrderByDescending(j => j.CreatedDate)
//             .ToListAsync();
//     }

//     public async Task<IEnumerable<Job>> GetJobsByClientIdAsync(Guid clientId)
//     {
//         return await _context.Jobs
//             .Where(j => j.ClientId == clientId)
//             .Include(j => j.Offers)
//             .OrderByDescending(j => j.CreatedDate)
//             .ToListAsync();
//     }

//     public async Task AddAsync(Job job)
//     {
//         await _context.Jobs.AddAsync(job);
//         await _context.SaveChangesAsync();
//     }

//     public async Task UpdateAsync(Job job)
//     {
//         _context.Jobs.Update(job);
//         await _context.SaveChangesAsync();
//     }

//     public async Task DeleteAsync(Job job)
//     {
//         _context.Jobs.Remove(job);
//         await _context.SaveChangesAsync();
//     }
// }
