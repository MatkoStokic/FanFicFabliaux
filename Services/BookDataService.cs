using FanFicFabliaux.Data;
using FanFicFabliaux.Models;
using FanFicFabliaux.Models.ViewModels;
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
            Book book = _context.Books.Find(bookId);

            if (book == null)
            {
                return null;
            }

            return new BookDataModel
            {
                Book = book,
                AverageRating = GetAverageRating(bookId),
                IsSubscribed = IsSubscribed(userId, book),
                Comments = GetComments(bookId)
            };
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

            return averageRating != 0 ? String.Format("{0:0.00}", averageRating) : "Not rated yet";
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
    }
}