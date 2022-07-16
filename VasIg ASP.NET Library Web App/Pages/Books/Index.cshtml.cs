using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VasIgASP.NETLibraryWebApp.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace VasIgASP.NETLibraryWebApp.Pages.Books
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Model.AuthDbContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(Model.AuthDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string ValueSort { get; set; }
        public string AuthorSort { get; set; }
        public string GenreSort { get; set; }
        public string LocationSort { get; set; }
        public string StatusSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public string SearchBy { get; set; }

        public PaginatedList<Book> Book { get;set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, string searchBy, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            ValueSort = sortOrder == "Value" ? "value_desc" : "Value";
            AuthorSort = sortOrder == "Author" ? "author_desc" : "Author";
            GenreSort = sortOrder == "Genre" ? "genre_desc" : "Genre";
            LocationSort = sortOrder == "Location" ? "location_desc" : "Location";
            StatusSort = sortOrder == "Status" ? "status_desc" : "Status";

            CurrentFilter = searchString;
            SearchBy = searchBy;

            IQueryable<Book> booksIQ = (IQueryable<Book>)(from s in _context.Books select s);

            if (!String.IsNullOrEmpty(searchString))
            {

                if (searchBy == "Name") {
                    booksIQ = booksIQ.Where(s => s.Name.Contains(searchString));
                }
                else if (searchBy == "Value")
                {
                    booksIQ = booksIQ.Where(s => s.Value.ToString().Contains(searchString));
                }
                else if (searchBy == "Author")
                {
                    booksIQ = booksIQ.Where(s => s.Author.Name.Contains(searchString));
                }
                else if (searchBy == "DateOfCreation")
                {
                    booksIQ = booksIQ.Where(s => s.DateOfCreation.ToString().Contains(searchString));
                }
                else if (searchBy == "Genre")
                {
                    booksIQ = booksIQ.Where(s => s.Genre.Name.Contains(searchString));
                }
                else if (searchBy == "Location")
                {
                    booksIQ = booksIQ.Where(s => s.Location.Name.Contains(searchString));
                }
            }

            switch (sortOrder)
            {
                case "name_desc":
                    booksIQ = booksIQ.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    booksIQ = booksIQ.OrderBy(s => s.DateOfCreation);
                    break;
                case "date_desc":
                    booksIQ = booksIQ.OrderByDescending(s => s.DateOfCreation);
                    break;
                case "Value":
                    booksIQ = booksIQ.OrderBy(s => s.Value);
                    break;
                case "value_desc":
                    booksIQ = booksIQ.OrderByDescending(s => s.Value);
                    break;
                case "Author":
                    booksIQ = booksIQ.OrderBy(s => s.Author);
                    break;
                case "author_desc":
                    booksIQ = booksIQ.OrderByDescending(s => s.Author);
                    break;
                case "Genre":
                    booksIQ = booksIQ.OrderBy(s => s.Genre);
                    break;
                case "genre_desc":
                    booksIQ = booksIQ.OrderByDescending(s => s.Genre);
                    break;
                case "Location":
                    booksIQ = booksIQ.OrderBy(s => s.Location);
                    break;
                case "location_desc":
                    booksIQ = booksIQ.OrderByDescending(s => s.Location);
                    break;
                case "Status":
                    booksIQ = booksIQ.OrderBy(s => s.Status);
                    break;
                case "status_desc":
                    booksIQ = booksIQ.OrderByDescending(s => s.Status);
                    break;
                default:
                    booksIQ = booksIQ.OrderBy(s => s.Name);
                    break;
            }

            booksIQ = booksIQ.Include(s => s.Author);
            booksIQ = booksIQ.Include(s => s.Genre);
            booksIQ = booksIQ.Include(s => s.Location);

            var pageSize = Configuration.GetValue("PageSize", 4);
            Book = await PaginatedList<Book>.CreateAsync(booksIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
