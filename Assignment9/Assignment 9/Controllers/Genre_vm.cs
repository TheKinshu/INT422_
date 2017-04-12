using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_9.Controllers
{
    public class GenreBase
    {
        public GenreBase()
        {       
        }
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}