using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAngularTask.Dtos
{
    public class AuthorCreate
    {

  
        [Required]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Bio { get; set; }
        public string ImagePath { get; set; }

        

    }
}
