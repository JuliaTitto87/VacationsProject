using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacations_DomainModel.Models.Identity;

namespace Vacations_BLL.Contracts.Identity
{
    public class UserBriefModel
    {
        public required string Email { get; set; }
        public required UserRoleEnum Role { get; set; }

    }
}
