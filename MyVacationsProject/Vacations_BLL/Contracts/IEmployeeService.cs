using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacations_DomainModel.Models.Department;

namespace Vacations_BLL.Contracts
{
    public interface IEmployeesService : IService
    {
        List<Employee> GetEmployees();
        Task DeleteEmployee(Employee employee);
        Task AddEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
    }
}
