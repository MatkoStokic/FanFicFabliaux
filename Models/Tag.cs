using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FanFicFabliaux.Models
{
    /// <summary>
    /// Contains information about tag.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Primary key of the tag.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Name of the tag.
        /// </summary>
        public string TagName { get; set; }
        /// <summary>
        /// Getter and setter for BookTag
        /// </summary>
        public virtual ICollection<BookTag> BookTags { get; set; }
    }
}
