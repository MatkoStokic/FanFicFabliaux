using FanFicFabliaux.Data;
using FanFicFabliaux.Models;
using FanFicFabliaux.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FanFicFabliaux.Services
{
    /// <summary>
    /// Contains methods related to whislist.
    /// </summary>
    public class WishlistService
    {
        private readonly ApplicationDbContext _context;
        /// <summary>
        /// Initializes WhishlistService.
        /// </summary>
        /// <param name="context"></param>
        public WishlistService(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Gets all books whishlisted by the user.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Whishlist</returns>
        public ICollection<WishlistModel> MapBooksToWishlistModel(string userId)
        {
            var bookStates = _context.BookStates.Where(bs => bs.UserId == userId).Where(bs => bs.IsOnWishList).ToList();


            var wishlist = new List<WishlistModel>();

            foreach (var bs in bookStates)
            {
                var book = _context.Books.Include(b => b.BookStates).Include(b => b.Category).Where(b => b.Id == bs.BookId).FirstOrDefault();

                wishlist.Add(new WishlistModel
                {
                    Title = book.Title,
                    Author = book.Author,
                    Length = book.Length,
                    Category = book.Category.CategoryName,
                    Book = book
                }) ;
            }

            return wishlist;
        }
        /// <summary>
        /// Adds book to whishlist.
        /// </summary>
        /// <param name="bookId">Book id.</param>
        /// <param name="userId">User id.</param>
        /// <param name="isOnWishlist">Is book on whishlist.</param>
        /// <returns>Is book in whishlist.</returns>
        public bool AddToWishlist(int bookId, string userId, bool isOnWishlist)
        {
            if (isOnWishlist)
            {
                _context.BookStates.Remove(new BookState()
                {
                    IsOnWishList = true,
                    BookId = bookId,
                    UserId = userId
                });

                isOnWishlist = false;
            }
            else
            {
                _context.BookStates.Add(new BookState()
                {
                    IsOnWishList = true,
                    BookId = bookId,
                    UserId = userId
                });

                isOnWishlist = true;
            }

            _context.SaveChanges();

            return isOnWishlist;

        }
    }
}