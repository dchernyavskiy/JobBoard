using IdentityServer4.Services;
using JobBoard.Application.Interfaces;
using JobBoard.Identity.Interfaces;
using JobBoard.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Identity.Conrollers
{
    [Route("[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IJobDbContext _context;

        public AuthController(SignInManager<AppUser> signInManager,
                             UserManager<AppUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                             IIdentityServerInteractionService interactionService,
                             IJobDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _interactionService = interactionService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var vm = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);


            var user = await _userManager.FindByEmailAsync(vm.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);

            if (result.Succeeded)
            {
                return Redirect(vm.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Login error");
            return View(vm);
        }

        [HttpGet]
        public IActionResult RegisterEmployee(string returnUrl = "default")
        {
            var viewModel = new RegisterEmployeeViewModel { ReturnUrl = returnUrl };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEmployee(RegisterEmployeeViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = $"{viewModel.FirstName.ToLower()}_{viewModel.LastName.ToLower()}",
                Email = viewModel.Email,
                EmailConfirmed = true,
            };
            _ = await _userManager.CreateAsync(user, viewModel.Password);

            var role = await _roleManager.FindByNameAsync("Employee");
            if (role == null) throw new Exception("Role was not found");
            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                var id = Guid.Parse(user.Id);

                await _context.Employees.AddAsync(new Domain.Employee 
                {
                    Id = id,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Email = viewModel.Email,
                    Phone = viewModel.Phone,
                    CVLink = viewModel.CVLink
                });

                await _context.SaveChangesAsync(new CancellationToken());
                return Redirect(viewModel.ReturnUrl);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult RegisterEmployer(string returnUrl = "default")
        {
            var viewModel = new RegisterEmployerViewModel { ReturnUrl = returnUrl };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEmployer(RegisterEmployerViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = viewModel.Name,
                Email = viewModel.Email,
                EmailConfirmed = true,
            };
            _ = await _userManager.CreateAsync(user, viewModel.Password);

            var role = await _roleManager.FindByNameAsync("Employer");
            if (role == null) throw new Exception("Role was not found");
            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                var id = Guid.Parse(user.Id);
                await _context.Employers.AddAsync(new Domain.Employer
                {
                    Id = id,
                    Name = viewModel.Name,
                    AboutUs = viewModel.AboutUs,
                    TeamSize = viewModel.TeamSize,
                    Location = viewModel.Location,
                    PhotoLink = viewModel.PhotoLink
                });
                await _context.SaveChangesAsync(new CancellationToken());
                return Redirect(viewModel.ReturnUrl);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var viewModel = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var user = new AppUser
            {
                Email = viewModel.Email,
                EmailConfirmed = true,
                UserName = string.Join("", viewModel.Email.TakeWhile(c => c != '@'))
            };

            _ = await _userManager.CreateAsync(user, viewModel.Password);

            var role = await _roleManager.FindByNameAsync(viewModel.Role);

            if (role == null) throw new Exception("Role was not found");

            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                var id = Guid.Parse(user.Id);
                if (role.Name == "Employee")
                    await _context.Employees.AddAsync(new Domain.Employee { Id = id });
                else if (role.Name == "Employer")
                    await _context.Employers.AddAsync(new Domain.Employer { Id = id });
                await _context.SaveChangesAsync(new CancellationToken());
                return Redirect(viewModel.ReturnUrl);
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
