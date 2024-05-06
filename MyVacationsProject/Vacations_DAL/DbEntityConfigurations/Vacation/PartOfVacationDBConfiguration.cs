using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacations_DAL.DbEntityConfigurations.Vacation
{
    internal class PartOfVacationDBConfiguration : IEntityTypeConfiguration<Vacations_DomainModel.Models.Vacation.PartOfVacation>
    {
        public void Configure(EntityTypeBuilder<Vacations_DomainModel.Models.Vacation.PartOfVacation> builder)
        {

                builder.HasOne(prf => prf.Vacation)
                    .WithMany(p => p.PartsOfVacation)
                    .HasForeignKey(prf => prf.VacationId);

        }
    }
}
