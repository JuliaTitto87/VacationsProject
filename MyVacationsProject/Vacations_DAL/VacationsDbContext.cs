using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vacations_DomainModel.Models.Identity;

namespace Vacations_DAL
{
    public class VacationsDbContext : IdentityDbContext<User>
    {
        public VacationsDbContext(DbContextOptions<VacationsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
  /*          modelBuilder.Entity<IdentityRole>().HasData(
        new IdentityRole { Name = "admin", NormalizedName = "ADMIN" },
        new IdentityRole { Name = "user", NormalizedName = "USER" }
        );*/
        }
    }



}
