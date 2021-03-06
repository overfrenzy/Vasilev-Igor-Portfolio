using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using VasIgASP.NETLibraryWebApp.classes;

namespace VasIgASP.NETLibraryWebApp.Pages
{
    [Authorize]
    public class ReportModel : PageModel
    {
        private readonly Model.AuthDbContext _context;

        public ReportModel(Model.AuthDbContext context)
        {
            _context = context;
        }

        public IList<Report> Books { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<Report> data =
                from Report in _context.Books
                group Report by Report.AcquirementDate into dateGroup
                select new Report()
                {
                    AcquirementDate = (System.DateTime?)dateGroup.Key,
                    BookCount = dateGroup.Count()
                };

            Books = await data.AsNoTracking().ToListAsync();
        }
    }
}