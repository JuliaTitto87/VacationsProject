using Microsoft.EntityFrameworkCore;
using MyVacationsProject.ConfigurationSection;
using Serilog;
using Vacations_DAL;
using Vacations_BLL.Services;
using Vacations_DomainModel.Models.Identity;
using ILogger = Serilog.ILogger;
using Vacations_BLL.Contracts;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;
using System.Reflection;

namespace MyVacationsProject
{
    internal struct InterfaceToImplementation
    {
        public Type Implementation;
        public Type Interface;
    }
    public class Startup
    {
        internal static void AddServices(WebApplicationBuilder builder)
        {
            AddSerilog(builder);

            RegisterDAL(builder);

            RegisterIdentity(builder);
        }

        private static void RegisterIdentity(WebApplicationBuilder builder)
        {
            builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<DbContext>();

        /*    builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
                options.AddPolicy("Member", policy => policy.RequireRole("Member"));
            });*/

        }

        public static void RegisterDAL(WebApplicationBuilder builder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            var services = builder.Services;

            var connectionString = builder.Configuration.GetConnectionString("Default");
            services.AddTransient<DbContextOptions<VacationsDbContext>>(provider =>
            {
                var builder = new DbContextOptionsBuilder<VacationsDbContext>();
                builder.UseNpgsql(connectionString);
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

                return builder.Options;
            });

            services.AddScoped<DbContext, VacationsDbContext>();

            services.AddScoped<IUnitOfWork>(prov =>
            {
                var context = prov.GetRequiredService<DbContext>();
                return new UnitOfWork(context);
            });
        }

        internal static void AddSerilog(WebApplicationBuilder builder)
        {
            var serilogConfig = builder.Configuration.GetSection(nameof(SerilogConfig)).Get<SerilogConfig>();
            var logFilePath = Path.Combine(serilogConfig?.LoggingDir ?? "./", "log.txt");

            var loggerConfig = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Month,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}");

            if (builder.Environment.IsDevelopment())
            {
                loggerConfig = loggerConfig.MinimumLevel.Debug();
            }
            else
            {
                loggerConfig = loggerConfig.MinimumLevel.Warning();
            }

            var logger = loggerConfig.CreateLogger();

            builder.Services.AddSingleton<ILogger>(logger);
        }
    }
}
