using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace FanFicFabliaux.Models.Mail
{
    /// <summary>
    /// Contains information about e-mail.
    /// </summary>
    public class MailRequest
    {
        /// <summary>
        /// Receiving e-mail.
        /// </summary>
        public string ToEmail { get; set; }
        /// <summary>
        /// Subject of the e-mail.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Body of the e-mail.
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Attachment of the e-mail.
        /// </summary>
        public List<IFormFile> Attachments { get; set; }
    }
}
