using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using System.Security.Claims;
using ULMS2.Models;
using ULMS2.ViewModels;

namespace ULMS2.Controllers
{
    public class AccountController : Controller
    {
        public readonly UserManager<Account> userManager;
        public readonly SignInManager<Account> signInManager;
        public readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<Account> _userManager, SignInManager<Account> _signInManager, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerVm)
        {
            // Check validity of model state first
            if (!ModelState.IsValid)
                return View(registerVm);

            // Verify that Librarian role doesn't have a department
            if (registerVm.Role == "Librarian" && !String.IsNullOrEmpty(registerVm.Department))
            {
                ModelState.AddModelError("", "Librarian cannot have a department");
                return View(registerVm);
            }

            Account acct = await userManager.FindByEmailAsync(registerVm.Email);
            if (acct != null)
            {
                ModelState.AddModelError("", "That email is already in use");
                return View(registerVm);
            }

            // Set role
            string role = registerVm.Role;

            // Create account based on chosen role
            Account newAccount;
            if (role == "Student")
            {
                newAccount = new Student()
                {
                    Name = registerVm.Name,
                    Email = registerVm.Email,
                    Department = registerVm.Department
                };
            }
            else if (role == "Faculty")
            {
                newAccount = new Faculty()
                {
                    Name = registerVm.Name,
                    Email = registerVm.Email,
                    Department = registerVm.Department
                };
            }
            else if (role == "Librarian")
            {
                newAccount = new Librarian()
                {
                    Name = registerVm.Name,
                    Email = registerVm.Email
                };
            }
            else
            {
                // Role name did match any valid ones (the aforementioned ones)
                ModelState.AddModelError("Role", "Invalid role");
                return View(registerVm);
            }
            newAccount.UserName = newAccount.Id;
            var result = await userManager.CreateAsync(newAccount, registerVm.Password);

            // Get role or create it if it hasn't been created already
            IdentityRole dbRole = await roleManager.FindByNameAsync(role);
            if (dbRole == null)
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                if (!roleResult.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(registerVm);
                }
                dbRole = await roleManager.FindByNameAsync(role);
            }

            // Redirect to Login page if registration is successful
            if (result.Succeeded)
            {
                // Assign user to role if registration was successful
                Account newUser = await userManager.FindByEmailAsync(registerVm.Email);
                var roleAssignmentResult = await userManager.AddToRoleAsync(newUser, dbRole.Name);
                if (!roleAssignmentResult.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(registerVm);
                }

                // Add claims to user if role assignment was successful
                IEnumerable<Claim> claims = new List<Claim> { new Claim (ClaimTypes.GivenName, registerVm.Name) };
                await userManager.AddClaimsAsync(newUser, claims);

                TempData["Registered"] = "true";
                return RedirectToAction(nameof(Login));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVm);
            }

        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVm)
        {
            if (!ModelState.IsValid)
                return View(loginVm);

            Account acct = await userManager.FindByEmailAsync(loginVm.Email);
            if (acct == null)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(loginVm);
            }

            var result = await signInManager.PasswordSignInAsync(acct, loginVm.Password, isPersistent: loginVm.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View(loginVm);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            TempData["SignedOut"] = "true";
            return RedirectToAction(nameof(Login));
        }
    }
}
