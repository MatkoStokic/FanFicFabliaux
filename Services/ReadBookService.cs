using FanFicFabliaux.Data;
using FanFicFabliaux.Models;
using System.Collections.Generic;
using System.Linq;
using static FanFicFabliaux.Models.ViewModels.ChooseBookModel;

namespace FanFicFabliaux.Services
{
    public class ReadBookService
    {
        private readonly ApplicationDbContext _context;

        public ReadBookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Book> GetBooksByFilter(BookFilter filter)
        {
            List<Book> books;
            if (filter == null)
            {
                books = GetAll();
            } else
            {
                books = GetsByFilter(filter);
            }

            PopulateUsers(books);
            return books;
        }

        private void PopulateUsers(List<Book> books)
        {
            foreach(Book book in books)
            {
                User user = _context.Users.Find(book.UserId);
                book.User = user;
            }
        }

        private List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        private List<Book> GetsByFilter(BookFilter filter)
        {
            List<int> taggedBookIds = null;
            List<string> authorIds = null;

            if (filter.Tags != null) {
                taggedBookIds = GetIdsByTags(filter.Tags);
            }
            if (filter.AuthorName != null)
            {
                authorIds = GetUserIdsByAuthorName(filter);
            }

            return _context.Books
                .Where(book => taggedBookIds == null || taggedBookIds.Contains(book.Id))
                .Where(book => authorIds == null || authorIds.Contains(book.UserId))
                .Where(book => filter.BookName == null || book.Title.Contains(filter.BookName))
                .Where(book => filter.Genre == null || book.CategoryId.Equals(filter.Genre))
                .ToList();
        }

        private List<string> GetUserIdsByAuthorName(BookFilter filter)
        {
            return _context.Users
                            .Where(u => u.UserName.Contains(filter.AuthorName))
                            .Select(u => u.Id)
                            .ToList();
        }

        private List<int> GetIdsByTags(string tags)
        {
            List<string> filterTags = tags.Split(" ").ToList();
            List<int> filterTagsId = _context.Tags
                    .Where(tag => filterTags.Contains(tag.TagName))
                    .Select(tag => tag.Id)
                    .ToList();

            List<int> taggedBookIds = _context.BookTags
                .Where(bt => filterTagsId.Contains(bt.TagId))
                .Select(bt => bt.BookId)
                .ToList();
            return taggedBookIds;
        }
    }
}
