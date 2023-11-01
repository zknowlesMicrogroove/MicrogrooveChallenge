using MicrogrooveChallenge.BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using MicrogrooveChallenge.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using MicrogrooveChallenge.BLL.JsonConverters;

[assembly: FunctionsStartup(typeof(MicrogrooveChallenge.Startup))]
namespace MicrogrooveChallenge
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<CustomerContext>(options => options.UseSqlite(DatabaseFileLocator.GetConnectionString()));
            builder.Services.AddHttpClient("uiAvatarsClient", c => c.BaseAddress = new System.Uri("https://ui-avatars.com")); ;
            builder.Services.AddScoped<IAvatarService, AvatarService>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
