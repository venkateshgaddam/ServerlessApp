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
        public MoviesModel()
        {
            Movie_Id = 0;
            MovieName = "";
            Producer = "";
            DateOfRelease = DateTime.Now.Date;
            actor = new Actor();
        }
        public int Movie_Id { get; set; }
        public string MovieName { get; set; }
        public DateTime DateOfRelease { get; set; }
        public string Producer { get; set; }
        public Actor actor { get; set; }
        public void OnGet()
        {

        }
    }
}