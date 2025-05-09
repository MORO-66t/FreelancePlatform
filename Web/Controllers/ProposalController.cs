
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FreelancePlatform.Application.Services;
using FreelancePlatform.Application.Dtos;
using FreelancePlatform.Web.ViewModels;
using System.Security.Claims;
using FreelancePlatform.Domain.Enums;
using FreelancePlatform.Domain.Exceptions;
using FreelancePlatform.Infrastructure.Services;
using static FreelancePlatform.Application.Dtos.OfferDto;
using FreelancePlatform.Application.Common.Interfaces;
using FreelancePlatform.Domain.Entities;
namespace FreelancePlatform.Web.Controllers;
using FreelancePlatform.Application.Common.Interfaces;
using FreelancePlatform.Application.Services;
using FreelancePlatform.Domain.Entities;
using FreelancePlatform.Domain.Enums;
using FreelancePlatform.Domain.Exceptions;


[Authorize]
public class ProposalController : Controller
{
    private readonly IOfferService _proposalService;
    private readonly IJobService _jobService;
    private readonly ICurrentUserService _currentUserService;

    public ProposalController(
        IOfferService proposalService,
        IJobService jobService,
        ICurrentUserService currentUserService)
    {
        _proposalService = proposalService;
        _jobService = jobService;
        _currentUserService = currentUserService;
    }

    [HttpGet]
    [Authorize(Roles = "Freelancer")]
    public async Task<IActionResult> Create(int jobId)
    {
        var job = await _jobService.GetJobByIdAsync(jobId);
        if (job == null || job.Status != JobStatus.Open)
        {
            return NotFound();
        }

        var viewModel = new OfferViewModel
        {
            JobId = jobId,
            JobTitle = job.Title
        };

        return View(viewModel);
    }

    [HttpPost]
    [Authorize(Roles = "Freelancer")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OfferViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var proposalDto = new OfferDto
            {
                JobId = model.JobId,
                BidAmount = model.BidAmount,
                CoverLetter = model.CoverLetter
            };

            await _proposalService.SubmitProposalAsync(proposalDto, _currentUserService.UserId.Value);
            return RedirectToAction("Details", "Job", new { id = model.JobId });
        }
        catch (AppException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
    }

    [HttpPost]
    [Authorize(Roles = "Client")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Accept(int proposalId)
    {
        try
        {
            await _proposalService.AcceptProposalAsync(proposalId, _currentUserService.UserId.Value);
            return RedirectToAction("Details", "Job", new { id = (await _proposalService.GetByIdAsync(proposalId)).JobId });
        }
        catch (AppException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Details", "Job", new { id = (await _proposalService.GetByIdAsync(proposalId)).JobId });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Client")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reject(int proposalId)
    {
        try
        {
            await _proposalService.RejectProposalAsync(proposalId, _currentUserService.UserId.Value);
            return RedirectToAction("Details", "Job", new { id = (await _proposalService.GetByIdAsync(proposalId)).JobId });
        }
        catch (AppException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Details", "Job", new { id = (await _proposalService.GetByIdAsync(proposalId)).JobId });
        }
    }

    [HttpGet]
[Authorize(Roles = "Freelancer")]
public async Task<IActionResult> MyProposals()
{
    try
    {
        var userId = _currentUserService.UserId;
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var proposals = await _proposalService.GetProposalsByFreelancerAsync(userId.Value);
        return View(proposals ?? new List<Offer>());
    }
    // catch (Exception ex)
    // {
    //     _logger.LogError(ex, "Error loading proposals");
    //     return View(new List<Offer>());
    // }
    catch (AppException ex)
    {
        TempData["ErrorMessage"] = ex.Message;
        return RedirectToAction("Index", "Home");
    }
}
}
