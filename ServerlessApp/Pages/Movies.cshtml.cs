using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServerlessApp.Models;

namespace ServerlessApp.Pages
{
    public class MoviesModel : PageModel
    {
        [BindProperty]
        public Movies movies { get; set; }
        public void OnGet()
        {
            string someVar = "";

            for (int i = 0; i < 10; i++)
            {
                someVar += "test" + i.ToString();
            }


        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}