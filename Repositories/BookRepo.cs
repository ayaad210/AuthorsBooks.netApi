using AuthorsAngularTask.Data;
using AuthorsAngularTask.Dtos;
using AuthorsAngularTask.Interfaces;
using AuthorsAngularTask.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAngularTask.Repositories
{
    public class BookRepo:IBookRepo
    {
        DatabaseContext _DbContext;
        public BookRepo(DatabaseContext DbContext)
        {
            _DbContext = DbContext;
        }

      
        public Task<Book> CreateBook(Book model)
        {
            Task<Book> T = Task.Factory.StartNew(  () =>
            {
                List<Author> authers = model.Authors.ToList();
                model.Authors.Clear();

                foreach (var item in authers)
                {
                    Author abj = _DbContext.Authors.Find(item.Id);
                    if (abj != null)
                    {
                        model.Authors.Add(abj);
                    }
                }
                _DbContext.Books.Add(model);
                return model;
            });

            return T;
        }

        public Task<Book> DeleteBook(int id)
        {
            Task<Book> T = Task.Factory.StartNew(() =>
            {
                var obj = _DbContext.Books.Find(id);
                if (obj != null) _DbContext.Books.Remove(obj);
                return obj;
            });


            return T;
        }

       
        public Task<Book> GetBookById(int Id)
        {
            Task<Book> T = Task.Factory.StartNew(() =>
            {
                return _DbContext.Books.Find(Id);
            });

            return T;
        }

        public Task<IEnumerable<Book>> GetBooks()
        {

            Task<IEnumerable<Book>> T = Task.Factory.StartNew(() =>
            {
                return _DbContext.Books.Include(x=>x.Authors).AsEnumerable();
            });

            return T;
        }

        public Task<IEnumerable<Book>> Search(Book model)
        {
            Task<IEnumerable<Book>> T = Task.Factory.StartNew(() =>
            {
                return _DbContext.Books.Where(x=>(model.Title!=null&&x.Title.Contains(model.Title))).Include(x => x.Authors).AsEnumerable();
            });

            return T;
        }

        public Task<Book> UpdateBook(Book book)
        {
            Task<Book> T = Task.Factory.StartNew(() =>
            {
                Book obj = _DbContext.Books.Where(b => b.Id == book.Id).Include(x => x.Authors).FirstOrDefault();
                
                if (obj != null)
                {
                   obj.Authors.Clear();
                
                    foreach (var item in book.Authors)
                    {
                        Author authobj = _DbContext.Authors.Find(item.Id);
                        if (authobj != null)
                        {
                            obj.Authors.Add(authobj);
                        }
                    }

                    obj.Title = book.Title;
                    obj.Description = book.Description;
                    obj. CoverPath= book.CoverPath;
                    obj.DateOfPublication = book.DateOfPublication;

                    _DbContext.Entry(obj).State = EntityState.Modified;
                }
                return obj;
            });

            return T;
        }
    }
}
