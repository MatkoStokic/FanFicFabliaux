using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FanFicFabliaux.Models
{
    /// <summary>
    /// Contains Inforimation about book for specific user.
    /// </summary>
    public class BookState
    {
        /// <summary>
        /// Primary key of the book state.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Page user last saw.
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Rating user gave to the book.
        /// </summary>
        public int Rating { get; set; }
        /// <summary>
        /// Is book on users whislist.
        /// </summary>
        public bool IsOnWishList { get; set; }

        /// <summary>
        /// Foreign key for the user.
        /// </summary>
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        /// <summary>
        /// Informatin about the user.
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Foreign key for the book
        /// </summary>
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        /// <summary>
        /// Information about the book.
        /// </summary>
        public Book Book { get; set; }
    }
}
