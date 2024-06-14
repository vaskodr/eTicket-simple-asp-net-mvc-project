using finalTicket.Models;
using finalTicket.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace finalTicket.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        // GET: Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Username };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    if (!await _roleManager.RoleExistsAsync(model.Role))
                    {
                        _logger.LogInformation($"Creating role: {model.Role}");
                        await _roleManager.CreateAsync(new IdentityRole(model.Role));
                    }

                    await _userManager.AddToRoleAsync(user, model.Role);

                    _logger.LogInformation($"Assigned role '{model.Role}' to user '{model.Username}'.");

                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    _logger.LogError($"Registration error: {error.Description}");
                }
            }
            else
            {
                _logger.LogWarning("Model state is not valid.");
            }
            return View(model);
        }

    // GET: Account/Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: Account/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe,
                lockoutOnFailure: false);
            if (result.Succeeded)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("AdminDashboard", "Dashboard");
                }
                else if (User.IsInRole("User"))
                {
                    return RedirectToAction("UserDashboard", "Dashboard");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(model);
    }

    // POST: Account/Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}