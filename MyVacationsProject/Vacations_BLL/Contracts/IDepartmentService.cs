using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacations_DomainModel.Models.Department;

namespace Vacations_BLL.Contracts
{
    public interface IDepartmentService:IService
    {
        Task CreateDepartment(Department department);

        Task DeleteDepartment(int id);

        Task UpdateDepartment(Department department);

        List<Department> GetAllDepartments();

    }
}
