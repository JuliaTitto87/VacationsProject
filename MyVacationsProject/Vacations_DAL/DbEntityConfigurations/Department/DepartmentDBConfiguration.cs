using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Vacations_DAL.DbEntityConfigurations.Department
{
    internal class DepartmentDBConfiguration : IEntityTypeConfiguration<Vacations_DomainModel.Models.Department.Department>
    {
        public void Configure(EntityTypeBuilder<Vacations_DomainModel.Models.Department.Department> builder)
        {
        }
    }
}

