using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RWSServices.Data;
using RWSServices.Models;

namespace RWSServices.Pages.Blogs
{
    public class DetailsModel : PageModel
    {
        private readonly RWSServices.Data.ApplicationDbContext _context;

        public DetailsModel(RWSServices.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Blog Blog { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string url)
        {
            if (id == null)
            {
                return NotFound();
            }

            Blog = await _context.Blogs
                .Include(b => b.ApplicationUserCreatedBy)
                .Include(b => b.ApplicationUserUpdatedBy).FirstOrDefaultAsync(m => m.BlogID == id);

            if (Blog == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
