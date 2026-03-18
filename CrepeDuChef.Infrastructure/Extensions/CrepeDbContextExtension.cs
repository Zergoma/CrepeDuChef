using CrepeDuChef.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CrepeDuChef.Infrastructure.Extensions
{
    public static class CrepeDbContextExtension
    {
        extension(IServiceCollection serviceCollection)
        {
            public IServiceCollection UseCrepeDuChefSqliteDb()
            {
                serviceCollection.AddDbContext<CrepeDbContext>();
                serviceCollection.AddTransient<ICrepePartyRepository, CrepePartyRepository>();

                return serviceCollection;
            }
        }
    }
}
