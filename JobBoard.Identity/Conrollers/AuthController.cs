using JobBoard.Identity.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Identity.Conrollers
{
    [Route("[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AuthController(SignInManager<AppUser> signInManager,
                             UserManager<AppUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                             IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _interactionService = interactionService;
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
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            //var user = await _userManager.FindByNameAsync(vm.Username);
            var user = await _userManager.FindByEmailAsync(vm.Email);
            if(user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);
            //var result = await _signInManager.PasswordSignInAsync(vm.Username,
            //                                                      vm.Password,
            //                                                      false,
            //                                                      false);

            if (result.Succeeded)
            {
                return Redirect(vm.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Login error");
            return View(vm);
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
            if (!ModelState.IsValid)
                return View(viewModel);

            var user = new AppUser { Email = viewModel.Email, EmailConfirmed = true };

            var result = await _userManager.CreateAsync(user, viewModel.Password);
            var role = await _roleManager.FindByNameAsync(viewModel.Role);

            if (role == null)
                throw new Exception("Role was not found");

            result = await _userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                //await _signInManager.SignInAsync(user, false);
                await new HttpClient().PostAsJsonAsync<string>("https://api.com/Administrator/Create", user.Id);
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
