using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerlessApp.Models
{
    public class Movies
    {
        public Movies()
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
    }
}
