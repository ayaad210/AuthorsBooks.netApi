using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAngularTask.Models
{
    public class Author
    {
        public Author()
        {
           this.Books = new HashSet<Book>();

        }
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Bio { get; set; }
        public string ImagePath { get; set; }
        public virtual ICollection<Book> Books { get; set; }

    }
}
