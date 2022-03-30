using FanFicFabliaux.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FanFicFabliaux.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext dbContext;
        public BookController(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(this.dbContext.Books);
        }
    }
}
