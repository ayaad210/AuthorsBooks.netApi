using AuthorsAngularTask.Data;
using AuthorsAngularTask.Dtos;
using AuthorsAngularTask.Interfaces;
using AuthorsAngularTask.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAngularTask.Repositories
{
    public class AuthorRepo : IAuthorRepo
    {
        DatabaseContext _DbContext;
        public AuthorRepo(DatabaseContext DbContext)
        {
            _DbContext = DbContext;
        }
        public Task<Author> GetAuthorById(int Id)
        {

            Task<Author> T = Task.Factory.StartNew(() =>
            {
                return _DbContext.Authors.Find(Id);
            });

            return T;

        }

        public Task<IEnumerable<Author>> GetAuthors()
        {
            Task<IEnumerable<Author>> T = Task.Factory.StartNew(() =>
            {

                return _DbContext.Authors.AsEnumerable();
            });

            return T;
        }
        public  Task<Author> CreateAuthor(Author author)
        {
        
           

            Task<Author> T = Task.Factory.StartNew( () =>
            {
                _DbContext.Authors.Add(author);
                return author;
            });

            return T;

        }

        public Task<Author> DeleteAuthor(int id)
        {

            Task<Author> T = Task.Factory.StartNew(() =>
            {
                var obj = _DbContext.Authors.Find(id);
                if(obj!=null) _DbContext.Authors.Remove(obj);
                return obj;
            });


            return T;

        }

        public Task<Author> UpdateAuthor(Author author)
        {
  
            Task<Author> T = Task.Factory.StartNew(() =>
            {
                var obj = _DbContext.Authors.Find(author.Id);
                
                if (obj != null)
                {
                    obj.Name = author.Name;
                    obj.DateOfBirth = author.DateOfBirth;
                    obj.ImagePath = author.ImagePath;
                    obj.Bio = author.Bio;                   
                    _DbContext.Entry(obj).State = EntityState.Modified;
                }
                return obj;
            });

            return T;

        }
    }
}
