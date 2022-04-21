using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FanFicFabliaux.Models
{
    /// <summary>
    /// Connects book to tag
    /// </summary>
    public class BookTag
    {
        /// <summary>
        /// Primary key of tag-book relation.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Foreign key for the tag.
        /// </summary>
        [ForeignKey(nameof(Tag))]
        public int TagId { get; set; }
        /// <summary>
        /// Information about the tag.
        /// </summary>
        public Tag Tag { get; set; }

        /// <summary>
        /// Foreign key for the book.
        /// </summary>
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        /// <summary>
        /// Information about the book.
        /// </summary>
        public Book Book { get; set; }
    }
}
