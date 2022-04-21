using FanFicFabliaux.Data;
using FanFicFabliaux.Models.ViewModels;
using FanFicFabliaux.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using static FanFicFabliaux.Models.ViewModels.ChooseBookModel;

namespace FanFicFabliaux.Controllers
{
    /// <summary>
    /// Used for book navigation.
    /// </summary>
    public class BookController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly WriteBookService _writeBookService;
        private readonly CategoryService _categoryService;
        private readonly ReadBookService _readBookService;
        private readonly BookDataService _bookDataService;
        private readonly SubscriptionService _subscriptionService;
        private readonly WishlistService _wishlistService;
        /// <summary>
        /// Initializes BookController.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="writeBookService"></param>
        /// <param name="categoryService"></param>
        /// <param name="readBookService"></param>
        /// <param name="bookDataService"></param>
        /// <param name="subscriptionService"></param>
        /// <param name="wishlistService"></param>
        public BookController(
            ApplicationDbContext dbContext,
            WriteBookService writeBookService,
            CategoryService categoryService,
            ReadBookService readBookService,
            BookDataService bookDataService,
            SubscriptionService subscriptionService,
            WishlistService wishlistService)
        {
            this.dbContext = dbContext;
            _writeBookService = writeBookService;
            _categoryService = categoryService;
            _readBookService = readBookService;
            _bookDataService = bookDataService;
            _subscriptionService = subscriptionService;
            _wishlistService = wishlistService;
        }

        /// <summary>
        /// Navigates to landing page.
        /// </summary>
        /// <returns>Landing page.</returns>
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(this.dbContext.Books);
        }

        /// <summary>
        /// Navigates to writeing page
        /// </summary>
        /// <returns>Writeing page</returns>
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
        /// <summary>
        /// Saves book if model is valid.
        /// </summary>
        /// <param name="Input">Form for writing book.</param>
        /// <returns>Returns landing page or error.</returns>
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
                var author = User.FindFirstValue(ClaimTypes.Name);
                await _writeBookService.WriteBook(userId, Input, author);
                return LocalRedirect("~/");
            }

            WriteBookModel model = new WriteBookModel
            {
                Input = Input,
                Options = _categoryService.GetCategoryOptions()
            };
            return View("WriteBook", model);
        }
        /// <summary>
        /// Navigates to choose book page.
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns>Choose book page.</returns>
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
        /// <summary>
        /// Navigates to book page.
        /// </summary>
        /// <param name="bookId">Book id.</param>
        /// <returns>Book page.</returns>
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
        /// <summary>
        /// Changes users subscribed status.
        /// </summary>
        /// <param name="AuthorId">Author id.</param>
        /// <param name="IsSubscribed">Is user curently subscribed.</param>
        /// <returns>Is user subscribed.</returns>
        [Authorize]
        public bool Subscribe(string AuthorId, bool IsSubscribed)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _subscriptionService.Subscribe(AuthorId, userId, IsSubscribed);
        }
        /// <summary>
        /// Shows pdf document of the book.
        /// </summary>
        /// <param name="bookId">Book id.</param>
        /// <returns>pdf document.</returns>
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
        /// <summary>
        /// Downloads pdf document of the book.
        /// </summary>
        /// <param name="bookId">Book id.</param>
        /// <returns>pdf dovument.</returns>
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
        /// <summary>
        /// Writes comment for book.
        /// </summary>
        /// <param name="bookId">Book id.</param>
        /// <param name="CommentInput">Form for the comment.</param>
        [Authorize]
        public void Comment(int bookId, string CommentInput)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _bookDataService.SaveComment(bookId, userId, CommentInput);
        }        
        /// <summary>
        /// Rates book
        /// </summary>
        /// <param name="bookId">Book id.</param>
        /// <param name="rating">raiting.</param>
        /// <returns>String if user is authenticated</returns>
        public string Rate(int bookId, int rating)
        {
            if (!User.Identity.IsAuthenticated) {
                return "301";
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _bookDataService.RateBook(bookId, userId, rating);
            return "";
        }
        /// <summary>
        /// Navigates to whishlist.
        /// </summary>
        /// <returns>Whishlist page.</returns>
        [Authorize]
        public IActionResult Wishlist()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(_wishlistService.MapBooksToWishlistModel(userId));
        }

        /// <summary>
        /// Adds book to users whishlist.
        /// </summary>
        /// <param name="bookDataModel">Whishlist form.</param>
        /// <param name="IsOnWishlist">Is on whishlist.</param>
        /// <returns>Is on whishlist.</returns>
        [Authorize]
        [HttpPost]
        public bool AddToWishlist(BookDataModel bookDataModel, bool IsOnWishlist)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _wishlistService.AddToWishlist(bookDataModel.Book.Id, userId, IsOnWishlist);
        }
    }
}