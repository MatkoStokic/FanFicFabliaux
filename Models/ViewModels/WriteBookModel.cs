using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        }
    }
}
