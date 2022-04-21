using FanFicFabliaux.Data;
using FanFicFabliaux.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FanFicFabliaux.Controllers
{
    /// <summary>
    /// Used for loggin menagment.
    /// </summary>
    public class BaseController : Controller
    {
        protected ApplicationDbContext dbContext;
        protected UserManager<User> userManager;
        /// <summary>
        /// User id.
        /// </summary>
        public string UserId { get => this.userManager.GetUserId(base.User); }
        /// <summary>
        /// Initializes BaseController
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="userManager"></param>
        public BaseController(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
    }
}
