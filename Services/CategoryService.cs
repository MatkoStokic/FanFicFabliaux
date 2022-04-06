using FanFicFabliaux.Data;
using FanFicFabliaux.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFicFabliaux.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SelectListItem> GetCategoryOptions()
        {
            List<SelectListItem> options = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Odaberite",
                    Value = ""
                }
            };

            foreach (Category category in _context.Categories.ToList()) 
            {
                options.Add(new SelectListItem 
                { 
                    Text = category.CategoryName, 
                    Value = category.Id.ToString() 
                });
            }


            return options;
        }
    }
}
