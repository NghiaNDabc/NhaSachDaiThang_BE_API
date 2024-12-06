// DbContextConfiguration.cs
using Microsoft.EntityFrameworkCore;
using NhaSachDaiThang_BE_API.Data;

namespace NhaSachDaiThang_BE_API.Configurations
{
    public static class DbContextConfiguration
    {
        public static void AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookStoreContext>(options =>
    options.UseLazyLoadingProxies()
           .UseSqlServer(configuration.GetConnectionString("BookStoreDb")),
    ServiceLifetime.Scoped);
        }
    }
}
