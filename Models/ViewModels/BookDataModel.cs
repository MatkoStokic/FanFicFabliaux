using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FanFicFabliaux.Models.ViewModels
{
    public class BookDataModel
    {
        [BindProperty]
        public Book Book { get; set; }
        public string AverageRating { get; set; }

        public bool IsOnWishlist { get; set; }

        public bool IsSubscribed { get; set; }

        public string CommentInput { get; set; }


        public List<Comment> Comments { get; set; }

        public int UserRating { get; set; }
    }
}