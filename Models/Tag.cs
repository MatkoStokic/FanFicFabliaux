using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FanFicFabliaux.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string TagName { get; set; }

        public virtual ICollection<BookTag> BookTags { get; set; }
    }
}
