using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FreelancePlatform.Infrastructure.Services;
using FreelancePlatform.Infrastructure.Dtos;
using FreelancePlatform.Web.ViewModels;

using FreelancePlatform.Application.Common.Interfaces;
using FreelancePlatform.Application.Services;
using System.Security.Claims;
using FreelancePlatform.Web.ViewModels;
// FreelancePlatform.Web/Controllers/JobController.cs

using FreelancePlatform.Domain.Enums;

namespace FreelancePlatform.Web.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        private readonly IJobService _jobService;
        private readonly IOfferService _proposalService;
        private readonly ICurrentUserService _currentUserService;

        public JobController(
            IJobService jobService,
            IOfferService proposalService,
            ICurrentUserService currentUserService)
        {
            _jobService = jobService;
            _proposalService = proposalService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new JobViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var jobDto = new JobDto
            {
                Title = model.Title,
                Description = model.Description,
                Budget = model.Budget,
                Deadline = model.Deadline,
                //RequiredSkills = model.RequiredSkills
            };

            var job = await _jobService.CreateJobAsync(jobDto, _currentUserService.UserId.Value);
            return RedirectToAction("Details", new { id = job.Id });
        }

//         public async Task<IActionResult> Browse()
// {
//     var jobs = await _jobService.GetOpenJobsAsync();
//     return View(jobs);
// }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var job = await _jobService.GetJobByIdAsync(id);
            if (job == null)
                return NotFound();

            var proposals = await _proposalService.GetProposalsForJobAsync(id);
            var isClient = _currentUserService.UserId == job.ClientId;

            var viewModel = new JobDetailsViewModel
            {
                Job = job,
                Proposals = proposals,
                IsClient = isClient,
                CanSubmitProposal = !isClient &&
                                 job.Status == JobStatus.Open &&
                                 !proposals.Any(p => p.FreelancerId == _currentUserService.UserId)
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> MyJobs()
        {
            var jobs = await _jobService.GetJobsByClientAsync(_currentUserService.UserId.Value);
            return View(jobs);
        }

        [HttpGet]
        public async Task<IActionResult> Browse()
        {
            var jobs = await _jobService.GetOpenJobsAsync();
            return View(jobs);
        }
    }
}