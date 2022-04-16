using FanFicFabliaux.Data;
using FanFicFabliaux.Models;
using FanFicFabliaux.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FanFicFabliaux.Services
{
    public class WishlistService
    {
        private readonly ApplicationDbContext _context;

        public WishlistService(ApplicationDbContext context)
        {
            _context = context;
        }

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
                    Category = book.Category.CategoryName
                });
            }

            return wishlist;
        }

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