using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FanFicFabliaux.Models
{
    /* User već ima sva predviđena polja u svojoj identity klasi koju nasljeđuje */
    public class User : IdentityUser
    {
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<BookState> BookStates { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public virtual ICollection<Subscription> Subscribers { get; set; }
    }
}
