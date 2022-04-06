using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFicFabliaux.Models.Mail
{
    public class SubscriptionMail : MailRequest
    {
        public string Username { get; set; }
        public string Author { get; set; }
        public string BookTitle { get; set; }
        public string Genre { get; set; }
        public DateTime Date{ get; set; }
    }
}
