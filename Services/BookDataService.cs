using FanFicFabliaux.Data;
using FanFicFabliaux.Models;
using FanFicFabliaux.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FanFicFabliaux.Services
{
    public class BookDataService
    {
        private readonly ApplicationDbContext _context;

        public BookDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public BookDataModel GetModel(int bookId, string userId)
        {
            Book book = _context.Books.Include(b => b.User).Where(b => b.Id == bookId).FirstOrDefault();

            if (book == null)
            {
                return null;
            }

            return new BookDataModel
            {
                Book = book,
                AverageRating = GetAverageRating(bookId),
                IsSubscribed = IsSubscribed(userId, book),
                Comments = GetComments(bookId),
                UserRating = GetUserRating(bookId, userId),
                IsOnWishlist = IsOnWishlist(bookId, userId)
            };
        }

        private bool IsOnWishlist(int bookId, string userId)
        {
            var bookState = _context.BookStates.Where(bs => bs.UserId == userId && bs.BookId == bookId).FirstOrDefault();
            
            if(bookState is null)
            {
                return false;
            }
            else return bookState.IsOnWishList;
        }

        public void SaveComment(int bookId, string userId, string commentText)
        {
            Comment comment = new Comment
            {
                BookId = bookId,
                UserId = userId,
                CommentText = commentText
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public void RateBook(int bookId, string userId, int rating)
        {
            BookState state = _context.BookStates
                .Where(state => state.BookId.Equals(bookId) && state.UserId.Equals(userId))
                .FirstOrDefault();

            if (state == null)
            {
                BookState newState = new BookState
                {
                    BookId = bookId,
                    UserId = userId,
                    Rating = rating
                };
                _context.BookStates.Add(newState);
            } else
            {
                state.Rating = rating;
            }

            _context.SaveChanges();
        }

        private bool IsSubscribed(string userId, Book book)
        {
            return _context.Subscriptions
                .Where(sub => sub.AuthorId.Equals(book.UserId) && sub.UserId.Equals(userId))
                .Any();
        }

        private string GetAverageRating(int bookId)
        {
            List<int> bookRatings = _context.BookStates
                .Where(state => state.BookId.Equals(bookId))
                .Select(state => state.Rating)
                .ToList();

            double averageRating = 0;

            if (bookRatings.Count != 0)
            {
                averageRating = bookRatings.Average();
            }

            return averageRating != 0 ? String.Format("{0:0.00}", averageRating) : null;
        }

        private List<Comment> GetComments(int bookId)
        {
            var comments = _context.Comments
                .Where(comment => comment.BookId.Equals(bookId))
                .Join(_context.Users,
                    comment => comment.UserId,
                    user => user.Id,
                    (comment, user) =>
                        new Comment
                        {
                            BookId = comment.BookId,
                            UserId = comment.UserId,
                            CommentText = comment.CommentText,
                            User = user
                        }
                    )
                .ToList();
            comments.Reverse();

            return comments;
        }

        private int GetUserRating(int bookId, string userId)
        {
            return _context.BookStates
                .Where(state => state.BookId.Equals(bookId) && state.UserId.Equals(userId))
                .Select(state => state.Rating)
                .FirstOrDefault();
        }
    }
}