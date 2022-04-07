using FanFicFabliaux.Data;
using FanFicFabliaux.Models.ViewModels;
using FanFicFabliaux.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FanFicFabliaux.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly WriteBookService _writeBookService;
        private readonly CategoryService _categoryService;
        public BookController(ApplicationDbContext dbContext, WriteBookService writeBookService, CategoryService categoryService)
        {
            this.dbContext = dbContext;
            _writeBookService = writeBookService;
            _categoryService = categoryService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(this.dbContext.Books);
        }

        [HttpGet]
        [Authorize]
        public IActionResult WriteBook()
        {
            WriteBookModel model = new WriteBookModel
            {
                Input = new WriteBookModel.InputModel(),
                Options = _categoryService.GetCategoryOptions()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SaveBook(WriteBookModel.InputModel Input)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _writeBookService.WriteBook(userId, Input.Naslov, Input.Oznake, Input.Zanr.Value, Input.Tekst);
                return LocalRedirect("~/");
            }

            WriteBookModel model = new WriteBookModel
            {
                Input = Input,
                Options = _categoryService.GetCategoryOptions()
            };
            return View("WriteBook", model);
        }

        [AllowAnonymous]
        public IActionResult ChooseBook()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult BookData()
        {
            return View();
        }
    }
}
