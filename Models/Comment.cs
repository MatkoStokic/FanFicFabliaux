using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FanFicFabliaux.Models
{
    /// <summary>
    /// Contains information about comment
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Primary key of the comment
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Foreign key for the book.
        /// </summary>
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        /// <summary>
        /// Information about the book.
        /// </summary>
        public Book Book { get; set; }
        /// <summary>
        /// Foreign key for the user.
        /// </summary>
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        /// <summary>
        /// Information about the user
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Text of the comment.
        /// </summary>
        public string CommentText { get; set; }
    }
}
