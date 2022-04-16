using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FanFicFabliaux.Models.ViewModels
{
    public class WishlistModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Length { get; set; }
        public string Category { get; set; }

        [BindProperty]
        public Book Book { get; set; }
    }
}
