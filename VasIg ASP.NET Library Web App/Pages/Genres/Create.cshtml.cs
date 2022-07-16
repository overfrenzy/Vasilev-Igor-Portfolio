using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VasIgASP.NETLibraryWebApp.Classes;
using Microsoft.AspNetCore.Authorization;

namespace VasIgASP.NETLibraryWebApp.Pages.Genres
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly Model.AuthDbContext _context;

        public CreateModel(Model.AuthDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Genre Genre { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Genres.Add(Genre);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
