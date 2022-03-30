using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FanFicFabliaux.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}