using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Data;
using PromoCodeFactory.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services,string stringConnection)
        {
            services.AddDbContext<EfDataContext>(options =>
            {
                options.UseSqlite(stringConnection);
            });
            //Add repositories
            services.AddScoped<IRepository<Role>, EfRepository<Role>>();
            services.AddScoped<IRepository<Employee>,EfRepository<Employee>>();
            services.AddScoped<IRepository<Customer>, EfRepository<Customer>>();
            services.AddScoped<IRepository<Preference>, EfRepository<Preference>>();
            services.AddScoped<IRepository<PromoCode>, EfRepository<PromoCode>>();
            return services;
        }
    }
}
