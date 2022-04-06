using FanFicFabliaux.Data;
using FanFicFabliaux.Models;
using FanFicFabliaux.Models.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFicFabliaux.Services
{
    public class SubscriptionService
    {
        private readonly ApplicationDbContext _context;
        private readonly MailService _mailService;

        public SubscriptionService(ApplicationDbContext context, MailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        public async Task SendMailToSubscribersAsync(string authorId, int bookId)
        {
            List<Subscription> subscriptions = _context.Subscriptions
                .Where(sub => sub.AuthorId.Equals(authorId))
                .ToList();
            User author = _context.Users.Find(authorId);
            Book book = _context.Books.Where(b => b.Id.Equals(bookId)).FirstOrDefault();

            foreach(Subscription subscription in subscriptions)
            {
                User user = _context.Users.Find(subscription.UserId);
                Category category = _context.Categories.Find(book.CategoryId);

                SubscriptionMail mail = new SubscriptionMail
                {
                    ToEmail = user.Email,
                    Username = user.UserName,
                    Author = author.UserName,
                    BookTitle = book.Title,
                    Genre = category.CategoryName,
                    Date = book.LastUpdateDate
                };

                await _mailService.SendSubscriptionEmailAsync(mail);
            }
        }
    }
}
