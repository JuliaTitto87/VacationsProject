using Microsoft.EntityFrameworkCore;
using MyVacationsProject.Contracts.Exceptions;

namespace MyVacationsProject
{
    internal static class DBInitializer
    {
        public static void InitializeDB(IServiceProvider provider)
        {
            if (!ApplyMigrations(provider))
            {
                throw new DbInitializationException("Could not initialize DB! See errors above.");
            }
        }

        private static bool ApplyMigrations(IServiceProvider provider)
        {
            var scope = provider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DbContext>();

            try
            {
                context.Database.Migrate();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

