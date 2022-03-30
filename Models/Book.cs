using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FanFicFabliaux.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Location { get; set; }
        public DateTime IssueDate { get; set; }
        public int Length { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string URL { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<BookState> BookStates { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public virtual ICollection<BookTag> BookTags { get; set; }
    }
}
