using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContactsCatalog.Data;
using ContactsCatalog.Models;

namespace ContactsCatalogWebApp.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly ContactsCatalog.Data.CustomerContext _context;

        public CreateModel(ContactsCatalog.Data.CustomerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CustomerItemDTO CustomerItemDTO { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.CustomerItems == null || CustomerItemDTO == null)
            {
                return Page();
            }


            var entry = _context.Add(new CustomerItem());
            entry.CurrentValues.SetValues(CustomerItemDTO);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
