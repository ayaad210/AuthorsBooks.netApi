using AuthorsAngularTask.Dtos;
using AuthorsAngularTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAngularTask.Interfaces
{
    public interface IBookRepo
    {
        public Task<IEnumerable<Book>> GetBooks();
        public Task<Book> GetBookById(int Id);
        public Task<Book> UpdateBook(Book model);
        public Task<Book> CreateBook(Book model);
        public Task<Book> DeleteBook(int id);
        public Task<IEnumerable<Book>> Search(Book model);





    }
}
