using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaltyHasher;
using ToDo.Data;
using ToDo.Entities;
using ToDo.Models.Account;

namespace ToDo.Controllers
{
    public class AuthorizationController : Controller
    {
        private DbSet<User> _users;

        public AuthorizationController(ApplicationDbContext applicationDbContext)
        {
            _users = applicationDbContext.Users;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerModel);
            }
            var saltyHash = SaltyHash.Create(registerModel.Password);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Logout()
        {
            return Redirect("/");
        }

        [Authorize]
        public IActionResult Account()
        {
            return View();
        }
    }
}
