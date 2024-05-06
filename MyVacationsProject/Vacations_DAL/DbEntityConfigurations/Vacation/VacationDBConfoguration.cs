using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Vacations_DAL.DbEntityConfigurations.Vacation
{
    internal class VacationDBConfoguration : IEntityTypeConfiguration<Vacations_DomainModel.Models.Vacation.Vacation>
    {

        public void Configure(EntityTypeBuilder<Vacations_DomainModel.Models.Vacation.Vacation> builder)
        {
            builder.HasOne(prf => prf.Employee)
                .WithMany(p => p.Vacations)
                .HasForeignKey(prf => prf.EmployeeId);
        }
    }
}
