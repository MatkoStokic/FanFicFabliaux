using FanFicFabliaux.Data;
using FanFicFabliaux.Models;
using FanFicFabliaux.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace FanFicFabliaux.Services
{
    public class WriteBookService
    {
        private readonly IWebHostEnvironment _hostEnviroment;
        private readonly IConverter _converter;
        private readonly ApplicationDbContext _context;
        private readonly SubscriptionService _subscriptionService;

        public WriteBookService(
            IWebHostEnvironment hostEnviroment,
            IConverter converter,
            ApplicationDbContext context,
            SubscriptionService subscriptionService)
        {
            _hostEnviroment = hostEnviroment;
            _converter = converter;
            _context = context;
            _subscriptionService = subscriptionService;
        }

        public async Task WriteBook(string userId, WriteBookModel.InputModel Input, string author)
        {
            var uniqueFileName = Guid.NewGuid().ToString().Substring(0, 16) + ".pdf";
            var uploads = Path.Combine(_hostEnviroment.WebRootPath, "upload");
            var filePath = Path.Combine(uploads, uniqueFileName);

            if (Input.Tekst != null)
            {
                GeneratePdf(Input.Naslov, Input.Tekst, filePath);
            }
            else
            {
                using Stream stream = new FileStream(filePath, FileMode.CreateNew);
                await Input.Datoteka.CopyToAsync(stream);
            }

            Book book = await SaveToDb(userId, Input.Naslov, Input.Oznake, Input.Zanr.Value, uniqueFileName, author);
            await _subscriptionService.SendMailToSubscribersAsync(userId, book.Id);
        }

        private async Task<Book> SaveToDb(string userId, string naslov, string oznake, int zanr, string uniqueFileName, string author)
        {
            Book book = new Book
            {
                UserId = userId,
                Title = naslov,
                URL = uniqueFileName,
                IssueDate = DateTime.Now,
                LastUpdateDate = DateTime.Now,
                CategoryId = zanr,
                Author = author
            };

            await _context.Books.AddAsync(book);

            string[] odvojeneOznake = oznake != null ? oznake.Split(' ') : new string[0];
            List<BookTag> bookTags = new List<BookTag>();
            List<Tag> tags = new List<Tag>();

            foreach (string odvojenaOznaka in odvojeneOznake)
            {
                Tag tag = _context.Tags.AsQueryable().Where(tag => tag.TagName.Equals(odvojenaOznaka)).FirstOrDefault();
                if (tag == null)
                {
                    tag = new Tag
                    {
                        TagName = odvojenaOznaka
                    };
                    await _context.Tags.AddAsync(tag);
                }

                tags.Add(tag);
            }

            await _context.SaveChangesAsync();

            tags.ForEach(tag => bookTags.Add(
                new BookTag
                {
                    BookId = book.Id,
                    TagId = tag.Id
                }
            ));

            await _context.BookTags.AddRangeAsync(bookTags);
            await _context.SaveChangesAsync();

            return book;
        }

        public void GeneratePdf(string naslov, string tekst, string path)
        {
            var html = $@"
               <!DOCTYPE html>
               <html>
               <head>
                <meta charset='utf-8'>
               </head>
              <body>
              <h1> {naslov} </h1>
              <pre> {tekst} </pre>
              </body>
              </html>
              ";
            GlobalSettings globalSettings = new GlobalSettings();
            globalSettings.ColorMode = ColorMode.Color;
            globalSettings.Orientation = Orientation.Portrait;
            globalSettings.PaperSize = PaperKind.A4;
            globalSettings.Margins = new MarginSettings { Top = 25, Bottom = 25 };
            ObjectSettings objectSettings = new ObjectSettings();
            objectSettings.PagesCount = true;
            objectSettings.HtmlContent = html;
            WebSettings webSettings = new WebSettings();
            webSettings.DefaultEncoding = "utf-8";
            HeaderSettings headerSettings = new HeaderSettings();
            headerSettings.FontSize = 15;
            headerSettings.FontName = "Ariel";
            headerSettings.Right = "Page [page] of [toPage]";
            headerSettings.Line = true;
            FooterSettings footerSettings = new FooterSettings();
            footerSettings.FontSize = 12;
            footerSettings.FontName = "Ariel";
            footerSettings.Line = true;
            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;
            globalSettings.Out = path;
            HtmlToPdfDocument htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            _converter.Convert(htmlToPdfDocument);
        }
    }
}