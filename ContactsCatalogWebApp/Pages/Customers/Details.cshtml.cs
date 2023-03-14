using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContactsCatalog.Data;
using ContactsCatalog.Models;

namespace ContactsCatalogWebApp.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly ContactsCatalog.Data.CustomerContext _context;

        public DetailsModel(ContactsCatalog.Data.CustomerContext context)
        {
            _context = context;
        }

        

        public CustomerItemDTO CustomerItemDTO { get; set; } = default!;
        
    

    public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.CustomerItems == null)
            {
                return NotFound();
            }

            var customeritem = await _context.CustomerItems.FirstOrDefaultAsync(m => m.Id == id);
            if (customeritem == null)
            {
                return NotFound();
            }
            else 
            {
                // Loads the related Roles entities associated with the User entity
                _context.Entry(customeritem).Collection(u => u.Contacts).Load();
                CustomerItemDTO = CustomerContext.CustomerToDTO(customeritem);
            }


            return Page();
        }
    }
}
