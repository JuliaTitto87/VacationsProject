using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vacations_DomainModel.Models.Vacation
{
    public class Holiday
    {
        public string? HolidayName { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }
    }
}
