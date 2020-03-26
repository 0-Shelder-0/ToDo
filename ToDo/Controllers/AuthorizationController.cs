using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDo.Models.Account;

namespace ToDo.Controllers
{
    public class AuthorizationController : Controller
    {
        private ApplicationDbContext dbContext;

        public AuthorizationController(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel loginModel)
        {
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel registerModel)
        {
            return Redirect("/");
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
