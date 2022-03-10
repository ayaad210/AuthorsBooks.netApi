using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAngularTask.Models
{
    public class Book
    {
        public Book()
        {
            this.Authors = new HashSet<Author>();

        }
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime DateOfPublication { get; set; }
        public string CoverPath { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Author> Authors { get; set; }


    }
}
