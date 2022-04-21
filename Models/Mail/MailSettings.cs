using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFicFabliaux.Models.Mail
{
    /// <summary>
    /// Contains settings for e-mail.
    /// </summary>
    public class MailSettings
    {
        /// <summary>
        /// Mail addres from which to send.
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// Name that will be displayed.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Password of the mail.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Host from which to send.
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Port from which to send.
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// App Link.
        /// </summary>
        public string AppLink { get; set; }
    }
}
