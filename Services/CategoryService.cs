using FanFicFabliaux.Data;
using FanFicFabliaux.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanFicFabliaux.Services
{
    /// <summary>
    /// Contains methods related to category.
    /// </summary>
    public class CategoryService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes CategoryService.
        /// </summary>
        /// <param name="context"></param>
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get list of all categories.
        /// </summary>
        /// <returns>List of category options.</returns>
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
