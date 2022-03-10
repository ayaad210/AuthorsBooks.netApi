using CoreIdentity.API.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAngularTask.Helbers
{
    public static class RepositoriesHelper
    {

        public static void ConfigureServices(this IServiceCollection service)
        {
  
            service.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
        }

    }
}
