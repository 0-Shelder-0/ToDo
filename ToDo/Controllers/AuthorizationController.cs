using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaltyHasher;
using ToDo.Entities;
using ToDo.Interfaces;
using ToDo.Models.Account;

namespace ToDo.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuthorizationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Boards", "Board");
            }
            return View();
        }

        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepository.GetUserByEmail(loginModel.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "User with this email does not exist.");
                    return View(loginModel);
                }
                var saltyHash = new SaltyHash(user.Hash, user.Salt);
                if (saltyHash.Validate(loginModel.Password))
                {
                    await Authenticate(loginModel.Email);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Password", "Please enter correct password.");
            }
            return View(loginModel);
        }

        [HttpGet]
        [Route("SignUp")]
        public IActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Boards", "Board");
            }
            return View();
        }

        [HttpPost]
        [Route("SignUp")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                if (_userRepository.GetUserByEmail(registerModel.Email) != null)
                {
                    ModelState.AddModelError("Email", "A user with this email already exists.");
                    return View(registerModel);
                }
                var saltyHash = SaltyHash.Create(registerModel.Password);
                var user = new User {Email = registerModel.Email, Hash = saltyHash.Hash, Salt = saltyHash.Salt};
                _userRepository.InsertEntity(user);
                _userRepository.Save();
                await Authenticate(registerModel.Email);
                return RedirectToAction("Index", "Home");
            }
            return View(registerModel);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("Settings")]
        public IActionResult Settings()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [Route("ChangePassword")]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            if (model.CurrentPassword == null)
            {
                ModelState.AddModelError("CurrentPassword", "Please enter your password.");
            }
            else if (ModelState.IsValid)
            {
                var user = _userRepository.GetUserByEmail(User.Identity.Name);
                var saltyHash = new SaltyHash(user.Hash, user.Salt);
                if (saltyHash.Validate(model.CurrentPassword))
                {
                    var newPassword = SaltyHash.Create(model.NewPassword);
                    user.Hash = newPassword.Hash;
                    user.Salt = newPassword.Salt;
                    _userRepository.UpdateEntity(user);
                    _userRepository.Save();
                    ViewData.Add("Success", "Password change was successful!");
                }
                else
                {
                    ModelState.AddModelError("CurrentPassword", "Please enter correct password.");
                }
            }
            return View("Settings");
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                                        ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}