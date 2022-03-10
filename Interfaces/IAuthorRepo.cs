using AuthorsAngularTask.Dtos;
using AuthorsAngularTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAngularTask.Interfaces
{
    public interface IAuthorRepo
    {
        public Task<IEnumerable<Author>> GetAuthors();
        public Task<Author> GetAuthorById(int Id);

        public Task<Author> UpdateAuthor(Author author);
        public Task<Author> CreateAuthor(Author model);

        public Task<Author> DeleteAuthor(int id);


    }
}
