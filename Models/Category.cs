using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FanFicFabliaux.Models
{
    /// <summary>
    /// Contains list of books within category
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Primary key of the category.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Name of the category.
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// List of books in this category
        /// </summary>
        public virtual ICollection<Book> Books { get; set; }
    }
}