using FanFicFabliaux.Data;
using FanFicFabliaux.Models.ViewModels;
using FanFicFabliaux.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using static FanFicFabliaux.Models.ViewModels.ChooseBookModel;

namespace FanFicFabliaux.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly WriteBookService _writeBookService;
        private readonly CategoryService _categoryService;
        private readonly ReadBookService _readBookService;
        private readonly BookDataService _bookDataService;
        private readonly SubscriptionService _subscriptionService;

        public BookController(
            ApplicationDbContext dbContext,
            WriteBookService writeBookService,
            CategoryService categoryService,
            ReadBookService readBookService,
            BookDataService bookDataService,
            SubscriptionService subscriptionService)
        {
            this.dbContext = dbContext;
            _writeBookService = writeBookService;
            _categoryService = categoryService;
            _readBookService = readBookService;
            _bookDataService = bookDataService;
            _subscriptionService = subscriptionService;
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
            if (Input.Tekst != null)
            {
                ModelState.ClearValidationState("Input.Datoteka");
                ModelState.MarkFieldValid("Input.Datoteka");
            }
            else if (Input.Datoteka != null)
            {
                ModelState.ClearValidationState("Input.Tekst");
                ModelState.MarkFieldValid("Input.Tekst");
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _writeBookService.WriteBook(userId, Input);
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
        public IActionResult ChooseBook(BookFilter filter)
        {
            ChooseBookModel model = new ChooseBookModel
            {
                Books = _readBookService.GetBooksByFilter(filter),
                Filter = filter ?? new BookFilter(),
                Options = _categoryService.GetCategoryOptions()
            };

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult BookData(int bookId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            BookDataModel model = _bookDataService.GetModel(bookId, userId);

            if (model == null)
            {
                //return not found
                return View("BookNotFound");
            }

            return View(model);
        }

        [Authorize]
        public bool Subscribe(string AuthorId, bool IsSubscribed)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _subscriptionService.Subscribe(AuthorId, userId, IsSubscribed);
        }

        [AllowAnonymous]
        public IActionResult Read(int bookId)
        {
            var file = _readBookService.GetPdfById(bookId);

            if (file == null)
            {
                return View("BookNotFound");
            }

            return File(file, "application/pdf");
        }

        [AllowAnonymous]
        public IActionResult Download(int bookId)
        {
            var file = _readBookService.GetPdfById(bookId);

            if (file == null)
            {
                return View("BookNotFound");
            }

            string fileName = _readBookService.GetBookTitle(bookId);
            return File(file, "application/pdf", fileName);
        }

        [Authorize]
        public void Comment(int bookId, string CommentInput)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _bookDataService.SaveComment(bookId, userId, CommentInput);
        }
    }
}