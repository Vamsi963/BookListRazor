using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task OnGetAsync(int? id)
        {
            if (id != null)
                Book = await _db.Book.FindAsync(id);
            else
                Book = new Book();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                //Book.Id = id;

                if (Book.Id == 0)
                    _db.Book.Add(Book);
                else
                    _db.Update(Book);

                //_db.Entry(Book).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }

    }
}
