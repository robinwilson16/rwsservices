using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RWSServices.Data;
using RWSServices.Models;

namespace RWSServices.Pages.Blogs
{
    public class IndexModel : PageModel
    {
        private readonly RWSServices.Data.ApplicationDbContext _context;

        public IndexModel(RWSServices.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Blog> Blog { get;set; }

        public async Task OnGetAsync()
        {
            Blog = await _context.Blogs
                .Include(b => b.ApplicationUserCreatedBy)
                .Include(b => b.ApplicationUserUpdatedBy)
                .AsNoTracking()
                .ToListAsync();
        }

        public static string StrippedHTMLSummaryText(string HTMLString)
        {
            //First convert new lines to spaces
            HTMLString = Regex.Replace(HTMLString, @"<br ?\/?>", " ").Trim();
            HTMLString = Regex.Replace(HTMLString, @"<\/p>", " ").Trim();
            HTMLString = Regex.Replace(HTMLString, @"<\/h[1-9]>", ". ").Trim();

            //First remove any HTML tags from summary text
            HTMLString = Regex.Replace(HTMLString, @"<[^>]+>|&nbsp;", "").Trim();

            //Now remove any double spaces
            HTMLString = Regex.Replace(HTMLString, @"\s{2,}", " ");

            //Now shorten to show only the introduction if it is longer
            if (HTMLString.Length > 50)
            {
                HTMLString = HTMLString.Substring(0, 50) + "...";
            }

            return HTMLString;
        }
    }
}
