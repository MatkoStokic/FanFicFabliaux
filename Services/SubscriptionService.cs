using FanFicFabliaux.Data;
using FanFicFabliaux.Models;
using FanFicFabliaux.Models.Mail;
using Microsoft.AspNetCore.Identity;
using System;
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
            Book book = _context.Books.Find(bookId);

            foreach(Subscription subscription in subscriptions)
            {
                SubscriptionMail mail = new SubscriptionMail
                {
                    ToEmail = author.Email,
                    Username = subscription.User.UserName,
                    Author = author.UserName,
                    BookTitle = book.Title,
                    Genre = book.Category.CategoryName,
                    Date = book.LastUpdateDate
                };

                await _mailService.SendSubscriptionEmailAsync(mail);
            }
        }
    }
}
