﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerlessApp.Models
{
    public class Actor
    {
        public Actor()
        {
            Actor_Id = 0;
            ActorName = "";
            MovieName = "";
            Age = 0;
        }
        public int Actor_Id { get; set; }
        public string ActorName { get; set; }
        public string MovieName { get; set; }
        public int Age { get; set; }
    }
}
