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
    public class IndexModel : PageModel
    {
        private readonly ContactsCatalog.Data.CustomerContext _context;


        public IndexModel(ContactsCatalog.Data.CustomerContext context)
        {
            _context = context;
        }

        public IList<CustomerItemDTO> CustomerItemDTO { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.CustomerItems != null)
            {
                var customers = from m in _context.CustomerItems
                             select m;

                if (!string.IsNullOrEmpty(SearchString))
                {
                    customers = customers.Where(s => s.Name.Contains(SearchString));
                }

                CustomerItemDTO = await customers.Select(x => CustomerContext.CustomerToDTO(x)).ToListAsync();
            }
        }
    }
}
