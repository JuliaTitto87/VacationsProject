using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vacations_DomainModel.Models.Department;
using Vacations_DomainModel.Models.Identity;

namespace Vacations_DAL.DbEntityConfigurations.Department
{
    internal class EmployeeDBConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasOne(fv => fv.Department)
              .WithMany(ft => ft.Employees);

            builder.HasIndex(employee => employee.PersonnelNumber)
        .IncludeProperties(
            nameof(Employee.Id),
            nameof(Employee.CurrentDurationOfVocation),
            nameof(Employee.DepartmentId),
            nameof(Employee.EmployeesPosition),
            nameof(Employee.FirstName),
            nameof(Employee.LastName),
            nameof(Employee.IsHeadOfDepartment)
        );
            builder.HasOne<User>()
    .WithOne(user => user.Employee)
    .HasForeignKey<Employee>(us => us.UserId);
        }
    }
}
