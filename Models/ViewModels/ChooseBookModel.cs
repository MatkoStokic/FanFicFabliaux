using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace FanFicFabliaux.Models.ViewModels
{
    public class ChooseBookModel
    {
        public List<Book> Books { get; set; }

        [BindProperty]
        public BookFilter Filter { get; set; }

        public List<SelectListItem> Options { get; set; }


        public class BookFilter
        {
            public string AuthorName { get; set; }
            public string BookName { get; set; }
            public string Tags { get; set; }
            public int? Genre { get; set; }
        }
    }
}
