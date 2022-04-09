using Microsoft.AspNetCore.Mvc;

namespace FanFicFabliaux.Models.ViewModels
{
    public class BookDataModel
    {
        [BindProperty]
        public Book Book { get; set; }

        public string AverageRating { get; set; }
        public bool IsSubscribed { get; set; }
    }
}