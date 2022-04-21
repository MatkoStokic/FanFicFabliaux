using FanFicFabliaux.Data;
using FanFicFabliaux.Models;
using FanFicFabliaux.Models.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFicFabliaux.Services
{
    /// <summary>
    /// Contains methods related to subscription.
    /// </summary>
    public class SubscriptionService
    {
        private readonly ApplicationDbContext _context;
        private readonly MailService _mailService;
        /// <summary>
        /// Initializes SubscriptionService.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mailService"></param>
        public SubscriptionService(ApplicationDbContext context, MailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }
        /// <summary>
        /// Sendes email to subscriber.
        /// </summary>
        /// <param name="authorId">Author id.</param>
        /// <param name="bookId">Book id.</param>
        /// <returns></returns>
        public async Task SendMailToSubscribersAsync(string authorId, int bookId)
        {
            List<Subscription> subscriptions = _context.Subscriptions
                .Where(sub => sub.AuthorId.Equals(authorId))
                .ToList();
            User author = _context.Users.Find(authorId);
            Book book = _context.Books.Where(b => b.Id.Equals(bookId)).FirstOrDefault();

            foreach (Subscription subscription in subscriptions)
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

        internal bool Subscribe(string authorId, string userId, bool unsubscribe)
        {
            bool isSubscribed = unsubscribe;
            Subscription subscription = _context.Subscriptions
                .Where(sub => sub.AuthorId.Equals(authorId) && sub.UserId.Equals(userId))
                .FirstOrDefault();

            if (subscription != null && unsubscribe)
            {
                _context.Subscriptions.Remove(subscription);

                isSubscribed = false;
            }
            else if (subscription == null && !unsubscribe)
            {
                _context.Subscriptions.Add(new Subscription
                {
                    AuthorId = authorId,
                    UserId = userId
                });

                isSubscribed = true;
            }

            _context.SaveChanges();
            return isSubscribed;
        }
    }
}