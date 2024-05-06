using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using Vacations_DomainModel.Models.Department;

namespace MyVacationsProject.Models
{
    public class VacationsViewModel
    {
        public SelectList Months 
        {
            get
            {
                return new SelectList(DateTimeFormatInfo.CurrentInfo.MonthNames);
            }
        }

        public string Month { get; set; }

        public int Year { get; set; }
        public Department Department { get; set; }

        public int EmployeeId { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
