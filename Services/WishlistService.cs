using FanFicFabliaux.Data;
using FanFicFabliaux.Models;
using FanFicFabliaux.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
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
            var bookStates = _context.BookStates.Where(bs => bs.UserId == userId).Where(bs => bs.IsOnWishList == true).ToList();


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

        public void AddToWishlist(int bookId, string userId)
        {
            if (!BookStateExists(bookId, userId))
            {
                _context.BookStates.Add(new BookState()
                {
                    IsOnWishList = true,
                    BookId = bookId,
                    UserId = userId
                });
            }
            else
            {
                var update = _context.BookStates.Where(bs => bs.BookId == bookId && bs.UserId == userId).FirstOrDefault();

                update.IsOnWishList = true;

                _context.BookStates.Update(update);
            }

            _context.SaveChanges();

            //return isOnWishlist;

        }


        public void RemoveFromWishlist(int bookId, string userId)
        {
            var update = _context.BookStates.Where(bs => bs.BookId == bookId && bs.UserId == userId).FirstOrDefault();
            update.IsOnWishList = false;

            _context.Update(update);

            _context.SaveChanges();

            //return isOnWishlist;

        }
        
        
        private bool BookStateExists(int bookId, string userId)
        {
            var bookState = _context.BookStates.Where(bs => bs.BookId == bookId && bs.UserId == userId).FirstOrDefault();
            if (bookState is null)
                return false;
            else return true;
        }
    }
}