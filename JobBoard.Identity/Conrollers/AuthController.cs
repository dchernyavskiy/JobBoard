using IdentityServer4.Services;
using JobBoard.Identity.Interfaces;
using JobBoard.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        private string _secureKey = "a very very very important secure key";

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

        public string GenerateEmployeeToken(string name)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, "Employee")
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secureKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Today.AddDays(1),
                signingCredentials: credentials
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public string GenerateEmployerToken(string name)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, "Employer")
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secureKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Today.AddDays(1),
                signingCredentials: credentials
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public JwtSecurityToken Verify(string jwt)// this method verifies distinct client by means of extracting a token
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secureKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
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

        [HttpPost]
        public async Task<IActionResult> ULogin(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("User not found");
            return Ok(user.Id);
        }

        [HttpPost]
        public async Task<IActionResult> URegisterEmployee(RegisterEmployeeViewModel viewModel)
        {
            if (!ModelState.IsValid) throw new Exception("Something was wrong");
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
                return Ok(id);
            }

            throw new Exception("Something was wrong");
        }

        [HttpPost]
        public async Task<IActionResult> URegisterEmployer(RegisterEmployerViewModel viewModel)
        {
            if (!ModelState.IsValid) throw new Exception("Something was wrong");
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
                return Ok(id);
            }

            throw new Exception("Something was wrong");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) throw new Exception("User not found");
            await _userManager.DeleteAsync(user);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> UDelete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) throw new Exception("User not found");
            await _userManager.DeleteAsync(user);
            return Ok();
        }
    }
}