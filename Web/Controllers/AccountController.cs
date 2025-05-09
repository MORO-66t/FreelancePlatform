// FreelancePlatform.Web/Controllers/AccountController.cs
// using FreelancePlatform.Application.DTOs;
using FreelancePlatform.Domain.Entities;
//using FreelancePlatform.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FreelancePlatform.Web.ViewModels; 
//using FreelancePlatform.Infrastructure.Services;
using FreelancePlatform.Infrastructure.Services;

namespace FreelancePlatform.Web.Controllers;

    public class AccountController : Controller
    {
    //private readonly AuthService _authService;
    private readonly IAuthService _authService;

    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    //public AccountController(
    //    AuthService authService,
    //    IUserRepository userRepository,
    //    ICurrentUserService currentUserService)
    //{
    //    _authService = authService;
    //    _userRepository = userRepository;
    //    _currentUserService = currentUserService;
    //}
    public AccountController(
IAuthService authService,
IUserRepository userRepository,
ICurrentUserService currentUserService)
    {
        _authService = authService;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (_currentUserService.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = await _authService.RegisterAsync(
                    model.Email,
                    model.Password,
                    model.Username,
                    model.UserType);

                await SignInUserAsync(user);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            if (_currentUserService.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = await _authService.LoginAsync(model.Email, model.Password);
                await SignInUserAsync(user);

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userRepository.GetByIdAsync(_currentUserService.UserId.Value);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ProfileViewModel
            {
                Username = user.Username,
                Email = user.Email,
                Bio = user.Bio,
                Skills = user.Skills,
                UserType = user.Type
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userRepository.GetByIdAsync(_currentUserService.UserId.Value);
            if (user == null)
            {
                return NotFound();
            }

            user.Bio = model.Bio;
            user.Skills = model.Skills;

            await _userRepository.UpdateAsync(user);

            ViewBag.SuccessMessage = "Profile updated successfully!";
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private async Task SignInUserAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Type.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
