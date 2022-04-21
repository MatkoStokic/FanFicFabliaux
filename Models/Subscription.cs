using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FanFicFabliaux.Models
{
    /// <summary>
    /// Contains information about subscription.
    /// </summary>
    public class Subscription
    {
        /// <summary>
        /// Primary key of the subscription.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Id of the user that subscribed.
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Information about the user.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Id of author of the book.
        /// </summary>
        public string AuthorId { get; set; }
        /// <summary>
        /// Informatin about the author.
        /// </summary>
        public User Author { get; set; }
    }
}
