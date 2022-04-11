using FanFicFabliaux.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FanFicFabliaux.Controllers
{
    public class WishlistController : Controller
    {
        private ApplicationDbContext dbContext;
        public WishlistController(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        [AllowAnonymous]
        public IActionResult Wishlist()
        {
            var wishlist = this.dbContext.Books;
            return View(wishlist);
        }
  
    }
}
