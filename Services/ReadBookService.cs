using FanFicFabliaux.Data;
using FanFicFabliaux.Models;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WkHtmlToPdfDotNet.Contracts;
using static FanFicFabliaux.Models.ViewModels.ChooseBookModel;

namespace FanFicFabliaux.Services
{
    public class ReadBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnviroment;
        private readonly IConverter _converter;

        public ReadBookService(
            ApplicationDbContext context,
            IWebHostEnvironment hostEnviroment,
            IConverter converter)
        {
            _context = context;
            _hostEnviroment = hostEnviroment;
            _converter = converter;
        }

        public List<Book> GetBooksByFilter(BookFilter filter)
        {
            List<Book> books;
            if (filter == null)
            {
                books = GetAll();
            }
            else
            {
                books = GetsByFilter(filter);
            }

            PopulateUsers(books);
            return books;
        }

        private void PopulateUsers(List<Book> books)
        {
            foreach (Book book in books)
            {
                User user = _context.Users.Find(book.UserId);
                book.User = user;
            }
        }

        private List<Book> GetAll()
        {
            return _context.Books
                .OrderByDescending(x => x.LastUpdateDate)
                .Take(10)
                .ToList();
        }

        private List<Book> GetsByFilter(BookFilter filter)
        {
            List<int> taggedBookIds = null;
            List<string> authorIds = null;

            if (filter.Tags != null)
            {
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
                .OrderByDescending(x => x.LastUpdateDate)
                .Take(10)
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

        internal FileStream GetPdfById(int bookId)
        {
            Book book = _context.Books.Find(bookId);
            if (book == null || book.URL == null)
            {
                return null;
            }

            var uploads = Path.Combine(_hostEnviroment.WebRootPath, "upload");
            var filePath = Path.Combine(uploads, book.URL);

            FileStream stream;
            try
            {
                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            }
            catch (FileNotFoundException)
            {
                return null;
            }

            return stream;
        }

        internal string GetBookTitle(int bookId)
        {
            return _context.Books.Find(bookId).Title + ".pdf";
        }
    }
}