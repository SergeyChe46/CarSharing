using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSharing.Context;
using Microsoft.EntityFrameworkCore;

namespace CarSharing.Services
{
    public static class DatabaseRegister
    {
        public static void RegisterContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(
                opts => opts.UseNpgsql(configuration.GetConnectionString("Identity"))
            );
        }
    }
}
