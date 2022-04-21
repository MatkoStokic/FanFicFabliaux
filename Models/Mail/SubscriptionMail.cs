using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFicFabliaux.Models.Mail
{
    /// <summary>
    /// Contains information about subscription mail.
    /// </summary>
    public class SubscriptionMail : MailRequest
    {
        /// <summary>
        /// Name of subscribed user.
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Name of the book author.
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Title of the book.
        /// </summary>
        public string BookTitle { get; set; }
        /// <summary>
        /// Genre of the book.
        /// </summary>
        public string Genre { get; set; }
        /// <summary>
        /// Date of sending.
        /// </summary>
        public DateTime Date{ get; set; }
    }
}
