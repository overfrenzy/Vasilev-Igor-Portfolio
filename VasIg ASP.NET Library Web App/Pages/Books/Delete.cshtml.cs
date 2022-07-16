using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VasIgASP.NETLibraryWebApp.Classes;
using Microsoft.AspNetCore.Authorization;

namespace VasIgASP.NETLibraryWebApp.Pages.Books
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly Model.AuthDbContext _context;

        public DeleteModel(Model.AuthDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Books
                .Include(p => p.Author)
                .Include(p => p.Genre)
                .Include(p => p.Location).FirstOrDefaultAsync(m => m.Id == id);

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Books.FindAsync(id);

            if (Book != null)
            {
                _context.Books.Remove(Book);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
