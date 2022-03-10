using AuthorsAngularTask.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAngularTask.Dtos
{
    public class BooksCreate
    {

     
   
        [Required]
        public string Title { get; set; }
        public DateTime DateOfPublication { get; set; }
        public string CoverPath { get; set; }
        public string Description { get; set; }
        public  ICollection<Author> Authors { get; set; }

    }
}
