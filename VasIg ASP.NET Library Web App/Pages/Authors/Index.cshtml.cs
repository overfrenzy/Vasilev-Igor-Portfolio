using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VasIgASP.NETLibraryWebApp.Classes;
using Microsoft.AspNetCore.Authorization;

namespace VasIgASP.NETLibraryWebApp.Pages.Authors
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Model.AuthDbContext _context;

        public IndexModel(Model.AuthDbContext context)
        {
            _context = context;
        }

        public string CurrentFilter { get; set; }

        public IList<Author> Author { get;set; }

        public async Task OnGetAsync(string searchString)
        {
            CurrentFilter = searchString;
            IQueryable<Author> authorsIQ = from s in _context.Authors select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                authorsIQ = authorsIQ.Where(s => s.Name.Contains(searchString));
            }
            Author = await authorsIQ.ToListAsync();
        }
    }
}
