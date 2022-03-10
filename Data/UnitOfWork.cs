using AuthorsAngularTask.Data;
using AuthorsAngularTask.Interfaces;
using AuthorsAngularTask.Repositories;



using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        DatabaseContext _DbContext;
        Dictionary<string, object> Instances = new Dictionary<string, object>();

        public UnitOfWork(DatabaseContext DbContext)
        {
            _DbContext = DbContext;
        }

        public IAuthorRepo GetAuthorRepo()
        {
            if (!Instances.ContainsKey(nameof(IAuthorRepo)))
            {
                Instances.Add(nameof(IAuthorRepo), new AuthorRepo(_DbContext));
            }

            return (AuthorRepo)Instances.GetValueOrDefault(nameof(IAuthorRepo));
         }
    
        public IBookRepo GetBookRepo()
        {
            if (!Instances.ContainsKey(nameof(IBookRepo)))
            {
                Instances.Add(nameof(IBookRepo), new BookRepo(_DbContext));
            }

            return (BookRepo)Instances.GetValueOrDefault(nameof(IBookRepo));
        }

   

        public Task<int> SaveChanges()
        {
            return _DbContext.SaveChangesAsync();
        }



        //public object GetRepo<TEntity> () 
        //{
        //   //var res= new Microsoft.Extensions.DependencyInjection.ServiceCollection().FirstOrDefault(x => x.ServiceType == typeof(TEntity));

        //    if (!Instances.ContainsKey(nameof(TEntity))) 
        //    {
        //        TEntity obj = (TEntity)Activator.CreateInstance(typeof(TEntity), new object[] { _DbContext });
        //        Instances.Add(nameof(TEntity), obj);
        //    }
        //    return (TEntity)Instances.GetValueOrDefault(nameof(TEntity));
        //}
    }
}

