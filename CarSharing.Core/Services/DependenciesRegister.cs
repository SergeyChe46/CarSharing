using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSharing.Core.Repository;
using CarSharing.Repositories;

namespace CarSharing.Core.Services
{
    public static class DependenciesRegister
    {
        public static void RepositoryManagerRegister(this IServiceCollection services)
        {
            //services.AddScoped(typeof(BaseRepository<>));
            services.AddScoped<CarRepository>();
            services.AddScoped<RepositoryManager>();
        }
    }
}
