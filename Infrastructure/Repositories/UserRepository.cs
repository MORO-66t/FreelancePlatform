// FreelancePlatform.Infrastructure/Repositories/UserRepository.cs
using FreelancePlatform.Domain.Entities;
using FreelancePlatform.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using FreelancePlatform.Infrastructure.Data;
using System.Collections.Generic;
using FreelancePlatform.Domain.Enums;

namespace FreelancePlatform.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email);
        }

        public async Task<bool> UsernameExistsAsync(string username)
{
    return await _context.Users
        .AnyAsync(u => u.Username == username);
}

        public async Task<IEnumerable<User>> GetFreelancersBySkillAsync(string skill)
        {
            return await _context.Users
                .Where(u => u.Type == UserType.Freelancer && 
                           (u.Skills != null && u.Skills.Contains(skill)))
                .ToListAsync();
        }
    }
}

// Similar implementations for JobRepository and ProposalRepository

//using cloud.Application.Common.Interfaces;
//using cloud.Domain.Common;
//using Cloud.Application.Common.Interfaces;
//using Cloud.Domain.Entities;
//using Cloud.Infrastructure.Data;
//using Microsoft.EntityFrameworkCore;

//namespace Cloud.Infrastructure.Repositories;

//public class UserRepository : IUserRepository
//{
//    private readonly ApplicationDbContext _context;

//    public UserRepository(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<User?> GetByIdAsync(Guid id)
//        => await _context.Users.FindAsync(id);

//    public async Task<User?> GetByEmailAsync(string email)
//        => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

//    public async Task<bool> EmailExistsAsync(string email)
//        => await _context.Users.AnyAsync(u => u.Email == email);

//    public async Task AddAsync(User user)
//    {
//        await _context.Users.AddAsync(user);
//        await _context.SaveChangesAsync();
//    }

//    public async Task UpdateAsync(User user)
//    {
//        _context.Users.Update(user);
//        await _context.SaveChangesAsync();
//    }
//}


// using cloud.Application.Common.Interfaces;
// using cloud.Domain.Common;
// using Cloud.Application.Common.Interfaces;
// using Cloud.Infrastructure.Identity;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;

// namespace Cloud.Infrastructure.Repositories;

// public class UserRepository : IUserRepository
// {
//     private readonly UserManager<ApplicationUser> _userManager;

//     public UserRepository(UserManager<ApplicationUser> userManager)
//     {
//         _userManager = userManager;
//     }

//     public async Task<User> GetByIdAsync(Guid id)
//     {
//         var user = await _userManager.FindByIdAsync(id.ToString());
//         return user?.ToDomainUser() ?? throw new Exception("User not found");
//     }

//     public async Task<User> GetByEmailAsync(string email)
//     {
//         var user = await _userManager.FindByEmailAsync(email);
//         return user?.ToDomainUser() ?? throw new Exception("User not found");
//     }

//     public async Task<bool> EmailExistsAsync(string email)
//     {
//         return await _userManager.Users.AnyAsync(u => u.Email == email);
//     }

//     public async Task AddAsync(User user)
//     {
//         var result = await _userManager.CreateAsync(ApplicationUserExtensions.FromDomainUser(user));
//         if (!result.Succeeded)
//             throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
//     }

//     public async Task UpdateAsync(User user)
//     {
//         var result = await _userManager.UpdateAsync(ApplicationUserExtensions.FromDomainUser(user));
//         if (!result.Succeeded)
//             throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
//     }
// }
