using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacations_BLL.Contracts.Identity
{
    public class UserCreateModel : UserBriefModel
    {
        public required string Password { get; set; }
    }
}
