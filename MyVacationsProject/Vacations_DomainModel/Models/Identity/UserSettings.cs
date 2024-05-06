using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacations_DomainModel.Models.Identity
{
    public class UserSettings : Entity<long>
    {
        public User? User { get; set; }
        public string? UserId { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
        public required bool DarkThemeEnabled { get; set; }
    }
}
