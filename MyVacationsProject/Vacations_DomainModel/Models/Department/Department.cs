using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacations_DomainModel.Models.Department
{
    public class Department: Entity<int>
    {
        [Required]
        public required string Name { get; set; }

        public IEnumerable<Employee>? Employees { get; set; }
    }
}
