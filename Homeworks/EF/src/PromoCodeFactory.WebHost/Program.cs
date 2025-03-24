using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PromoCodeFactory.DataAccess.Data;
using PromoCodeFactory.WebHost.InitData;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<EfDataContext>();
               await  db.Database.EnsureDeletedAsync();
                db.Database.Migrate();

                var seekData =new SeekFromFakeDataFactory(scope.ServiceProvider);
                //await seekData.RoleDataSeek();
                await seekData.EmployeeDataSeek();
                await seekData.ReferenceDataSeek();
                await seekData.CustomerDataSeek();
            }
                host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}