using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FanFicFabliaux.Models
{
    /// <summary>
    /// Contains all informations about book.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Primary key of book.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Title of the book.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Name of the author.
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Location of the book.
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// Date of creation.
        /// </summary>
        public DateTime IssueDate { get; set; }
        /// <summary>
        /// Number of pages.
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// Time of last change.
        /// </summary>
        public DateTime LastUpdateDate { get; set; }
        /// <summary>
        /// URL.
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Foreign key for the author.
        /// </summary>
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        /// <summary>
        /// Author information.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Foreign key for the category.
        /// </summary>
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        /// <summary>
        /// Category information.
        /// </summary>
        public Category Category { get; set; }
        /// <summary>
        /// Comment getter and setter.
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }
        /// <summary>
        /// Book state getter and setter.
        /// </summary>
        public virtual ICollection<BookState> BookStates { get; set; }
        /// <summary>
        /// Book tags getter and setter.
        /// </summary>
        public virtual ICollection<BookTag> BookTags { get; set; }
    }
}
