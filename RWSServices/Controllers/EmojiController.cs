using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RWSServices.Data;

namespace RWSServices.Controllers
{
    public class EmojiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmojiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Emoji
        [Authorize(Roles = "Admin, Manager, Member")]
        public async Task<IActionResult> Index()
        {
            //return View(await _context.EmojiCategories.ToListAsync());
            return View(await _context.EmojiCategories
                .Include(e => e.Emojis)
                .AsNoTracking()
                .ToListAsync()
            );
        }
    }
}