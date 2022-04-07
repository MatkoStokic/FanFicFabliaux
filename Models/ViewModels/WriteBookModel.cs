using FanFicFabliaux.Models.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FanFicFabliaux.Models.ViewModels
{
    public class WriteBookModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public List<SelectListItem> Options { get; set; }

        public class InputModel
        {
            [Required]
            public string Naslov { get; set; }

            public string Oznake { get; set; }

            [Required]
            public int? Zanr { get; set; }

            [Required]
            public string Tekst { get; set; }

            [Required]
            [AllowedExtensions(new string[] {".pdf"})]
            public IFormFile Datoteka { get; set; }
        }
    }
}
