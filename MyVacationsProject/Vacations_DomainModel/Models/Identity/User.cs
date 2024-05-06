using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacations_DomainModel.Models.Department;

namespace Vacations_DomainModel.Models.Identity
{
    public class User : IdentityUser, IEntity
    {
        public required UserSettings UserSettings { get; set; }

        public required UserRoleEnum Role { get; set; }

        public Employee? Employee { get; set; }

    }
}
