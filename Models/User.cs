using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FanFicFabliaux.Models
{
    /* User već ima sva predviđena polja u svojoj identity klasi koju nasljeđuje */
    /// <summary>
    /// Contains information about user.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// List of books writen by the user.
        /// </summary>
        public virtual ICollection<Book> Books { get; set; }
        /// <summary>
        /// List of comments left by the user.
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }
        /// <summary>
        /// List of book states for the user.
        /// </summary>
        public virtual ICollection<BookState> BookStates { get; set; }
        /// <summary>
        /// List of users subscriptions.
        /// </summary>
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        /// <summary>
        /// List of users subscribers.
        /// </summary>
        public virtual ICollection<Subscription> Subscribers { get; set; }
    }
}
