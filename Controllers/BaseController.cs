using FanFicFabliaux.Data;
using FanFicFabliaux.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FanFicFabliaux.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext dbContext;
        protected UserManager<User> userManager;

        public string UserId { get => this.userManager.GetUserId(base.User); }

        public BaseController(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
    }
}
