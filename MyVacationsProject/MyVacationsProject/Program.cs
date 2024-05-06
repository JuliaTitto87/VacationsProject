using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vacations_BLL;
using Vacations_DomainModel.Models.Identity;
using Vacations_DomainModel.Models.Vacation;

namespace MyVacationsProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages();


            Startup.AddServices(builder);
            BLL_Startup.RegisterModule(builder.Services);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            DBInitializer.InitializeDB(app.Services);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

          /*  using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Manager", "Member" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }*/


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseEndpoints(configure: endpoints =>
            {
                endpoints.MapControllerRoute(
                     "create",
                     "Vacations/Create/{errorMessage}",
                    new { controller = "VacationsController", action = "Create" }
                    );
            });
            app.UseEndpoints(configure: endpoints =>
            {
                endpoints.MapControllerRoute(
                     "delete",
                     "Vacations/DeletePartOfVacation/{id}",
                    new { controller = "VacationsController", action = "DeletePartOfVacation" }
                    );
            });

            app.UseEndpoints(configure: endpoints =>
            {
                endpoints.MapControllerRoute(
                     "delete",
                     "Employees/Create/{userId}",
                    new { controller = "EmployeesController", action = "Create" }
                    );
            });

            app.MapRazorPages();
            app.Run();
        }
    }
}
