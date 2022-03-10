using AuthorsAngularTask.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.API.Data
{

     public  interface IUnitOfWork
    {

        public IAuthorRepo GetAuthorRepo();
        public IBookRepo GetBookRepo();
        public Task<int> SaveChanges();

    }


}
